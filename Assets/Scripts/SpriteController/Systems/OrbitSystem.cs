using Unity.Entities;
using Unity.Collections;
using Unity.Transforms;
using Unity.Mathematics;

public partial class OrbitSystem : SystemBase
{
    
    protected override void OnUpdate()
    {
        float3 playerPos = StaticPosition.positions["Player"];
        float elapsedTime = (float)Time.ElapsedTime * 60f;
        int truncatedTime = (int)elapsedTime;
        Entities.ForEach((ref Translation trans, ref Rotation rotation, in DynamicBuffer<OrbitBufferData> orbitBuffer) =>
        {
            if (orbitBuffer.Length > 0)
            {
                int index = truncatedTime % orbitBuffer.Length;
                trans.Value = orbitBuffer[index].position;
                rotation.Value = orbitBuffer[index].rotation;
            }
        }).Schedule();
	}
}
