using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorAnimation : MonoBehaviour
{
    [SerializeField]
    GameObject cursorUp;
    [SerializeField]
    GameObject cursorDown;
    [SerializeField]
    GameObject cursorLeft;
    [SerializeField]
    GameObject cursorRight;

    public void ResetCursor()
    {
        gameObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
        SetPositionAndScale(cursorUp, Vector3.zero, 1);
        SetPositionAndScale(cursorDown, Vector3.zero, 1);
        SetPositionAndScale(cursorLeft, Vector3.zero, 1);
        SetPositionAndScale(cursorRight, Vector3.zero, 1);

    }

    public void SetCursorSizeToAsset(int x, int z)
    {
        int adjustedX = x * Dimensions.pointEspacement;
        int adjustedZ = z * Dimensions.pointEspacement;

        SetPositionAndScale(cursorDown, new Vector3(adjustedX / 2 - 2, 0, 0), x);
        SetPositionAndScale(cursorUp, new Vector3(adjustedX / 2 - 2, 0, adjustedZ - Dimensions.pointEspacement), x);
        SetPositionAndScale(cursorLeft, new Vector3(0, 0, adjustedZ / 2 - 2), z);
        SetPositionAndScale(cursorRight, new Vector3(adjustedX - Dimensions.pointEspacement, 0, adjustedZ / 2 - 2), z);
    }

    private void SetPositionAndScale(GameObject cursorPart, Vector3 pos, int scale)
    {
        cursorPart.transform.localPosition = pos;
        cursorPart.transform.localScale = new Vector3(scale, 1, 1);
    }
}
