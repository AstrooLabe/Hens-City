using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class OptionsObject
{
    public float musicVolume = 10;
    public float sfxVolume = 10;

    public int targetFPS = Options.THIRTY_FPS;
    public bool vSync = false;
    public string screen = Options.FULLSCREEN;

    public string selectedLanguage = Options.ENGLISH;
}
