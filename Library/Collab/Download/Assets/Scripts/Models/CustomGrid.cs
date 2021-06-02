using System.Collections.Generic;
using UnityEngine;

public class CustomGrid : MonoBehaviour
{
    [SerializeField]
    private float pointEspacement = 4;
    [SerializeField]
    private int gridSizeX = 50;
    [SerializeField]
    private int gridSizeZ = 50;

    [SerializeField]
    private GameObject highlight;

    [SerializeField]
    private GameObject baseTilePrefab;

    private int offsetX;
    private int offsetZ;

    private int[,] statesGrid;
    [SerializeField]
    private List<GenericBuilding> assetsList;

    private GameObject[,] tiles;

    private Mesh mesh;

    private void Awake()
    {
        offsetX = gridSizeX * (int)pointEspacement / -2;
        offsetZ = gridSizeZ * (int)pointEspacement / -2;

        Generate();
    }

    public void Start()
    {
        assetsList = new List<GenericBuilding>();
        statesGrid = new int[gridSizeX + 1, gridSizeX + 1];
        int x = offsetX;
        for (int i = 0; i <= gridSizeX; i++)
        {
            int z = offsetZ;
            for (int j = 0; j <= gridSizeZ; j++)
            {
                statesGrid[i, j] = 0;

                z += (int)pointEspacement;
            }
            x += (int)pointEspacement;
        }
    }

    public Vector3 GetNearestPointOnGrid(Vector3 position)
    {
        position -= transform.position;

        int xCount = Mathf.RoundToInt(position.x / pointEspacement);
        int yCount = Mathf.RoundToInt(position.y / pointEspacement);
        int zCount = Mathf.RoundToInt(position.z / pointEspacement);

        Vector3 result = new Vector3(
            (float)xCount * pointEspacement,
            (float)yCount * pointEspacement,
            (float)zCount * pointEspacement);

        result += transform.position;

        return result;
    }

    private void rawGizmos()
    {
        Gizmos.color = Color.white;
        offsetX = gridSizeX * (int)pointEspacement / -2;
        offsetZ = gridSizeZ * (int)pointEspacement / -2;
        int x = offsetX;
        for (int i = 0; i <= gridSizeX; i += 1)
        {
            int z = offsetZ;
            for (int j = 0; j <= gridSizeZ; j += 1)
            {
                Gizmos.DrawSphere(new Vector3(x, 0f, z), pointEspacement / 10);
                z += (int)pointEspacement;
            }
            x += (int)pointEspacement;
        }
    }

    public int GetOffsetX()
    {
        return offsetX;
    }

    public int GetOffsetZ()
    {
        return offsetZ;
    }

    public int GetPointEspacement()
    {
        return (int)pointEspacement;
    }

    public int GetGridSize()
    {
        return (int)gridSizeX;
    }

    public void AddToList(GenericBuilding assetToAdd, int indexX, int indexZ, bool isLoading)
    {
        if (!isLoading)
        {
            if (assetsList.Count > 0)
                assetToAdd.SetUniqueId(assetsList[assetsList.Count - 1].GetUniqueId() + 1);
            else
                assetToAdd.SetUniqueId(1);
            SetRange(indexX, indexZ, assetToAdd.GetX(), assetToAdd.GetZ(), assetToAdd.GetRotation(), assetToAdd.GetUniqueId());
        }
        assetsList.Add(assetToAdd);
    }

    public void DeleteFromList(GenericBuilding assetToDelete)
    {
        SetRange((int)(assetToDelete.gameObject.transform.position.x / pointEspacement + gridSizeX / 2), (int)(assetToDelete.gameObject.transform.position.z / pointEspacement + gridSizeX / 2), assetToDelete.GetX(), assetToDelete.GetZ(), assetToDelete.GetRotation(), 0);
        assetsList.Remove(assetToDelete);
        Destroy(assetToDelete.gameObject);
    }

    public void EmptyList()
    {
        int iterations = assetsList.Count;
        for (int i = 0; i < iterations; i++)
        {
            Destroy(assetsList[0].gameObject);
            assetsList.Remove(assetsList[0]);
        }
    }

