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

        ApplyNewMusicVolume(optionsObject.musicVolume);
        ApplyNewSFXVolume(optionsObject.sfxVolume);

    }

    private void SaveOptionsFile()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/options.cfg");
        bf.Serialize(file, optionsObject);
        file.Close();
    }

    public void ApplyNewMusicVolume(int newVolume)
    {
        optionsObject.musicVolume = newVolume;
        musicSource.volume = (float)newVolume / 10;
        SaveOptionsFile();
    }

    public int GetMusicVolume()
    {
        return (optionsObject.musicVolume);
    }

    public void ApplyNewSFXVolume(int newVolume)
    {
        optionsObject.sfxVolume = newVolume;
        sfxSource.volume = (float)newVolume / 10;
        SaveOptionsFile();
    }

    public int GetSFXVolume()
    {
        return (optionsObject.sfxVolume);
    }
}
