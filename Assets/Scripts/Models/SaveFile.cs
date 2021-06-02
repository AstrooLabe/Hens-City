using System.Collections.Generic;

[System.Serializable]
public class SaveFile
{
    public List<SimplifiedGenericBuilding> assetsList = new List<SimplifiedGenericBuilding>();
    public int[,] statesGrid;
}
