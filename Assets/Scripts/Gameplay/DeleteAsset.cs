using UnityEngine;

public class DeleteAsset : MonoBehaviour
{
    private bool isDeleting = false;
    private GenericBuilding targetedBuilding = null;

    void Update()
    {
        if (isDeleting)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hitInfo))
            {
                if (hitInfo.collider.tag == "PlacedAsset")
                {
                    targetedBuilding = hitInfo.collider.GetComponentInParent<GenericBuilding>();
                }
                else
                {
                    targetedBuilding = null;
                }
            }

            if (targetedBuilding != null && Input.GetMouseButtonDown(0) && !FindObjectOfType<InputsManager>().isCursorOnButton)
            {
                FindObjectOfType<CustomGrid>().DeleteFromList(targetedBuilding);
            }

        }
    }

    public void EnterDeleteMode()
    {
        isDeleting = true;
    }

    public void ExitDeleteMode()
    {
        isDeleting = false;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            if (hitInfo.collider.tag == "PlacedAsset")
            {
                hitInfo.collider.GetComponentInParent<GenericBuilding>().SetHighlightWhite();
            }
        }
    }

    public bool isDeleteMode()
    {
        return isDeleting;
    }
}
