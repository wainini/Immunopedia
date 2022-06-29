using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LivesUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.instance;
        gameManager.OnReduceLive += ChangeLiveText;
    }

    private void OnDisable()
    {
        gameManager.OnReduceLive -= ChangeLiveText;
    }

    private void ChangeLiveText(int currLives)
    {
        text.text = "Lives: " + currLives;
    }

}
