using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SideMenuButtonBackground : MonoBehaviour
{
    [SerializeField]
    RectTransform imageHolder;

    private readonly float speed = 10f/60f;
    private Vector2 originalSize;
    private Vector2 extendedSize;
    private bool goneUp = false;
    //private bool extended = false;
    private float extend = 20f;
    private float half = 0;

    // Start is called before the first frame update
    void Start()
    {
        originalSize = GetComponent<RectTransform>().sizeDelta;
        extendedSize = new Vector2(originalSize.x, originalSize.y + extend);
        half = originalSize.y / 2f;
    }

    void Update()
    {

    }

    public void SetVisible(bool value)
    {
        if (value)
        {
            StopCoroutine("WaitBeforeHiding");
            imageHolder.GetComponent<Image>().enabled = value;
        }
        else
            StartCoroutine("WaitBeforeHiding");
    }

    public void MoveToNewPosition(Vector3 newPosition)
    {
        StopAllCoroutines();
        StartCoroutine(MoveToPosition(newPosition));
    }

    IEnumerator WaitBeforeHiding()
    {
        yield return new WaitForSecondsRealtime(0.15f);

        imageHolder.GetComponent<Image>().enabled = false;
    }

    IEnumerator MoveToPosition(Vector3 newPosition)
    {
        if (newPosition.y > transform.localPosition.y)
        {
            goneUp = true;
            imageHolder.pivot = new Vector2(0.5f, 0);
            imageHolder.localPosition = new Vector3(0, half * -1, 0);
        }
        else
        {
            goneUp = false;
            imageHolder.pivot = new Vector2(0.5f, 1);
            imageHolder.localPosition = new Vector3(0, half, 0);
        }

        while (imageHolder.sizeDelta.y < extendedSize.y - 1)
        {
            imageHolder.sizeDelta = Vector2.Lerp(imageHolder.sizeDelta, new Vector2(extendedSize.x, extendedSize.y + extend / 2), speed / 2);
            yield return null;
        }

        imageHolder.sizeDelta = extendedSize;

        while (transform.localPosition.y > newPosition.y + (extend + 1) || transform.localPosition.y < newPosition.y - (extend + 1))
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(newPosition.x, newPosition.y + (goneUp ? extend / 2 * -1 : extend / 2)), speed);
            yield return null;
        }
        transform.localPosition = new Vector3(newPosition.x, newPosition.y + (goneUp ? extend * -1 : extend));

        imageHolder.pivot = new Vector2(0.5f, goneUp ? 1 : 0);
        imageHolder.localPosition = new Vector3(0, goneUp ? extend + half : (extend + half) * -1, 0);

        while (imageHolder.sizeDelta.y > originalSize.y + 1)
        {
            imageHolder.sizeDelta = Vector2.Lerp(imageHolder.sizeDelta, new Vector2(originalSize.x, originalSize.y - extend / 2), speed);
            yield return null;
        }

        imageHolder.sizeDelta = originalSize;
        transform.localPosition = newPosition;
        imageHolder.pivot = new Vector2(0.5f, 0.5f);
        imageHolder.localPosition = new Vector3(0, 0, 0);
    }
}
