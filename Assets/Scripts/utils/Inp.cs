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
}
