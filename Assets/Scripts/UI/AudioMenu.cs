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

    private int musicVolume = 10;
    private int sfxVolume = 10;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            FindObjectOfType<OptionsMenu>().SubMenuClosed();
            gameObject.SetActive(false);
        }
    }

    public void IncreaseMusicVolume()
    {
        if(musicVolume < 10)
        {
            musicVolume++;
            music.text = "Music : " + musicVolume.ToString();
        }
    }

    public void DecreaseMusicVolume()
    {
        if (musicVolume > 0)
        {
            musicVolume--;
            music.text = "Music : " + musicVolume.ToString();
        }
    }

    public void IncreaseSFXVolume()
    {
        if (sfxVolume < 10)
        {
            sfxVolume++;
            sfx.text = "SFX : " + sfxVolume.ToString();
        }
    }

    public void DecreaseSFXVolume()
    {
        if (sfxVolume > 0)
        {
            sfxVolume--;
            sfx.text = "SFX : " + sfxVolume.ToString();
        }
    }
}
