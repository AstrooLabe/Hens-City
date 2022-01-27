using UnityEngine;

public class GenericBuilding : MonoBehaviour
{
    [SerializeField]
    private int id;
    [SerializeField]
    private int x;
    [SerializeField]
    private float y;
    [SerializeField]
    private int z;
    [SerializeField]
    private string buildingName = "";

    private string buildingType = "";

    [SerializeField]
    private Outline outline;

    private int uniqueId = 0;
    private Transform buildingModel;
    private int rotation = 0;

    public void createGenericBuilding(GenericBuildingObject genericBuildingObject)
    {
        id = genericBuildingObject.id;
        x = genericBuildingObject.x;
        y = genericBuildingObject.y;
        z = genericBuildingObject.z;
        buildingName = genericBuildingObject.buildingName;
        buildingType = genericBuildingObject.category;
        buildingModel = transform.GetChild(0);

        outline = transform.GetComponent<Outline>();
    }

    public string GetBuildingName()
    {
        return buildingName;
    }

    public string GetBuildingType()
    {
        return buildingType;
    }

    public int GetX()
    {
        return x;
    }

    public float GetY()
    {
        return y;
    }

    public int GetZ()
    {
        return z;
    }

    public int GetAssetId()
    {
        return id;
    }

    public int GetUniqueId()
    {
        return uniqueId;
    }

    public void SetUniqueId(int uniqueId)
    {
        this.uniqueId = uniqueId;
    }

    public int GetRotation()
    {
        return rotation;
    }

    public void SetRotation(int rotation)
    {
        this.rotation = rotation;
    }

    public int GetModelRotation()
    {
        return (int)buildingModel.localRotation.eulerAngles.y;
    }

    public void SetModelRotation(int rotation)
    {
        buildingModel.localRotation = Quaternion.Euler(0, rotation, 0);
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    private void OnMouseEnter()
    {
        if (!FindObjectOfType<PauseMenu>().IsGamePaused() && uniqueId != 0)
        {
            if (!outline.enabled) outline.enabled = true;
            if (FindObjectOfType<DeleteAsset>().isDeleteMode())
            {
                outline.OutlineColor = new Color(1, 0, 0, 81f / 255f);
            }
            else
            {
                outline.OutlineColor = new Color(1, 1, 1, 81f / 255f);
            }
        }
    }

    private void OnMouseExit()
    {
        if (outline != null)
            if (outline.enabled) outline.enabled = false;
    }

    public void SetHighlightWhite()
    {
        if (!outline.enabled) outline.enabled = true;
        outline.OutlineColor = new Color(1, 1, 1, 81f / 255f);
    }

    public void SetHighlightOrange()
    {
        if (!outline.enabled) outline.enabled = true;
        outline.OutlineColor = new Color(1, 102f / 255f, 0, 81f / 255f);
    }

    public void SetHighlightOff()
    {
        outline.enabled = false;
    }
}
