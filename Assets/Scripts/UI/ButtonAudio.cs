using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonAudio : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    private AudioSource uiAudioSource;

    private void Start()
    {
        uiAudioSource = GameObject.Find("UI Audio Source").GetComponent<AudioSource>();
        if (uiAudioSource == null)
        {
            Debug.Log("UI Audio Source not found");
        }

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (GetComponentInParent<Button>().interactable)
        {
            uiAudioSource.clip = Resources.Load<AudioClip>("Sounds/UI/overSound");
            uiAudioSource.Play();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (GetComponentInParent<Button>())
        {
            if (GetComponentInParent<Button>().interactable)
            {
                uiAudioSource.clip = Resources.Load<AudioClip>("Sounds/UI/clickSound");
                uiAudioSource.Play();
            }
        }
    }
}
