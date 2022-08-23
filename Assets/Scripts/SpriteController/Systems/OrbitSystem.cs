using Unity.Entities;
using Unity.Collections;
using Unity.Transforms;
using Unity.Mathematics;

public partial class OrbitSystem : SystemBase
{
    
    protected override void OnUpdate()
    {
        float3 playerPos = StaticPosition.positions["Player"];
        float elapsedTime = (float)Time.ElapsedTime;
        Entities.ForEach((ref Translation trans, ref Rotation rotation, in OrbitData orbit) =>
        {
            float x = math.cos(elapsedTime * orbit.speed) * orbit.a;
            float y = math.sin(elapsedTime * orbit.speed) * orbit.b;
            float3 newTranslation = new(x, 0, y);

            rotation.Value = quaternion.LookRotationSafe(-newTranslation, new(0, 1, 0));

            trans.Value = newTranslation;

        }).Schedule();
	}
}
