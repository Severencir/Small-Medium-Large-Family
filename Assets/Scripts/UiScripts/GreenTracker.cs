using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GreenTracker : MonoBehaviour
{
    TMP_Text textComp;
    private void Start()
    {
        textComp = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        textComp.text = SpriteManager.green.active.ToString();
    }
}
