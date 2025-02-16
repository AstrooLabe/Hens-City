﻿using UnityEngine;

public class InputsManager : MonoBehaviour
{
    [SerializeField]
    private CameraPlayer cameraP;
    [SerializeField]
    private AssetPlacer placer;
    [SerializeField]
    private PauseMenu pauseMenu;
    [SerializeField]
    private BuilderMenu builderMenu;

    private bool hasCameraMoved = false;

    public bool isCursorOnButton = false;

    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            pauseMenu.ToggleMenu();
            builderMenu.CloseMenu(false);
            placer.DisablePlacementMode(false);
        }

        //if (Input.GetMouseButtonDown(0))
        //{
        //    placer.PlaceSelectedBuilding();
        //}

        if (!Input.GetButton("Alt Camera Move"))
        {
            cameraP.MoveCameraWithKeysOrRotate();
        }
        else if (Input.GetButton("Alt Camera Move"))
        {
            if(hasCameraMoved)
                cameraP.MoveCameraWithMouse();
            else
                hasCameraMoved = cameraP.MoveCameraWithMouse();
        }

        if (Input.GetButtonUp("Alt Camera Move") && !Input.GetMouseButton(0))
        {
            builderMenu.CloseMenu(hasCameraMoved);
            placer.DisablePlacementMode(hasCameraMoved);
            hasCameraMoved = false;
        }
    }
}
