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
    public int width = 1920;
    public int height = 1080;

    public string selectedLanguage = Options.ENGLISH;
}
