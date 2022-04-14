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

    private int maxRefreshRate = 0;
    private GeneralUtils utils = new GeneralUtils();

    void Start()
    {
        List<int> refreshRates = utils.GetAllPossibleRefreshRates();

        maxRefreshRate = refreshRates[refreshRates.Count - 1];

        if (!File.Exists(Application.persistentDataPath + "/options.cfg"))
        {
            optionsObject.targetFPS = utils.GetAllPossibleRefreshRates()[utils.GetAllPossibleRefreshRates().Count - 1];

            List<CustomResolution> resList;
            resList = utils.GetAllPossibleResolutions();

            optionsObject.width = resList[resList.Count - 1].width;
            optionsObject.height = resList[resList.Count - 1].height;

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
        if (!isStartUp) SaveOptionsFile();
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

    public void SetVSync(bool value, bool isStartUp)
    {
        optionsObject.vSync = value;
        if (optionsObject.vSync)
            QualitySettings.vSyncCount = maxRefreshRate/optionsObject.targetFPS;
        else
            QualitySettings.vSyncCount = 0;
        if (!isStartUp) SaveOptionsFile();
    }

    public bool GetVSync()
    {
        return optionsObject.vSync;
    }

    public void SetScreenMode(string newMode, bool isStartUp)
    {
        optionsObject.screen = newMode;
        if (!isStartUp) SaveOptionsFile();
        ApplyGraphicsSettings();
    }

    public string GetScreenMode()
    {
        return optionsObject.screen;
    }

    public void ApplyGraphicsSettings()
    {
        switch (optionsObject.screen)
        {
            case Options.FULLSCREEN:
                Screen.SetResolution(optionsObject.width, optionsObject.height, FullScreenMode.ExclusiveFullScreen);
                break;
            case Options.BORDERLESS:
                Screen.SetResolution(optionsObject.width, optionsObject.height, FullScreenMode.FullScreenWindow);
                break;
            case Options.WINDOWED:
                Screen.SetResolution(optionsObject.width, optionsObject.height, false);
                break;
        }
        if (optionsObject.vSync)
            QualitySettings.vSyncCount = maxRefreshRate / optionsObject.targetFPS;
        else
            QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = optionsObject.targetFPS;
    }

    public void SetFramerate(int framerate, bool isStartUp)
    {
        optionsObject.targetFPS = framerate;
        Application.targetFrameRate = framerate;
        if (!isStartUp) SaveOptionsFile();
    }

    public int GetFramerate()
    {
        return optionsObject.targetFPS;
    }

    public void SetResolution(int width, int height, bool isStartUp)
    {
        optionsObject.width = width;
        optionsObject.height = height;
        if (!isStartUp) SaveOptionsFile();
        ApplyGraphicsSettings();
    }

    public CustomResolution GetResolution()
    {
        CustomResolution res = new CustomResolution(optionsObject.width, optionsObject.height);
        return res;
    }

}
