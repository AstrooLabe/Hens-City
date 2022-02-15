using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField]
    GameObject optionsMenu;

    OptionsObject options = new OptionsObject();

    private void Start()
    {
        if (File.Exists(Application.persistentDataPath + "/options.sav"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/options.sav", FileMode.Open);
            options = (OptionsObject)bf.Deserialize(file);
            file.Close();
        }
        else
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(Application.persistentDataPath + "/options.sav");
            bf.Serialize(file, options);
            file.Close();
        }
    }

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
