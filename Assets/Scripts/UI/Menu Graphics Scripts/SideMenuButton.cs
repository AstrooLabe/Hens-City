using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SideMenuButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private SideMenuButtonBackground buttonBackground;

    void Start()
    {
        buttonBackground = FindObjectOfType<SideMenuButtonBackground>();
    }

    void Update()
    {

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (transform.GetComponent<Button>().IsInteractable())
        {
            FindObjectOfType<InputsManager>().isCursorOnButton = true;
            buttonBackground.SetVisible(true);
            if (transform.localPosition != buttonBackground.transform.localPosition)
                buttonBackground.MoveToNewPosition(transform.localPosition);
        }
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        if (transform.GetComponent<Button>().IsInteractable())
        {
            FindObjectOfType<InputsManager>().isCursorOnButton = false;
            buttonBackground.SetVisible(false);
        }
    }
}
