using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHoverSound : MonoBehaviour, IPointerEnterHandler
{
    //attach this to buttons that are instantiated

    public void OnPointerEnter(PointerEventData eventData)
    {
        AudioManager.instance.PlaySound("ButtonHover", SoundOutput.sfx, new Vector2(0.5f, 1.5f));
    }

}
