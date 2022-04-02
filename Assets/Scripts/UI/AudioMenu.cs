using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AudioMenu : MonoBehaviour
{
    [SerializeField]
    private TMP_Text music;
    [SerializeField]
    private TMP_Text sfx;

    [SerializeField]
    OptionsManager optionsManager;


    void Start()
    {
        music.text = "Music : " + optionsManager.GetMusicVolume().ToString();
        sfx.text = "SFX : " + optionsManager.GetSFXVolume().ToString();
    }

    void Update()
    {
        if (Input.GetButtonDown("Cancel") || Input.GetButtonUp("Cancel Road"))
        {
            FindObjectOfType<OptionsMenu>().SubMenuClosed();
            gameObject.SetActive(false);
        }
    }

    public void IncreaseMusicVolume()
    {
        if(optionsManager.GetMusicVolume() < 10)
        {
            optionsManager.ApplyNewMusicVolume(optionsManager.GetMusicVolume()+1);
            music.text = "Music : " + optionsManager.GetMusicVolume().ToString();
        }
    }

    public void DecreaseMusicVolume()
    {
        if (optionsManager.GetMusicVolume() > 0)
        {
            optionsManager.ApplyNewMusicVolume(optionsManager.GetMusicVolume()-1);
            music.text = "Music : " + optionsManager.GetMusicVolume().ToString();
        }
    }

    public void IncreaseSFXVolume()
    {
        if (optionsManager.GetSFXVolume() < 10)
        {
            optionsManager.ApplyNewSFXVolume(optionsManager.GetSFXVolume()+1);
            sfx.text = "SFX : " + optionsManager.GetSFXVolume().ToString();
        }
    }

    public void DecreaseSFXVolume()
    {
        if (optionsManager.GetSFXVolume() > 0)
        {
            optionsManager.ApplyNewSFXVolume(optionsManager.GetSFXVolume()-1);
            sfx.text = "SFX : " + optionsManager.GetSFXVolume().ToString();
        }
    }
}
