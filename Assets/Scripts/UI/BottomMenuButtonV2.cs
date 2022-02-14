using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BottomMenuButtonV2 : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private GenericBuildingObject assignedBuilding;
    private UIBuildingObject uiAssignedBuilding;

    [SerializeField]
    private GameObject displayedBuilding;
    [SerializeField]
    private Image buttonBackground;
    private GameObject infoPanel;

    void Start()
    {
        buttonBackground.color = new Color(1, 1, 1, 0);
    }

    void Update()
    {
        if (displayedBuilding != null)
            displayedBuilding.transform.Rotate(0, 30f * Time.deltaTime, 0);
    }

    public void SetInfoPanel(GameObject infoPanel)
    {
        this.infoPanel = infoPanel;
    }

    public void SetAssignedBuilding(GenericBuildingObject newAssignedBuilding)
    {
        assignedBuilding = newAssignedBuilding;
        uiAssignedBuilding = FindObjectOfType<GameManager>().uiBuildingsDatabase.getBuildingById(newAssignedBuilding.id);
        SetDisplayedBuilding();
    }

    private void SetDisplayedBuilding()
    {
        displayedBuilding = Resources.Load<GameObject>("UI/" + assignedBuilding.city + "/" + assignedBuilding.category + "/" + assignedBuilding.buildingPrefabName + "_UI");
        Vector3 finalScale = displayedBuilding.transform.localScale;
        Vector3 finalPosition = displayedBuilding.transform.localPosition;
        Quaternion rotation = displayedBuilding.transform.localRotation;
        displayedBuilding = Instantiate(displayedBuilding, Vector3.zero, Quaternion.identity);
        displayedBuilding.transform.SetParent(transform);
        displayedBuilding.transform.localPosition = finalPosition;
        displayedBuilding.transform.localScale = finalScale;
        displayedBuilding.transform.localRotation = rotation;
    }

    public void SendBuildingToPlacer()
    {
        FindObjectOfType<AssetPlacer>().SetBuilding(assignedBuilding);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        FindObjectOfType<InputsManager>().isCursorOnButton = true;
        buttonBackground.color = new Color(1, 1, 1, 1);
        this.DisplayInfoPanel();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        FindObjectOfType<InputsManager>().isCursorOnButton = false;
        buttonBackground.color = new Color(1, 1, 1, 0);
        EmptyAndHideInfoPanel();
    }

    private void DisplayInfoPanel()
    {
        infoPanel.SetActive(true);
        infoPanel.GetComponent<InformationPanel>().SetDisplayedInformations(uiAssignedBuilding);
    }

    private void EmptyAndHideInfoPanel()
    {
        infoPanel.GetComponent<InformationPanel>().EmptyDisplayedInformations();
        infoPanel.SetActive(false);
    }
}
