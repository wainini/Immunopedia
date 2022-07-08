using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MenuManager : MonoBehaviour
{
    public static MenuManager instance { get; private set; }

    [SerializeField] private AudioMixer mixer;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject almanacMenu;
    [SerializeField] private GameObject winMenu;
    [SerializeField] private GameObject loseMenu;
    [SerializeField] private GameObject dialogPopUp;

    private Stack<GameObject> MenuStack = new Stack<GameObject>();

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
<<<<<<< Updated upstream
        Screen.SetResolution(Screen.width, Screen.width * 9 / 16, true);
=======
        //Screen.SetResolution(Screen.width, Screen.width * 9 / 16, false);
>>>>>>> Stashed changes
    }
    private void Start()
    {
        SetVolume("MasterVol");
        SetVolume("BGMVol");
        SetVolume("SFXVol");
    }
    private void SetVolume(string group)
    {
        mixer.SetFloat(group, Mathf.Log10(PlayerPrefs.GetFloat(group, 1)) * 20);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        if(instance == this)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (MenuStack.Count == 0 && SceneManager.GetActiveScene().buildIndex != 0)
            {
                OpenMenu(pauseMenu);
            }
            else if(MenuStack.Count != 0)
            {
                CloseMenu();
            }
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode a)
    {
        GetComponent<Canvas>().worldCamera = Camera.main;
        CloseAllMenu();
    }

    private void OnSceneUnloaded(Scene scene)
    {
        CloseAllMenu();
    }

    public void OpenMenu(GameObject menu)
    {
        menu.SetActive(true);
        MenuStack.Push(menu);
        //AudioManager.instance.PlaySound("ButtonClick", SoundOutput.sfx, new Vector2(0.8f, 1.2f));
    }

    public void CloseMenu()
    {
        MenuStack.Pop().SetActive(false);
        //AudioManager.instance.PlaySound("ButtonClick", SoundOutput.sfx, new Vector2(0.8f, 1.2f));
    }

    public void CloseAllMenu()
    {
        if (MenuStack.Count <= 0) return;
        int count = MenuStack.Count;
        for(int i = 0; i < count; i++)
        {
            GameObject menu = MenuStack.Pop();
            if (menu != null) menu.SetActive(false);
        }
    }

    public void OpenDialogPopUp()
    {
        OpenMenu(dialogPopUp);
    }

    public void OpenAlmanac()
    {
        OpenMenu(almanacMenu);
    }

    public void OpenWinMenu()
    {
        OpenMenu(winMenu);
    }

    public void OpenLoseMenu()
    {
        OpenMenu(loseMenu);
    }

    public void OnButtonHoverSound()
    {
        AudioManager.instance.PlaySound("ButtonHover", SoundOutput.sfx, new Vector2(0.5f, 1.5f));
    }
}
