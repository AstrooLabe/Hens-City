using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BottomMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject buttonPrefab;
    [SerializeField]
    private GameObject infoPanel;
    private string category = "";
    private string city = Cities.SULIMO;
    private List<GenericBuildingObject> objects = new List<GenericBuildingObject>();
    private GameObject[] buttons = new GameObject[0];
    [SerializeField]
    private GameManager gameManager;

    private int positionInArray = 0;

    void Start()
    {
    }

    public void SetCity(string newCity)
    {
        city = newCity;
    }

    public void SetCategory(string newCategory)
    {
        if (newCategory != category)
        {
            category = newCategory;
            positionInArray = 0;
            PopulateObjects(true);
        }
    }

    private void PopulateObjects(bool catchange)
    {
        if (buttons.Length > 0)
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                Destroy(buttons[i].gameObject);
            }
        }
        objects = gameManager.buildingsDatabase.getBuildingListByCityAndCat(city, category);

        int nbButtons = objects.Count > 4 ? 4 : objects.Count;

        buttons = new GameObject[nbButtons];

        for (int i = 0; i < nbButtons; i++)
        {
            buttons[i] = Instantiate(buttonPrefab);
            buttons[i].transform.SetParent(transform, false);
            buttons[i].transform.localPosition = new Vector3(-120 + (80 * i), -13.5f);
            string imgPath = "Buildings/" + city + "/" + category + "/Images/" + objects[positionInArray + i].buildingPrefabName;
            buttons[i].SendMessage("SetAssignedBuilding", objects[positionInArray + i]);
            buttons[i].SendMessage("SetInfoPanel", infoPanel);
        }
    }

    public void ScrollUp()
    {
        if (objects.Count > 4)
        {
            if (positionInArray - 4 >= 0)
            {
                positionInArray -= 4;
            }
            else
            {
                positionInArray = 0;
            }
            PopulateObjects(false);
        }
    }

    public void ScrollDown()
    {
        if (objects.Count > 4)
        {
            if (positionInArray + 8 <= objects.Count - 1)
            {
                positionInArray += 4;
            }
            else
            {
                positionInArray = objects.Count >= 4 ? objects.Count - 4 : 0;
            }
            PopulateObjects(false);
        }
    }

    public void CloseMenu()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            Destroy(buttons[i].gameObject);
        }
        objects = new List<GenericBuildingObject>();
        buttons = new GameObject[0];
        category = "";
    }
}
