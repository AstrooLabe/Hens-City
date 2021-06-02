using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DeleteButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private float speed = 10f;
    private Image background;
    private Color32 idleColor = new Color32(19, 19, 19, 240);
    private Color32 selectedColor = new Color32(204, 0, 0, 240);

    // Start is called before the first frame update
    void Start()
    {
        background = transform.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (transform.GetComponent<Button>().IsInteractable())
        {
            StartCoroutine("changeColorToRed");
        }
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        if (transform.GetComponent<Button>().IsInteractable())
        {
            StartCoroutine("changeColorToBlack");
        }
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if (transform.GetComponent<Button>().IsInteractable())
        {

        }
    }

    IEnumerator changeColorToRed()
    {
        StopCoroutine("changeColorToBlack");
        while (background.color.r < selectedColor.r - 1)
        {
            background.color = Color32.Lerp(background.color, selectedColor, speed * Time.deltaTime);
            yield return null;
        }
        background.color = selectedColor;
    }

    IEnumerator changeColorToBlack()
    {
        StopCoroutine("changeColorToRed");
        while (background.color.r * 255 > idleColor.r + 1)
        {
            background.color = Color32.Lerp(background.color, idleColor, speed / 2 * Time.deltaTime);
            yield return null;
        }
        background.color = idleColor;
    }
}
