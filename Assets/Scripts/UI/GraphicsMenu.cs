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
    [SerializeField]
    private TMP_Dropdown frameratesDropdown;

    // Start is called before the first frame update
    void Start()
    {
        //Load settings

        List<string> refreshRates = new List<string>();
        Resolution[] possibleResolutions = Screen.resolutions;

        foreach (Resolution resolution in possibleResolutions)
        {
            if (refreshRates.Count == 0)
                refreshRates.Add("30");

            if (resolution.refreshRate % 5 == 0)
            {
                if (!refreshRates.Exists(rate => rate == resolution.refreshRate.ToString()))
                {
                    refreshRates.Add(resolution.refreshRate.ToString());
                }
            }
        }

        refreshRates.Sort();

        frameratesDropdown.ClearOptions();
        frameratesDropdown.AddOptions(refreshRates);
        vSyncToggle.isOn = optionsManager.GetVSync();

        screenModeDropdown.value = screenModeDropdown.options.FindIndex(option => { return option.text == optionsManager.GetScreenMode(); });
        frameratesDropdown.value = frameratesDropdown.options.FindIndex(option => { return option.text == optionsManager.GetFramerate().ToString(); });
    }

    private void OnEnable()
    {
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
        optionsManager.SetVSync(vSyncToggle.isOn, false);
    }

    public void SetScreenMode()
    {
        optionsManager.SetScreenMode(screenModeDropdown.options[screenModeDropdown.value].text, false);
    }

    public void SetFramerate()
    {
        optionsManager.SetFramerate(int.Parse(frameratesDropdown.options[frameratesDropdown.value].text), false);
    }
}
