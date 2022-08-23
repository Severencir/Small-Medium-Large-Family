using Unity.Entities;
using Unity.Mathematics;

[GenerateAuthoringComponent]
[InternalBufferCapacity(360)]
public struct OrbitBufferData : IBufferElementData 
{
    public float3 position;
    public float3 rotation;
}
