using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Texture2D cursorTexture;

    [SerializeField]
    public BuildingListObject buildingsDatabase = new BuildingListObject();
    [SerializeField]
    public UIBuildingListObject uiBuildingsDatabase = new UIBuildingListObject();

    void Start()
    {
        buildingsDatabase = JsonUtility.FromJson<BuildingListObject>(Resources.Load<TextAsset>("JSON_Data/BuildingList").text);
        uiBuildingsDatabase = JsonUtility.FromJson<UIBuildingListObject>(Resources.Load<TextAsset>("JSON_Data/UIBuildingList").text);

    }

    private void Awake()
    {
        //Application.targetFrameRate = 60;
    }
}
