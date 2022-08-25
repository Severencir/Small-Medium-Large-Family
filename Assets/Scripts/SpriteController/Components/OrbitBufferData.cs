using Unity.Entities;
using Unity.Mathematics;

[GenerateAuthoringComponent]
[InternalBufferCapacity(180)]
public struct OrbitBufferData : IBufferElementData 
{
    public float3 position;
    public quaternion rotation;
}
