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
    [SerializeField]
    private TMP_Dropdown resolutionssDropdown;

    private GeneralUtils utils = new GeneralUtils();

    // Start is called before the first frame update
    void Start()
    {
        //Load settings

        frameratesDropdown.ClearOptions();
        frameratesDropdown.AddOptions(utils.GetAllPossibleRefreshRatesString());
        resolutionssDropdown.ClearOptions();
        resolutionssDropdown.AddOptions(utils.GetAllPossibleResolutionsString());
        vSyncToggle.isOn = optionsManager.GetVSync();

        screenModeDropdown.value = screenModeDropdown.options.FindIndex(option => { return option.text == optionsManager.GetScreenMode(); });
        frameratesDropdown.value = frameratesDropdown.options.FindIndex(option => { return option.text == optionsManager.GetFramerate().ToString(); });
        resolutionssDropdown.value = resolutionssDropdown.options.FindIndex(option => { return option.text == optionsManager.GetResolution().displayRes; });
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

    public void SetResolution()
    {
        CustomResolution resToSet = utils.GetAllPossibleResolutions().Find(res => res.displayRes == resolutionssDropdown.options[resolutionssDropdown.value].text);

        optionsManager.SetResolution(resToSet.width, resToSet.height, false);

    }
}
