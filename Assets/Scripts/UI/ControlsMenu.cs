using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsMenu : MonoBehaviour
{
    [SerializeField]
    OptionsManager optionsManager;

    // Start is called before the first frame update
    void Start()
    {
        //Load settings
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
