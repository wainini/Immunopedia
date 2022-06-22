using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;

public class AreYouSure : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI message;
    [SerializeField] private Button yes, no;
    public static AreYouSure instance { get; private set; }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    public void OpenDialogPopUp(string message, Action<bool> function)
    {
        this.message.text = message;
        yes.onClick.AddListener(()=>function(true));
        no.onClick.AddListener(()=>function(false));
    }

    public void RemoveListener()
    {
        yes.onClick.RemoveAllListeners();
        no.onClick.RemoveAllListeners();
    }
}
