using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField]
    GameObject optionsMenu;
    [SerializeField]
    GameObject audioMenu;

    private bool subMenuDisplayed = false;

    private void Start()
    {

    }

    private void Update()
    {
        if ((Input.GetButtonDown("Cancel") || Input.GetButtonUp("Cancel Road")) && !subMenuDisplayed)
        {
            HideMenu();
        }
    }

    public void HideMenu()
    {
        GameObject.Find("UI Audio Source").GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("Sounds/UI/clickSound");
        GameObject.Find("UI Audio Source").GetComponent<AudioSource>().Play();
        optionsMenu.SetActive(false);
    }

    private void OnEnable()
    {
        foreach (MenuButtonAnimations button in transform.GetComponentsInChildren<MenuButtonAnimations>())
        {
            button.ResetAnimation();
        }
    }

    public void OpenAudio()
    {
        subMenuDisplayed = true;
        audioMenu.SetActive(true);
    }

    public void SubMenuClosed()
    {
        subMenuDisplayed = false;
    }
}
