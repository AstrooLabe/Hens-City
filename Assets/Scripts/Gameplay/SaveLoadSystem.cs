using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveLoadSystem : MonoBehaviour
{
    private CustomGrid grid;
    private SaveFile save = new SaveFile();
    private GameManager gameManager;

    void Start()
    {
        grid = FindObjectOfType<CustomGrid>();
        gameManager = FindObjectOfType<GameManager>();
        if (PlayerPrefs.GetInt("isLoading") == 1)
        {
            PlayerPrefs.SetInt("isLoading", 0);
            FindObjectOfType<SaveLoadSystem>().LoadGame();
        }
    }

    public void SaveGame()
    {
        List<GenericBuilding> assetsList = grid.GetAssetsList();
        for (int i = 0; i < assetsList.Count; i++)
        {
            SimplifiedGenericBuilding asset = new SimplifiedGenericBuilding();
            asset.NewSimplifiedGenericBuilding(assetsList[i]);
            save.assetsList.Add(asset);
        }
        save.statesGrid = grid.GetStatesGrid();

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/save1.sav");
        bf.Serialize(file, save);
        file.Close();
    }

    public void LoadGame()
    {
        if (File.Exists(Application.persistentDataPath + "/save1.sav"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/save1.sav", FileMode.Open);
            SaveFile save = (SaveFile)bf.Deserialize(file);
            file.Close();

            grid.EmptyList();

            for (int i = 0; i < save.assetsList.Count; i++)
            {
                GenericBuildingObject assetToLoad = gameManager.buildingsDatabase.getBuildingById(save.assetsList[i].id);

                Vector3 position = new Vector3(save.assetsList[i].positionX, save.assetsList[i].positionY, save.assetsList[i].positionZ);

                GameObject generatedBuilding = Resources.Load<GameObject>("Buildings/" + assetToLoad.city + "/" + assetToLoad.category + "/" + assetToLoad.buildingPrefabName);
                generatedBuilding = Instantiate(generatedBuilding, position, Quaternion.identity);
                generatedBuilding.AddComponent<GenericBuilding>();
                generatedBuilding.GetComponent<GenericBuilding>().createGenericBuilding(assetToLoad);
                generatedBuilding.GetComponent<GenericBuilding>().SetUniqueId(save.assetsList[i].uniqueId);
                generatedBuilding.GetComponent<GenericBuilding>().SetRotation(save.assetsList[i].rotation);
                generatedBuilding.transform.Rotate(0f, (float)save.assetsList[i].rotation, 0f, Space.World);
                generatedBuilding.layer = 0;
                generatedBuilding.transform.SetParent(GameObject.Find("BuildingGrid").transform);
                grid.AddToList(generatedBuilding.GetComponent<GenericBuilding>(), 0, 0, true);

            }

            grid.SetStatesGrid(save.statesGrid);
        }
    }
}
