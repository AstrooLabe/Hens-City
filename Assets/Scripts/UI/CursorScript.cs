using UnityEngine;

public class CursorScript : MonoBehaviour
{
    private CustomGrid grid;
    [SerializeField]
    private GameObject cursorPrefab;

    private Vector3 translationVelo = new Vector3(0, 0, 0);

    private void Awake()
    {
        grid = FindObjectOfType<CustomGrid>();
    }

    void Start()
    {
        cursorPrefab = Instantiate(cursorPrefab, new Vector3(0, 0, 0), Quaternion.identity);
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            PlaceCursorNear(hitInfo.point);
        }
    }

    private void PlaceCursorNear(Vector3 clickPoint)
    {
        Vector3 finalPosition = grid.GetNearestPointOnGrid(clickPoint);
        finalPosition = new Vector3(Mathf.Clamp(finalPosition.x, grid.GetOffsetX(), grid.GetOffsetX() * -1), 0, Mathf.Clamp(finalPosition.z, grid.GetOffsetZ(), grid.GetOffsetZ() * -1));
        cursorPrefab.transform.position = Vector3.SmoothDamp(cursorPrefab.transform.position, finalPosition, ref translationVelo, 0.05f);
    }

    public Vector3 GetPosition()
    {
        return cursorPrefab.transform.position;
    }

    public void RotateCursor(int angle)
    {
        cursorPrefab.transform.localRotation = Quaternion.Euler(0, angle, 0);
    }

    public void Enable(int x, int z)
    {
        cursorPrefab.SetActive(true);
        cursorPrefab.GetComponent<CursorAnimation>().SetCursorSizeToAsset(x, z);
    }

    public void Disable()
    {
        cursorPrefab.GetComponent<CursorAnimation>().ResetCursor();
        cursorPrefab.SetActive(false);
    }
}
