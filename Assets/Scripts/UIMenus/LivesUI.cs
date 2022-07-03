using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LivesUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private GameManager gameManager;

    private void OnEnable()
    {
        GameManager.instance.OnReduceLive += ChangeLiveText;
    }

    private void OnDisable()
    {
        GameManager.instance.OnReduceLive -= ChangeLiveText;
    }

    private void ChangeLiveText(int currLives)
    {
        text.text = "Lives: " + currLives;
    }

}
