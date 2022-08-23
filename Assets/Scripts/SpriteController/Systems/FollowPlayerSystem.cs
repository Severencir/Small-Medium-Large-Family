using Unity.Entities;
using Unity.Collections;
using Unity.Transforms;
using Unity.Mathematics;

public partial class FollowPlayerSystem : SystemBase
{
    protected override void OnUpdate()
    {
        float3 playerPos = StaticPosition.positions["Player"];

        Entities.WithAll<FollowPlayerTag>().ForEach((ref Translation trans) =>
        {
            trans.Value = playerPos + new float3(0, 0.5f, 0);

        }).Schedule();
	}
}
