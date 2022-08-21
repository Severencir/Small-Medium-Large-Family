using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteMeOnSight : MonoBehaviour
{
    Vector2 whatever;
    bool fuckYou;

    void funcitonIGuess()
    {
        whatever = Inp.inputs.Player.Move.ReadValue<Vector2>();
        fuckYou = Inp.inputs.Player.Fire.WasReleasedThisFrame();    
    }
    

}
