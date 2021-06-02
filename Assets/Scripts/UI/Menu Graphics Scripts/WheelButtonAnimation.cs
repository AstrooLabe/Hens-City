using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WheelButtonAnimation : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    private Transform wheelButtonSprite;
    private float extendSpeed = 10f;
    private Vector2 extentedPosition;
    private Vector2 shrinkedPosition;
    private Image grey;
    private bool isSelected = false;

    [SerializeField]
    private float extendX = 0;
    [SerializeField]
    private float extendY = 0;

    [SerializeField]
    private bool sensX = false;

    void Start()
    {
        wheelButtonSprite = transform;
        grey = transform.Find("Grey").transform.GetComponent<Image>();
        if (wheelButtonSprite == null)
        {
            DebugUtils.debugLogErrorObjectNotFound("wheelButtonSprite");
        }
        else
        {
            extentedPosition = new Vector2(wheelButtonSprite.position.x + extendX, wheelButtonSprite.position.y + extendY);
            shrinkedPosition = wheelButtonSprite.position;
        }
    }

    void Update()
    {

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (transform.GetComponent<Button>().IsInteractable())
        {
            StartCoroutine("extendButton");
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (transform.GetComponent<Button>().IsInteractable() && !isSelected)
        {
            StartCoroutine("shrinkButton");
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (transform.GetComponent<Button>().IsInteractable())
        {
            if (!isSelected)
            {
                //selectedStyle = <Village>
                isSelected = true;
            }
            else
            {
                isSelected = false;
                StartCoroutine("shrinkButton");
            }
        }
    }

    IEnumerator extendButton()
    {
        StopCoroutine("shrinkButton");
        if (sensX)
        {
            while (wheelButtonSprite.position.x < extentedPosition.x)
            {
                moveButton(extentedPosition, new Color32(255, 255, 255, 0));
                yield return null;
            }
        }
        else
        {
            while (wheelButtonSprite.position.x > extentedPosition.x)
            {
                moveButton(extentedPosition, new Color32(255, 255, 255, 0));
                yield return null;
            }
        }
        wheelButtonSprite.position = extentedPosition;
        grey.color = new Color32(255, 255, 255, 0);
    }

    IEnumerator shrinkButton()
    {
        StopCoroutine("extendButton");
        if (!isSelected)
        {
            if (sensX)
            {
                while (wheelButtonSprite.position.x > shrinkedPosition.x)
                {
                    moveButton(shrinkedPosition, new Color32(255, 255, 255, 255));
                    yield return null;
                }
            }
            else
            {
                while (wheelButtonSprite.position.x < shrinkedPosition.x)
                {
                    moveButton(shrinkedPosition, new Color32(255, 255, 255, 255));
                    yield return null;
                }
            }
        }
        wheelButtonSprite.position = shrinkedPosition;
        grey.color = new Color32(255, 255, 255, 255);
    }

    private void moveButton(Vector2 finalPosition, Color32 finalColor)
    {
        wheelButtonSprite.position = Vector2.Lerp(wheelButtonSprite.position, finalPosition, extendSpeed * Time.deltaTime);
        grey.color = Color32.Lerp(grey.color, finalColor, extendSpeed * Time.deltaTime);
    }
}
