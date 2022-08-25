using Unity.Entities;
using Unity.Collections;
using Unity.Transforms;
using Unity.Mathematics;
using Unity.VisualScripting.FullSerializer;

public partial class SpriteSpawnSystem : SystemBase
{
    const float dPI = math.PI * 2;

    protected override void OnUpdate()
    {
        EndSimulationEntityCommandBufferSystem EndECB = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
        EntityCommandBuffer ecb = EndECB.CreateCommandBuffer();
        float3 playerPosition = StaticPosition.positions["Player"];


        EntityArchetype rotationDads = EntityManager.CreateArchetype(typeof(Rotation), typeof(Translation), typeof(FollowPlayerTag), typeof(LocalToWorld));

        int redToAdd = SpriteManager.red.ToAdd();
        int greenToAdd = SpriteManager.green.ToAdd();
        int yellowToAdd = SpriteManager.yellow.ToAdd();
        int blueToAdd = SpriteManager.blue.ToAdd();
        int purpleToAdd = SpriteManager.purple.ToAdd();

        Entities.ForEach((Entity e, ref SpriteSpawnData spawn, ref RandomData rand) =>
        {
            for (int i = 0; i < greenToAdd; i++)
            {
                Entity sprite = ecb.Instantiate(spawn.greenSprite);
                ecb.SetComponent(sprite, new Rotation { Value = quaternion.Euler(rand.rng.NextFloat3(new(dPI, dPI, dPI))) } );
            }
            for (int i = 0; i < yellowToAdd; i++)
            {
                Entity sprite = ecb.Instantiate(spawn.yellowSprite);
                ecb.SetComponent(sprite, new Rotation { Value = quaternion.Euler(rand.rng.NextFloat3(new(dPI, dPI, dPI))) });
            }
            for (int i = 0; i < blueToAdd; i++)
            {
                Entity sprite = ecb.Instantiate(spawn.blueSprite);
                ecb.SetComponent(sprite, new Rotation { Value = quaternion.Euler(rand.rng.NextFloat3(new(dPI, dPI, dPI))) });
            }
            for (int i = 0; i < redToAdd; i++)
            {
                Entity sprite = ecb.Instantiate(spawn.redSprite);
                ecb.SetComponent(sprite, new Rotation { Value = quaternion.Euler(rand.rng.NextFloat3(new(dPI, dPI, dPI))) });
            }
            for (int i = 0; i < purpleToAdd; i++)
            {
                Entity sprite = ecb.Instantiate(spawn.purpleSprite);
                ecb.SetComponent(sprite, new Rotation { Value = quaternion.Euler(rand.rng.NextFloat3(new(dPI, dPI, dPI))) });
            }


        }).Schedule();

        int redToRemove = SpriteManager.red.ToRemove();
        int greenToRemove = SpriteManager.green.ToRemove();
        int yellowToRemove = SpriteManager.yellow.ToRemove();
        int blueToRemove = SpriteManager.blue.ToRemove();
        int purpleToRemove = SpriteManager.purple.ToRemove();

        
        Entities.WithAll<RedSpriteTag>().ForEach((Entity e) =>
        {
            if (redToRemove > 0)
            {
                ecb.DestroyEntity(e);
                redToRemove--;
            }
        }).Schedule();
        Entities.WithAll<GreenSpriteTag>().ForEach((Entity e) =>
        {
            if (greenToRemove > 0)
            {
                ecb.DestroyEntity(e);
                greenToRemove--;
            }
        }).Schedule();
        Entities.WithAll<YellowSpriteTag>().ForEach((Entity e) =>
        {
            if (yellowToRemove > 0)
            {
                ecb.DestroyEntity(e);
                yellowToRemove--;
            }
        }).Schedule();
        Entities.WithAll<BlueSpriteTag>().ForEach((Entity e) =>
        {
            if (blueToRemove > 0)
            {
                ecb.DestroyEntity(e);
                blueToRemove--;
            }
        }).Schedule();
        Entities.WithAll<PurpleSpriteTag>().ForEach((Entity e) =>
        {
            if (purpleToRemove > 0)
            {
                ecb.DestroyEntity(e);
                purpleToRemove--;
            }
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
                buffData.position = new float3(x, 0, y);

                newRot = quaternion.LookRotationSafe(-buffData.position, new float3(0, 1, 0));

                buffData.rotation = newRot;
                trans.Value = buffData.position;
                orbitBuffer.Add(buffData);
            }
            ecb.RemoveComponent<OrbitData>(e);
        }).Schedule();

        EndECB.AddJobHandleForProducer(Dependency);
    }
}
