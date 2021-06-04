using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class AssetPlacer : MonoBehaviour
{
    private CustomGrid grid;
    private GameObject selectedBuilding;
    private GameObject instantiatedBuilding;
    private Vector3 buildingPositionOffset;
    [SerializeField]
    private GameObject buildingGrid;
    private bool isABuildingSelected = false;

    private Vector3 translationVelo = new Vector3(0, 0, 0);

    private void Awake()
    {
        grid = FindObjectOfType<CustomGrid>();
    }

    private void RotateSelectedBuildingClockwise()
    {
        int objectRotation = (int)instantiatedBuilding.transform.localRotation.eulerAngles.y;
        Vector3 newOffsetVector = Vector3.zero;

        switch (objectRotation)
        {
            case 0:
                newOffsetVector = new Vector3(0, 0, instantiatedBuilding.GetComponent<GenericBuilding>().GetX() * grid.GetPointEspacement() - grid.GetPointEspacement());
                objectRotation = 90;
                break;
            case 90:
                newOffsetVector = new Vector3(instantiatedBuilding.GetComponent<GenericBuilding>().GetX() * grid.GetPointEspacement() - grid.GetPointEspacement(), 0, selectedBuilding.GetComponent<GenericBuilding>().GetZ() * grid.GetPointEspacement() - grid.GetPointEspacement());
                objectRotation = 180;
                break;
            case 180:
                newOffsetVector = new Vector3(instantiatedBuilding.GetComponent<GenericBuilding>().GetZ() * grid.GetPointEspacement() - grid.GetPointEspacement(), 0, 0);
                objectRotation = 270;
                break;
            case 270:
                newOffsetVector = Vector3.zero;
                objectRotation = 0;
                break;
        }

        buildingPositionOffset = newOffsetVector;
        instantiatedBuilding.transform.localRotation = Quaternion.Euler(0, objectRotation, 0);
        instantiatedBuilding.GetComponent<GenericBuilding>().SetRotation(objectRotation);
        instantiatedBuilding.transform.localPosition = FindObjectOfType<CursorScript>().GetPosition() + buildingPositionOffset;
    }

    public void DisablePlacementMode(bool hasCameraMoved)
    {
        if (isABuildingSelected && !hasCameraMoved)
        {
            DestroyImmediate(instantiatedBuilding.gameObject, true);
            buildingPositionOffset = Vector3.zero;
            isABuildingSelected = false;
            FindObjectOfType<CursorScript>().Disable();
        }
    }

    IEnumerator PlaceInstantiatedBuilding()
    {
        while (isABuildingSelected)
        {
            if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                Physics.Raycast(ray, out RaycastHit hitInfo);

                GenericBuilding genericSelectedBuilding = instantiatedBuilding.GetComponent<GenericBuilding>();
                int gameObjectRotation = (int)instantiatedBuilding.transform.localRotation.eulerAngles.y;
                Vector3 finalPosition = grid.GetNearestPointOnGrid(hitInfo.point);
                finalPosition = new Vector3(finalPosition.x, 0, finalPosition.z) + buildingPositionOffset;
                int indexPositionX = CalculateIndexOfGridPosition((int)finalPosition.x);
                int indexPositionZ = CalculateIndexOfGridPosition((int)finalPosition.z);

                if (grid.IsRangeConstructible(indexPositionX, indexPositionZ, genericSelectedBuilding.GetX(), genericSelectedBuilding.GetZ(), gameObjectRotation))
                {
                    GameObject newBuilding = Instantiate(selectedBuilding, Vector3.zero, Quaternion.identity);
                    newBuilding.transform.localRotation = Quaternion.Euler(0, gameObjectRotation, 0);
                    newBuilding.GetComponent<GenericBuilding>().SetRotation(instantiatedBuilding.GetComponent<GenericBuilding>().GetRotation());
                    newBuilding.transform.SetParent(buildingGrid.transform);
                    newBuilding.layer = 0;
                    newBuilding.transform.position = finalPosition;
                    grid.AddToList(newBuilding.GetComponent<GenericBuilding>(), indexPositionX, indexPositionZ, false);
                }
            }

            if (Input.GetButtonUp("Rotate Clockwise"))
            {
                RotateSelectedBuildingClockwise();
            }

            yield return null;
        }
    }

    IEnumerator PlaceInstantiatedRoad()
    {

        while (isABuildingSelected)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out RaycastHit hitInfo);

            Vector3 firstTile = grid.GetNearestPointOnGrid(hitInfo.point);
            firstTile = new Vector3(Mathf.Clamp(firstTile.x, grid.GetOffsetX(), grid.GetOffsetX() * -1), instantiatedBuilding.transform.position.y, Mathf.Clamp(firstTile.z, grid.GetOffsetZ(), grid.GetOffsetZ() * -1)) + buildingPositionOffset;

            instantiatedBuilding.transform.position = Vector3.SmoothDamp(instantiatedBuilding.transform.position, firstTile, ref translationVelo, 0.05f);

            if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                List<GameObject> roadsToDisplay = new List<GameObject>();
                Vector3 delta = Vector3.zero;

                bool isConstructible = false;
                bool cancel = false;

                while (Input.GetMouseButton(0) && !cancel)
                {
                    Ray ray2 = Camera.main.ScreenPointToRay(Input.mousePosition);
                    Physics.Raycast(ray2, out RaycastHit hitInfo2);

                    Vector3 lastTile = grid.GetNearestPointOnGrid(hitInfo2.point);

                    lastTile = new Vector3(Mathf.Clamp(lastTile.x, grid.GetOffsetX(), grid.GetOffsetX() * -1), instantiatedBuilding.transform.position.y, Mathf.Clamp(lastTile.z, grid.GetOffsetZ(), grid.GetOffsetZ() * -1)) + buildingPositionOffset;
                    instantiatedBuilding.transform.position = lastTile;

                    if (lastTile == firstTile)
                    {
                        roadsToDisplay.ForEach(road => { DestroyImmediate(road); });
                        roadsToDisplay = new List<GameObject>();

                        if (!grid.IsCaseConstructible(CalculateIndexOfGridPosition((int)instantiatedBuilding.transform.position.x), CalculateIndexOfGridPosition((int)instantiatedBuilding.transform.position.z)))
                            instantiatedBuilding.GetComponent<GenericBuilding>().SetHighlightOrange();
                        else
                        {
                            roadsToDisplay.Add(Instantiate(selectedBuilding, new Vector3(firstTile.x, firstTile.y, firstTile.z), Quaternion.identity));
                            isConstructible = true;
                        }
                    }
                    else if (delta != lastTile - firstTile)
                    {
                        delta = lastTile - firstTile;
                        roadsToDisplay.ForEach(road => { DestroyImmediate(road); });
                        roadsToDisplay = new List<GameObject>();

                        if (delta.x > 0)
                        {
                            for (int x = (int)firstTile.x; x <= firstTile.x + delta.x; x += 4)
                            {
                                roadsToDisplay.Add(Instantiate(instantiatedBuilding, new Vector3(x, firstTile.y, firstTile.z), Quaternion.identity));
                            }
                        }
                        else if (delta.x < 0)
                        {
                            for (int x = (int)firstTile.x; x >= firstTile.x + delta.x; x -= 4)
                            {
                                roadsToDisplay.Add(Instantiate(instantiatedBuilding, new Vector3(x, firstTile.y, firstTile.z), Quaternion.identity));
                            }
                        }

                        if (delta.z > 0)
                        {
                            for (int z = (int)firstTile.z; z <= firstTile.z + delta.z; z += 4)
                            {
                                roadsToDisplay.Add(Instantiate(instantiatedBuilding, new Vector3(lastTile.x, firstTile.y, z), Quaternion.identity));
                            }
                        }
                        else if (delta.z < 0)
                        {
                            for (int z = (int)firstTile.z; z >= firstTile.z + delta.z; z -= 4)
                            {
                                roadsToDisplay.Add(Instantiate(instantiatedBuilding, new Vector3(lastTile.x, firstTile.y, z), Quaternion.identity));
                            }
                        }
                        RemoveDuplicates(ref roadsToDisplay);
                        isConstructible = IsRoadConstructible(ref roadsToDisplay);
                    }

                    if (Input.GetButtonUp("Cancel Road"))
                    {
                        cancel = true;
                    }

                    yield return null;
                }

                if (isConstructible && !cancel)
                {
                    roadsToDisplay.ForEach(road =>
                    {
                        GameObject newRoad = Instantiate(selectedBuilding, Vector3.zero, Quaternion.identity);
                        newRoad.transform.SetParent(buildingGrid.transform);
                        newRoad.layer = 0;
                        newRoad.transform.position = road.transform.position;
                        int indexPositionX = CalculateIndexOfGridPosition((int)road.transform.position.x);
                        int indexPositionZ = CalculateIndexOfGridPosition((int)road.transform.position.z);
                        grid.AddToList(newRoad.GetComponent<GenericBuilding>(), indexPositionX, indexPositionZ, false);
                    });
                }

                instantiatedBuilding.GetComponent<GenericBuilding>().SetHighlightWhite();
                roadsToDisplay.ForEach(road => { DestroyImmediate(road); });

                //GenericBuilding genericSelectedBuilding = selectedBuilding.GetComponent<GenericBuilding>();
                //int gameObjectRotation = (int)selectedBuilding.transform.localRotation.eulerAngles.y;
                //Vector3 finalPosition = grid.GetNearestPointOnGrid(hitInfo.point);
                //finalPosition = new Vector3(finalPosition.x, 0, finalPosition.z) + buildingPositionOffset;
                //int indexPositionX = CalculateIndexOfGridPosition((int)finalPosition.x);
                //int indexPositionZ = CalculateIndexOfGridPosition((int)finalPosition.z);

                //if (grid.IsRangeConstructible(indexPositionX, indexPositionZ, genericSelectedBuilding.GetX(), genericSelectedBuilding.GetZ(), gameObjectRotation))
                //{
                //    selectedBuilding.GetComponent<GenericBuilding>().SetHighlightOff();
                //    GameObject newBuilding = Instantiate(selectedBuilding, Vector3.zero, Quaternion.identity);
                //    newBuilding.transform.localRotation = Quaternion.Euler(0, gameObjectRotation, 0);
                //    newBuilding.GetComponent<GenericBuilding>().SetRotation(selectedBuilding.GetComponent<GenericBuilding>().GetRotation());
                //    newBuilding.transform.SetParent(buildingGrid.transform);
                //    newBuilding.layer = 0;
                //    newBuilding.transform.position = finalPosition;
                //    grid.AddToList(newBuilding.GetComponent<GenericBuilding>(), indexPositionX, indexPositionZ, false);
                //}
            }

            //if (Input.GetButtonUp("Rotate Clockwise"))
            //{
            //    RotateSelectedBuildingClockwise();
            //}

            yield return null;
        }
    }

    private void RemoveDuplicates(ref List<GameObject> roadTilesList)
    {
        for (int x = 0; x < roadTilesList.Count; x++)
        {
            GameObject roadToTest = roadTilesList[x];
            List<GameObject> duplicates = roadTilesList.FindAll(road =>
            {
                if (road.transform.position.x == roadToTest.transform.position.x && road.transform.position.z == roadToTest.transform.position.z)
                    return true;
                return false;
            });

            if (duplicates.Count > 1)
            {
                int index = roadTilesList.FindIndex(road => { return road == duplicates[1]; });
                DestroyImmediate(roadTilesList[index], true);
                roadTilesList.RemoveAt(index);
            }
        }
    }

    private bool IsRoadConstructible(ref List<GameObject> roadTilesList)
    {
        bool isBlocked = false;
        roadTilesList.ForEach(road =>
        {
            if (!grid.IsCaseConstructible(CalculateIndexOfGridPosition((int)road.transform.position.x), CalculateIndexOfGridPosition((int)road.transform.position.z)))
                isBlocked = true;
        });

        if (isBlocked)
        {
            roadTilesList.ForEach(road =>
            {
                road.GetComponent<GenericBuilding>().SetHighlightOrange();
            });
            instantiatedBuilding.GetComponent<GenericBuilding>().SetHighlightOrange();

            return false;
        }
        else
        {
            roadTilesList.ForEach(road =>
            {
                road.GetComponent<GenericBuilding>().SetHighlightWhite();
            });
            instantiatedBuilding.GetComponent<GenericBuilding>().SetHighlightWhite();

            return true;
        }

    }

    private int CalculateIndexOfGridPosition(int position)
    {
        return position / grid.GetPointEspacement() + grid.GetGridSize() / 2;
    }

    private IEnumerator PlaceDisplayedBuildingNear()
    {
        while (isABuildingSelected)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out RaycastHit hitInfo);

            Vector3 finalPosition = grid.GetNearestPointOnGrid(hitInfo.point);
            finalPosition = new Vector3(Mathf.Clamp(finalPosition.x, grid.GetOffsetX(), grid.GetOffsetX() * -1), instantiatedBuilding.transform.position.y, Mathf.Clamp(finalPosition.z, grid.GetOffsetZ(), grid.GetOffsetZ() * -1)) + buildingPositionOffset;

            instantiatedBuilding.transform.position = Vector3.SmoothDamp(instantiatedBuilding.transform.position, finalPosition, ref translationVelo, 0.05f);

            int indexPositionX = CalculateIndexOfGridPosition((int)finalPosition.x);
            int indexPositionZ = CalculateIndexOfGridPosition((int)finalPosition.z);
            if (!grid.IsRangeConstructible(indexPositionX, indexPositionZ, instantiatedBuilding.GetComponent<GenericBuilding>().GetX(), instantiatedBuilding.GetComponent<GenericBuilding>().GetZ(), (int)instantiatedBuilding.transform.localRotation.eulerAngles.y))
            {
                instantiatedBuilding.GetComponent<GenericBuilding>().SetHighlightOrange();
            }
            else
            {
                instantiatedBuilding.GetComponent<GenericBuilding>().SetHighlightWhite();
            }
            yield return null;
        }
    }

    public void SetBuilding(GenericBuildingObject newBuilding)
    {
        if (selectedBuilding)
        {
            if (selectedBuilding.GetComponent<GenericBuilding>().GetBuildingType() == Categories.ROADS)
            {
                StopCoroutine("PlaceInstantiatedRoad");
            }
            else
            {
                StopCoroutine("PlaceDisplayedBuildingNear");
                StopCoroutine("PlaceInstantiatedBuilding");
            }
            DestroyImmediate(instantiatedBuilding.gameObject, true);
        }
        buildingPositionOffset = Vector3.zero;
        CursorScript cursor = FindObjectOfType<CursorScript>();

        selectedBuilding = Resources.Load<GameObject>("Buildings/" + newBuilding.city + "/" + newBuilding.category + "/" + newBuilding.buildingPrefabName);
        instantiatedBuilding = Instantiate(selectedBuilding, Vector3.zero, Quaternion.identity);
        selectedBuilding.AddComponent<GenericBuilding>();
        selectedBuilding.GetComponent<GenericBuilding>().createGenericBuilding(newBuilding);

        instantiatedBuilding.AddComponent<GenericBuilding>();
        instantiatedBuilding.GetComponent<GenericBuilding>().createGenericBuilding(newBuilding);

        instantiatedBuilding.gameObject.layer = 2;
        instantiatedBuilding.GetComponent<GenericBuilding>().SetHighlightWhite();

        if (cursor)
        {
            cursor.Enable();
            Vector3 finalPosition = new Vector3(cursor.GetPosition().x, 0, cursor.GetPosition().z);
            instantiatedBuilding.transform.position = finalPosition;
        }
        isABuildingSelected = true;

        if (selectedBuilding.GetComponent<GenericBuilding>().GetBuildingType() == Categories.ROADS)
        {
            StartCoroutine("PlaceInstantiatedRoad");
        }
        else
        {
            StartCoroutine("PlaceDisplayedBuildingNear");
            StartCoroutine("PlaceInstantiatedBuilding");
        }
    }
}