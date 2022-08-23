using Unity.Entities;
using Unity.Collections;
using Unity.Transforms;
using Unity.Mathematics;

public partial class SpriteSpawnSystem : SystemBase
{
    const float dPI = math.PI * 2;
    protected override void OnUpdate()
    {
        EndSimulationEntityCommandBufferSystem EndECB = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
        EntityCommandBuffer ecb = EndECB.CreateCommandBuffer();
        float3 playerPosition = StaticPosition.positions["Player"];

        EntityArchetype rotationDads = EntityManager.CreateArchetype(typeof(Rotation), typeof(Translation), typeof(FollowPlayerTag), typeof(LocalToWorld));

        if (Inp.inputs.TestControls.SpawnSprite.IsPressed())
        {
            Entities.ForEach((ref SpriteSpawnData spawn) => {
                spawn.quantity++;
            }).Schedule();
        }
        

        Entities.ForEach((Entity e, ref SpriteSpawnData spawn, ref RandomData rand) =>
        {
            for (int i = 0; i < spawn.quantity; i++)
            {
                Entity sprite = ecb.Instantiate(spawn.spritePrefab);

                ecb.SetComponent(sprite, new Rotation { Value = quaternion.Euler(rand.rng.NextFloat3(new(dPI, dPI, dPI))) } );
            }

            spawn.quantity = 0;
        }).Schedule();

        Entities.WithAll<RandomizeOrbitTag>().ForEach((Entity e, ref OrbitData orbit) =>
        {
            Random tempRNG = new Random((uint)e.Index);
            orbit.a = tempRNG.NextFloat(1f, 2f);
            orbit.b = tempRNG.NextFloat(1f, 2f);

            orbit.frames = tempRNG.NextInt(180, 360);
            
            ecb.RemoveComponent<RandomizeOrbitTag>(e);
        }).Schedule();

        Entities.ForEach((Entity e, ref OrbitData orbit, ref DynamicBuffer<OrbitBufferData> orbitBuffer, ref Translation trans) =>
        {
            float x = math.cos(math.radians(360f / orbit.frames * (orbit.frames - 1))) * orbit.a;
            float y = math.sin(math.radians(360f / orbit.frames * (orbit.frames - 1))) * orbit.b;
            trans.Value = new float3(x, 0, y);
            for (int i = 0; i < orbit.frames; i++)
            {
                OrbitBufferData buffData = new();
                quaternion newRot;
                
                x = math.cos(math.radians(360f / orbit.frames) * i) * orbit.a;
                y = math.sin(math.radians(360f / orbit.frames) * i) * orbit.b;
                float3 newPos = new float3(x, 0, y);
                
                newRot = quaternion.LookRotationSafe(-newPos, new float3(0, 1, 0));

                trans.Value = newPos;
                buffData.position = newPos;
                buffData.rotation = newRot;
                
                orbitBuffer.Add(buffData);
            }
            ecb.RemoveComponent<OrbitData>(e);
        }).Schedule();

        EndECB.AddJobHandleForProducer(Dependency);
    }
}
