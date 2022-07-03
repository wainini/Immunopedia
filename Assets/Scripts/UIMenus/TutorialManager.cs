using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    private Queue<Image> tutorialQueue = new Queue<Image>();
    private Image currentTutorial;
    void Start()
    {
        if(PlayerPrefs.GetInt("WantTutorial", 1) == 0)
        {
            StopTutorial();
        }

        var childs = GetComponentsInChildren<Image>();
       
        foreach(Image child in childs)
        {
            if (child.CompareTag("Tutorial"))
            {
                tutorialQueue.Enqueue(child);
                child.gameObject.SetActive(false);
            }
        }
        if (tutorialQueue.Count != 0)
        {
            tutorialQueue.Peek().gameObject.SetActive(true);
        }

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if(tutorialQueue.Count > 0)
            {
                Destroy(tutorialQueue.Dequeue().gameObject); //close the last tutorial
                Debug.Log(tutorialQueue.Count);
                if(tutorialQueue.Count == 0)
                {
                    StopTutorial();
                }
                else
                    tutorialQueue.Peek().gameObject.SetActive(true); //activate the next tutorial
            }
        }
    }

    private void StopTutorial()
    {
        Destroy(gameObject);
    }
}
