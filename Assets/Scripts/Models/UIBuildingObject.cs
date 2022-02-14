using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UIBuildingObject
{
    public int id = 0;

    public int x = 0;
    public int z = 0;
    public string buildingPrefabName = "";
    public string displayedName = "";
    public string description = "";
    public bool specialEvents = false;
    public string category = "";
    public string city = "";
}

[System.Serializable]
public class UIBuildingListObject
{
    public List<UIBuildingObject> uiBuildingList;

    public UIBuildingObject getBuildingById(int id)
    {
        return uiBuildingList.Find(building => building.id == id);
    }

    public List<UIBuildingObject> getBuildingListByCityAndCat(string city, string category)
    {
        List<UIBuildingObject> list = new List<UIBuildingObject>();

        uiBuildingList.ForEach(building =>
        {
            if (building.city == city && building.category == category) list.Add(building);
        });

        return list;
    }
}