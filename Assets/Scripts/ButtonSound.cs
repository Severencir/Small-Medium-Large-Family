using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSound : MonoBehaviour, IPointerEnterHandler
{
        public void OnPointerEnter(PointerEventData eventData)
        {
            AudioManager.Play("UISound1");
        }    
}
