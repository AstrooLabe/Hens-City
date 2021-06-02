using UnityEngine;

public class BuilderMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject bottomMenu;
    [SerializeField]
    private Transform categories;
    private bool isMenuSelected = false;
    private string selectedButton = "";
    private DeleteAsset deleteAssetComponent;

    private void Start()
    {
        deleteAssetComponent = FindObjectOfType<DeleteAsset>();
    }

    public void CloseMenu(bool hasCameraMoved)
    {
        if (isMenuSelected && !hasCameraMoved && !deleteAssetComponent.isDeleteMode())
        {
            bottomMenu.SendMessage("CloseMenu");
            SetBottomMenuInactive();
            selectedButton = "";
        }
        else if(deleteAssetComponent.isDeleteMode())
        {
            SetBottomMenuInactive();
            selectedButton = "";
            deleteAssetComponent.ExitDeleteMode();
        }
    }

    public void OpenHousing()
    {
        SetBottomMenuActive();
        bottomMenu.SendMessage("SetCategory", Categories.HOUSING);
    }

    public void OpenShops()
    {
        SetBottomMenuActive();
        bottomMenu.SendMessage("SetCategory", Categories.SHOPS);
    }

    public void OpenRoads()
    {
        SetBottomMenuActive();
        bottomMenu.SendMessage("SetCategory", Categories.ROADS);
    }

    public void OpenSpecialBuildings()
    {
        SetBottomMenuActive();
        bottomMenu.SendMessage("SetCategory", Categories.SPECIAL_BUILDINGS);
    }

    public void OpenVegetation()
    {
        SetBottomMenuActive();
        bottomMenu.SendMessage("SetCategory", Categories.VEGETATION);
    }

    public void OpenTerraforming()
    {

    }

    private void SetBottomMenuActive()
    {
        FindObjectOfType<DeleteAsset>().ExitDeleteMode();
        bottomMenu.SetActive(true);
        isMenuSelected = true;
    }

    public void EnterDeleteMode()
    {
        FindObjectOfType<AssetPlacer>().DisablePlacementMode(false);
        CloseMenu(false);
        isMenuSelected = true;
        deleteAssetComponent.EnterDeleteMode();
    }

    private void SetBottomMenuInactive()
    {
        bottomMenu.SetActive(false);
        isMenuSelected = false;
    }

    public void SetSelectedButton(string button)
    {
        if (selectedButton != "")
        if (deleteAssetComponent.isDeleteMode() && button != "DeleteButton")
            deleteAssetComponent.ExitDeleteMode();
        selectedButton = button;
    }
}
