using UnityEngine;

public class BottomMenuButton : MonoBehaviour
{
    private GenericBuildingObject assignedBuilding;

    public void SetAssignedBuilding(GenericBuildingObject newAssignedBuilding)
    {
        assignedBuilding = newAssignedBuilding;
    }

    public void SendBuildingToPlacer()
    {
        FindObjectOfType<AssetPlacer>().SetBuilding(assignedBuilding);
    }
}
