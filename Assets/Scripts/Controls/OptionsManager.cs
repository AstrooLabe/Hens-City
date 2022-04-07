using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class OptionsManager : MonoBehaviour
{
    private OptionsObject optionsObject = new OptionsObject();

    [SerializeField]
    private AudioSource musicSource;
    [SerializeField]
    private AudioSource sfxSource;

    void Start()
    {
        if (!File.Exists(Application.persistentDataPath + "/options.cfg"))
        {
            SaveOptionsFile();
        }
        LoadOptions();
    }

    void Update()
    {

    }

    private void LoadOptions()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/options.cfg", FileMode.Open);
        optionsObject = (OptionsObject)bf.Deserialize(file);
        file.Close();

        ApplyNewMusicVolume(optionsObject.musicVolume, true);
        ApplyNewSFXVolume(optionsObject.sfxVolume, true);
        SetScreenMode(optionsObject.screen, true);
    }

    private void SaveOptionsFile()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/options.cfg");
        bf.Serialize(file, optionsObject);
        file.Close();
    }

    public void ApplyNewMusicVolume(int newVolume, bool isStartUp)
    {
        optionsObject.musicVolume = newVolume;
        musicSource.volume = (float)newVolume / 10;
        if(!isStartUp) SaveOptionsFile();
    }

    public int GetMusicVolume()
    {
        return (optionsObject.musicVolume);
    }

    public void ApplyNewSFXVolume(int newVolume, bool isStartUp)
    {
        optionsObject.sfxVolume = newVolume;
        sfxSource.volume = (float)newVolume / 10;
        if (!isStartUp) SaveOptionsFile();
    }

    public int GetSFXVolume()
    {
        return (optionsObject.sfxVolume);
    }

    public void ApplySelectedLanguage(string language, bool isStartUp)
    {
        optionsObject.selectedLanguage = language;
        if (!isStartUp) SaveOptionsFile();

        //Apply selected language on texts of the whole game
    }

    public string GetSelectedLanguage()
    {
        return optionsObject.selectedLanguage;
    }

    public void SetVSync(bool isStartUp)
    {
        optionsObject.vSync = !optionsObject.vSync;

        QualitySettings.vSyncCount = optionsObject.vSync == true ? 1 : 0;
        if (!isStartUp) SaveOptionsFile();
    }

    public bool GetVSync()
    {
        return optionsObject.vSync;
    }

    public void SetScreenMode(string newMode, bool isStartUp)
    {
        switch (newMode)
        {
            case Options.FULLSCREEN:
                optionsObject.screen = newMode;
                Screen.SetResolution(1920, 1080,FullScreenMode.ExclusiveFullScreen);
                break;
            case Options.BORDERLESS:
                optionsObject.screen = newMode;
                Screen.SetResolution(1920, 1080, FullScreenMode.FullScreenWindow);
                break;
            case Options.WINDOWED:
                optionsObject.screen = newMode;
                Screen.SetResolution(800, 600, false);
                //Screen.fullScreenMode = FullScreenMode.Windowed;
                break;
        }
        if (!isStartUp) SaveOptionsFile();
    }

    public string GetScreenMode()
    {
        return optionsObject.screen;
    }

}
