using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageMenu : MonoBehaviour
{
    [SerializeField]
    OptionsManager optionsManager;
    [SerializeField]
    GameObject confirmationPopUp;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void SelectNewLanguage(string language)
    {
        optionsManager.ApplySelectedLanguage(language, false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel") || Input.GetButtonUp("Cancel Road"))
        {
            FindObjectOfType<OptionsMenu>().SubMenuClosed();
            gameObject.SetActive(false);
        }
    }
}
