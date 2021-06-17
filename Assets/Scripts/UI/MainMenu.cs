using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject quitGameButton;
    [SerializeField]
    private GameObject optionsMenu;
    private Transform noSavePopup;

    void Awake()
    {
        //QualitySettings.vSyncCount = 0;
        //Application.targetFrameRate = 60;
    }

    void Start()
    {
        noSavePopup = FindObjectOfType<Canvas>().transform.Find("NoSavePopup");
    }

    public void NewGame()
    {
        SceneManager.LoadScene("BuildScene", LoadSceneMode.Single);
    }

    public void LoadGame()
    {
        if (File.Exists(Application.persistentDataPath + "/save1.sav"))
        {
            PlayerPrefs.SetInt("isLoading", 1);
            SceneManager.LoadScene("BuildScene", LoadSceneMode.Single);
        }
        else
        {
            noSavePopup.gameObject.SetActive(true);
            quitGameButton.GetComponent<Button>().interactable = false;
        }
    }

    public void OpenOptions()
    {
        optionsMenu.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void CloseNoSavePopup()
    {
        noSavePopup.gameObject.SetActive(false);
        quitGameButton.GetComponent<Button>().interactable = true;
    }
}
