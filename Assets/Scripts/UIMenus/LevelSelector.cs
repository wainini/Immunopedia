using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LevelSelector : MonoBehaviour
{
    [System.Serializable]
    public class LevelButtons
    {
        public string name;
        public RectTransform uiRT;
        public LevelButtons(string name, RectTransform uiRT)
        {
            this.name = name;
            this.uiRT = uiRT;
        }
    }

    [SerializeField] private List<LevelButtons> levelButtons = new List<LevelButtons>();
    private void OnEnable()
    {
        //PlayerPrefs.SetInt("Level 1", 1);
        //PlayerPrefs.SetInt("Level 2", 1);
        //PlayerPrefs.SetInt("Level 3", 0);
        //PlayerPrefs.SetInt("Level 4", 0);
        //PlayerPrefs.SetInt("Level 5", 0);
        //PlayerPrefs.SetInt("Level 6", 0);
        //PlayerPrefs.SetInt("Level 7", 0);
        //PlayerPrefs.SetInt("Level 8", 0);
        //PlayerPrefs.SetInt("Level 9", 0);
        //PlayerPrefs.SetInt("Level 10", 0);

        bool unlockNext = false;
        foreach(LevelButtons button in levelButtons)
        {
            int score = PlayerPrefs.GetInt(button.name, 0);
            if(score >= 1)
            {
                button.uiRT.gameObject.SetActive(true);
                Image[] stars = button.uiRT.GetComponentsInChildren<Image>();
                // index 0 is the Button Image
                AddListenerToButton(button);
                for (int i = 1; i <= score; i++)
                {
                    stars[i].color = Color.white;
                }
                unlockNext = true;
            }
            else if(unlockNext || button.name == "Level 1")
            {
                button.uiRT.gameObject.SetActive(true);
                AddListenerToButton(button);
                unlockNext = false;
            }
            else
            {
                button.uiRT.gameObject.SetActive(false);
            }
        }
    }

    private void AddListenerToButton(LevelButtons button)
    {
        button.uiRT.GetComponentInChildren<Button>().onClick.AddListener(
            () => SceneManager.LoadScene(button.name));
    }

    public void ExitLevelSelector()
    {
        MenuManager.instance.CloseMenu();
    }
}
