using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class OptionsObject
{
    public int musicVolume = 10;
    public int sfxVolume = 10;

    public int targetFPS = Options.SIXTY_FPS;
    public bool vSync = false;
    public string screen = Options.FULLSCREEN;

    public string selectedLanguage = Options.ENGLISH;
}
