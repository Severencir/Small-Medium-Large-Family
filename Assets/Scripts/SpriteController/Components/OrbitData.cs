using Unity.Entities;
using Unity.Mathematics;

[GenerateAuthoringComponent]
public struct OrbitData : IComponentData 
{
    public float a;
    public float b;
    public int frames;
}
