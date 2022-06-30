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
    [SerializeField] private GameObject settingMenu;
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
    }
    private void Start()
    {
        SetVolume("MasterVol");
        SetVolume("BGMVol");
        SetVolume("SFXVol");
    }
    private void SetVolume(string group)
    {
        mixer.SetFloat(group, Mathf.Log10(PlayerPrefs.GetFloat(group, 1) * 20));
    }

    private void OnEnable()
    {
        SceneManager.sceneUnloaded += OnSceneUnloaded;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
        SceneManager.sceneLoaded -= OnSceneLoaded;
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
    }

    private void OnSceneUnloaded(Scene scene)
    {
        CloseAllMenu();
    }

    public void OpenMenu(GameObject menu)
    {
        menu.SetActive(true);
        MenuStack.Push(menu);
    }

    public void CloseMenu()
    {
        MenuStack.Pop().SetActive(false);
    }

    public void CloseAllMenu()
    {
        if (MenuStack.Count <= 0) return;
        for(int i = 0; i < MenuStack.Count; i++)
        {
            MenuStack.Pop().SetActive(false);
        }
    }

    public void OpenDialogPopUp()
    {
        OpenMenu(dialogPopUp);
    }
}
