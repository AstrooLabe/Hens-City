using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField]
    GameObject optionsMenu;

    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
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
}
