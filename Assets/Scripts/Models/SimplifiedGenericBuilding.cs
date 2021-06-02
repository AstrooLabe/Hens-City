[System.Serializable]
public class SimplifiedGenericBuilding
{
    public int id = 0;

    public int uniqueId = 0;
    public int rotation = 0;
    public float positionX = 0;
    public float positionY = 0;
    public float positionZ = 0;

    public void NewSimplifiedGenericBuilding(GenericBuilding asset)
    {
        id = asset.GetAssetId();
        uniqueId = asset.GetUniqueId();
        rotation = asset.GetRotation();
        positionX = asset.GetPosition().x;
        positionY = asset.GetPosition().y;
        positionZ = asset.GetPosition().z;
    }

}