    public List<GenericBuilding> GetAssetsList()
    {
        return assetsList;
    }

    public int[,] GetStatesGrid()
    {
        return statesGrid;
    }

    public void SetStatesGrid(int[,] newStatesGrid)
    {
        statesGrid = newStatesGrid;
    }

    private Coordinates GetCoordinatesFromPosAndDir(int posX, int posZ, int dirX, int dirZ, int rotation)
    {
        Coordinates coordinates = new Coordinates
        {
            startPositionX = 0,
            startPositionZ = 0,
            endPositionX = 0,
            endPositionZ = 0
        };

        switch (rotation)
        {
            case 0:
                coordinates.startPositionX = posX;
                coordinates.startPositionZ = posZ;
                coordinates.endPositionX = coordinates.startPositionX + dirX;
                coordinates.endPositionZ = coordinates.startPositionZ + dirZ;
                break;

            case 90:
                posZ++;
                coordinates.startPositionX = posX;
                coordinates.startPositionZ = posZ + (dirX) * -1;
                coordinates.endPositionX = coordinates.startPositionX + dirZ;
                coordinates.endPositionZ = posZ;
                break;

            case 180:
                posX++;
                posZ++;
                coordinates.startPositionX = posX + (dirX) * -1;
                coordinates.startPositionZ = posZ + (dirZ) * -1;
                coordinates.endPositionX = posX;
                coordinates.endPositionZ = posZ;
                break;

            case 270:
                posX++;
                coordinates.startPositionX = posX + (dirZ) * -1;
                coordinates.startPositionZ = posZ;
                coordinates.endPositionX = posX;
                coordinates.endPositionZ = coordinates.startPositionZ + dirX;
                break;

            default:
                break;
        }

        if (Input.GetKeyDown("p"))
        {
            Debug.Log("P");
        }

        return coordinates;
    }

    public bool IsRangeConstructible(int posX, int posZ, int dirX, int dirZ, int rotation)
    {
        bool isConstructible = true;

        Coordinates coordinates = GetCoordinatesFromPosAndDir(posX, posZ, dirX, dirZ, rotation);

        for (int x = coordinates.startPositionX; x < coordinates.endPositionX; x++)
        {
            for (int z = coordinates.startPositionZ; z < coordinates.endPositionZ; z++)
            {
                if (x > gridSizeX || x < 0 || z > gridSizeZ || z < 0)
                {
                    isConstructible = false;
                }
                else if (statesGrid[x, z] != 0)
                {
                    isConstructible = false;
                }
            }
        }

        return isConstructible;
    }

    public bool IsCaseConstructible(int posX, int posZ)
    {
        if (statesGrid[posX, posZ] != 0)
            return false;
        return true;
    }

    public void SetRange(int posX, int posZ, int dirX, int dirZ, int rotation, int newState)
    {
        Coordinates coordinates = GetCoordinatesFromPosAndDir(posX, posZ, dirX, dirZ, rotation);

        for (int x = coordinates.startPositionX; x < coordinates.endPositionX; x++)
        {
            for (int z = coordinates.startPositionZ; z < coordinates.endPositionZ; z++)
            {
                statesGrid[x, z] = newState;
            }
        }
    }

    public void SetCase(int posX, int posZ, int newState)
    {
        statesGrid[posX, posZ] = newState;
    }

    private void Generate()
    {
        //GameObject tilePrefab = Resources.Load<GameObject>("Terrain/TilePrefab");
        tiles = new GameObject[gridSizeX+1, gridSizeZ+1];
        for (int x = 0; x <= gridSizeX; x++)
        {
            for (int z = 0; z <= gridSizeZ; z++)
            {
                Vector3 position = new Vector3((x * pointEspacement) + offsetX, 0, (z * pointEspacement) + offsetZ);
                tiles[x, z] = Instantiate<GameObject>(baseTilePrefab, position, Quaternion.identity, transform);
            }
        }
    }

    public struct Coordinates
    {
        public int startPositionX;
        public int endPositionX;
        public int startPositionZ;
        public int endPositionZ;
    }
}
