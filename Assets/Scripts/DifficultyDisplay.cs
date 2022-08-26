using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DifficultyDisplay : MonoBehaviour
{
    TMP_Text textComp;

    private void Start()
    {
        textComp = GetComponent<TMP_Text>();
    }
    private void Update()
    {
        textComp.text = "Difficulty Level: " + (1 + (Time.time - Attack.startTime) * 0.01f).ToString("0");

    }
}
