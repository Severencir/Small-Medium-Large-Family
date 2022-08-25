using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PurpleTracker : MonoBehaviour
{
    TMP_Text textComp;
    private void Start()
    {
        textComp = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        textComp.text = SpriteManager.purple.active.ToString();
    }
}
