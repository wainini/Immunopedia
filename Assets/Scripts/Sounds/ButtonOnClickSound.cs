using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonOnClickSound : MonoBehaviour, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        AudioManager.instance.PlaySound("ButtonClick", SoundOutput.sfx, new Vector2(0.8f, 1.2f));
    }
}
