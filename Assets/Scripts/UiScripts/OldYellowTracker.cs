using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OldYellowTracker : MonoBehaviour
{
    Text textComp;
    private void Start()
    {
        textComp = GetComponent<Text>();
    }

    private void Update()
    {
        textComp.text = SpriteManager.yellow.active.ToString();
    }
}
