using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Texture2D cursorTexture;

    [SerializeField]
    public BuildingListObject buildingsDatabase = new BuildingListObject();

    void Start()
    {
        buildingsDatabase = JsonUtility.FromJson<BuildingListObject>(Resources.Load<TextAsset>("JSON_Data/BuildingList").text);

    }

    private void Awake()
    {
        //Application.targetFrameRate = 60;
    }
}
