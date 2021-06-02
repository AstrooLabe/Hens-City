using UnityEngine;

public static class DebugUtils
{

    public static void debugLogErrorObjectNotFound(string objectName)
    {
        Debug.LogError(objectName + " is null or not found");
    }
}
