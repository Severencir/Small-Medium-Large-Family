using System.Collections;
using UnityEngine;
using Unity.Entities;

[UpdateInGroup(typeof(InitializationSystemGroup))]
public partial class Inp : SystemBase
{
    public static Inputs inputs;
    protected override void OnStartRunning()
    {
        inputs = new Inputs();
        inputs.Enable();
    }
    protected override void OnUpdate()
    {
    }
    public void Enable()
    {
        inputs.Enable();
    }

    public void Disable()
    {
        inputs.Disable();
    }

    public static void PlayerEnable()
    {
        inputs.Player.Enable();
    }

    public static void PlayerDisable()
    {
        inputs.Player.Disable();
    }
}
