using Unity.Entities;

[GenerateAuthoringComponent]
public struct SpriteSpawnData : IComponentData 
{
    public Entity spritePrefab;
    public int quantity;
}
