using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject pauseMenuContent;
    [SerializeField]
    private GameObject loadGameButton;
    [SerializeField]
    private GameObject saveGameButton;
    [SerializeField]
    private GameObject quitGameButton;
    private Transform noSavePopup;

    private bool menuEnabled = false;

    void Start()
    {
        noSavePopup = FindObjectOfType<Canvas>().transform.Find("NoSavePopup");
    }

    public void ToggleMenu()
    {
        if (pauseMenuContent.activeInHierarchy == false)
        {
            pauseMenuContent.SetActive(true);
            menuEnabled = true;
        }
        else if (pauseMenuContent.activeInHierarchy == true)
        {
            loadGameButton.GetComponent<MenuButtonAnimations>().ResetAnimation();
            saveGameButton.GetComponent<MenuButtonAnimations>().ResetAnimation();
            quitGameButton.GetComponent<MenuButtonAnimations>().ResetAnimation();
            pauseMenuContent.SetActive(false);

            if (noSavePopup.gameObject.activeInHierarchy) noSavePopup.gameObject.SetActive(false);

            menuEnabled = false;
        }
    }

    public bool IsGamePaused() { return menuEnabled; }

    public void LoadGame()
    {
        if (File.Exists(Application.persistentDataPath + "/save1.sav"))
        {
            FindObjectOfType<SaveLoadSystem>().LoadGame();
        }
        else
        {
            noSavePopup.gameObject.SetActive(true);
            saveGameButton.GetComponent<Button>().interactable = false;
            quitGameButton.GetComponent<Button>().interactable = false;
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void CloseNoSavePopup()
    {
        GameObject.Find("UI Audio Source").GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("Sounds/UI/clickSound");
        GameObject.Find("UI Audio Source").GetComponent<AudioSource>().Play();
        noSavePopup.Find("ClosePopup").transform.Find("ForegroundImage").GetComponent<RectTransform>().sizeDelta = new Vector2(30f, 100f);
        noSavePopup.gameObject.SetActive(false);
        saveGameButton.GetComponent<Button>().interactable = true;
        quitGameButton.GetComponent<Button>().interactable = true;
    }
}
