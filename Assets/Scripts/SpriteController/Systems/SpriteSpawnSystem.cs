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

            orbit.speed = tempRNG.NextFloat(0.5f, 2f);
            
            ecb.RemoveComponent<RandomizeOrbitTag>(e);
        }).Schedule();
        
        EndECB.AddJobHandleForProducer(Dependency);
    }
}
