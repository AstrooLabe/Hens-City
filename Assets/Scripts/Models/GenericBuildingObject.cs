using System.Collections.Generic;

[System.Serializable]
public class GenericBuildingObject
{

    public int id = 0;

    public int x = 0;
    public float y = 0;
    public int z = 0;
    public string buildingName = "";
    public string buildingPrefabName = "";
    public string category = "";
    public string city = "";
}

[System.Serializable]
public class BuildingListObject
{
    public List<GenericBuildingObject> buildingList;

    public GenericBuildingObject getBuildingById(int id)
    {
        return buildingList.Find(building => building.id == id);
    }

    public List<GenericBuildingObject> getBuildingListByCityAndCat(string city, string category)
    {
        List<GenericBuildingObject> list = new List<GenericBuildingObject>();

        buildingList.ForEach(building =>
        {
            if (building.city == city && building.category == category) list.Add(building);
        });

        return list;
    }
}
