using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LivesUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    private GameManager gameManager;

    private void Awake()
    {
        gameManager = GameManager.instance;
    }
    private void OnEnable()
    {
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
