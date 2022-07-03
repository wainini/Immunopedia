using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinMenu : MonoBehaviour
{
    [SerializeField] private GameObject starsLayout;
    private void OnEnable()
    {
        AudioManager.instance.PlaySound("WinBGM", SoundOutput.bgm);
        int score = GameManager.instance.score;
        Image[] stars = starsLayout.GetComponentsInChildren<Image>();
        for(int i=0; i< score;i++)
        {
            stars[i].color = Color.white;
        }
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
