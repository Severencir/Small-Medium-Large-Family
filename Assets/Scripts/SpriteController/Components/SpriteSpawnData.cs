using Unity.Entities;

[GenerateAuthoringComponent]
public struct SpriteSpawnData : IComponentData 
{
    public Entity greenSprite;
    public Entity redSprite;
    public Entity yellowSprite;
    public Entity blueSprite;
    public Entity purpleSprite;
}
