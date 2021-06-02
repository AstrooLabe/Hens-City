using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuButtonAnimations : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    private Transform foregroundButtonSprite;
    private TextMeshProUGUI buttonText;
    private float extendSpeed = 10f/60f;
    private Color32 greyText = new Color32(102, 102, 102, 255);
    private Color32 white = new Color32(255, 255, 255, 255);
    private Vector2 shrinkedSize = new Vector2(30f, 100f);
    private Vector2 extendedSize = new Vector2(330f, 100f);

    void Start()
    {
        foregroundButtonSprite = transform.Find("ForegroundImage");
        buttonText = transform.Find("TextTMP").transform.GetComponent<TextMeshProUGUI>();
        if (foregroundButtonSprite == null)
        {
            DebugUtils.debugLogErrorObjectNotFound("foregroundButtonSprite");
        }
        if (buttonText == null)
        {
            DebugUtils.debugLogErrorObjectNotFound("buttonText");
        }
        buttonText.faceColor = greyText;
    }

    void Update()
    {

    }

    public void ResetAnimation()
    {
        StopCoroutine("extendForeground");
        StopCoroutine("shrinkForeground");
        foregroundButtonSprite.GetComponent<RectTransform>().sizeDelta = shrinkedSize;
        buttonText.faceColor = greyText;

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (transform.GetComponent<Button>().IsInteractable())
        {
            StartCoroutine("extendForeground");
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (transform.GetComponent<Button>().IsInteractable())
        {
            StartCoroutine("shrinkForeground");
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (transform.GetComponent<Button>().IsInteractable())
        {

        }
    }

    IEnumerator extendForeground()
    {
        StopCoroutine("shrinkForeground");
        while (foregroundButtonSprite.GetComponent<RectTransform>().sizeDelta.x < extendedSize.x - 1)
        {
            buttonText.faceColor = Color32.Lerp(buttonText.faceColor, white, extendSpeed);
            foregroundButtonSprite.GetComponent<RectTransform>().sizeDelta = Vector2.Lerp(foregroundButtonSprite.GetComponent<RectTransform>().sizeDelta, extendedSize, extendSpeed);
            yield return null;
        }
        buttonText.faceColor = white;
        foregroundButtonSprite.GetComponent<RectTransform>().sizeDelta = extendedSize;
    }

    IEnumerator shrinkForeground()
    {
        StopCoroutine("extendForeground");
        while (foregroundButtonSprite.GetComponent<RectTransform>().sizeDelta.x > shrinkedSize.x + 1)
        {
            buttonText.faceColor = Color32.Lerp(buttonText.faceColor, greyText, extendSpeed);
            foregroundButtonSprite.GetComponent<RectTransform>().sizeDelta = Vector2.Lerp(foregroundButtonSprite.GetComponent<RectTransform>().sizeDelta, shrinkedSize, extendSpeed);
            yield return null;
        }
        buttonText.faceColor = greyText;
        foregroundButtonSprite.GetComponent<RectTransform>().sizeDelta = shrinkedSize;
    }

}
