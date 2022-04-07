using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class GraphicsMenu : MonoBehaviour
{
    [SerializeField]
    OptionsManager optionsManager;

    [SerializeField]
    private Toggle vSyncToggle;
    [SerializeField]
    private TMP_Dropdown screenModeDropdown;

    // Start is called before the first frame update
    void Start()
    {
        //Load settings
        vSyncToggle.isOn = optionsManager.GetVSync();
        screenModeDropdown.value = screenModeDropdown.options.FindIndex(option => { return option.text == optionsManager.GetScreenMode(); });
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

    public void SetVSync()
    {
        optionsManager.SetVSync(false);
    }

    public void SetScreenMode()
    {
        optionsManager.SetScreenMode(screenModeDropdown.options[screenModeDropdown.value].text, false);
    }
}
