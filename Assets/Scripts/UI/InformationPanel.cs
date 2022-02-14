using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class InformationPanel : MonoBehaviour
{
    [SerializeField]
    private Text displayedName;
    [SerializeField]
    private TMP_Text description;
    [SerializeField]
    private Text size;
    [SerializeField]
    private GameObject gridSize;

    private GameObject displayedBuilding;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (displayedBuilding != null)
            displayedBuilding.transform.Rotate(0, 30f * Time.deltaTime, 0);
    }

    public void SetDisplayedInformations(UIBuildingObject buildingToDisplay)
    {
        displayedName.text = buildingToDisplay.displayedName;
        description.text = buildingToDisplay.description;
        size.text = buildingToDisplay.x + " x " + buildingToDisplay.z;
        gridSize.transform.localScale = new Vector3(buildingToDisplay.x, buildingToDisplay.z, 1);
        SetDisplayedBuilding(buildingToDisplay);
    }

    public void EmptyDisplayedInformations()
    {
        displayedName.text = "";
        description.text = "";
        size.text = "";
        DestroyImmediate(displayedBuilding, true);
        gridSize.transform.localScale = Vector3.one;
    }

    private void SetDisplayedBuilding(UIBuildingObject buildingToDisplay)
    {
        displayedBuilding = Resources.Load<GameObject>("UI/" + buildingToDisplay.city + "/" + buildingToDisplay.category + "/" + buildingToDisplay.buildingPrefabName);
        Vector3 finalScale = displayedBuilding.transform.localScale;
        Vector3 finalPosition = displayedBuilding.transform.localPosition;
        Quaternion rotation = displayedBuilding.transform.localRotation;
        displayedBuilding = Instantiate(displayedBuilding, Vector3.zero, Quaternion.identity);
        displayedBuilding.transform.SetParent(transform);
        displayedBuilding.transform.localPosition = new Vector3(-125, finalPosition.y * 2, finalPosition.z);
        displayedBuilding.transform.localScale = new Vector3(finalScale.x * 2, finalScale.y * 2, finalScale.z * 2);
        displayedBuilding.transform.localRotation = rotation;
    }
}
