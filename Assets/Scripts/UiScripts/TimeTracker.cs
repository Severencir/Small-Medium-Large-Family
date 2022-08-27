using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeTracker : MonoBehaviour
{
    TMP_Text textComp;
    // Start is called before the first frame update
    void Start()
    {
        textComp = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        textComp.text = "Time: " + (Time.time - Attack.startTime).ToString("0");
    }
}
