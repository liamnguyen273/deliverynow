using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils
{
    public static void DestroyAllChild(Transform parent)
    {
        List<GameObject> childs = new List<GameObject>();
        foreach (Transform child in parent)
        {
            childs.Add(child.gameObject);
        }
        parent.DetachChildren();

        foreach (GameObject child in childs)
        {
            GameObject.Destroy(child);
        }
    }
    public static string TimeToString(float time)
    {
        int min = Mathf.FloorToInt(time / 60f);
        int sec = Mathf.FloorToInt(time - (min * 60));
        return min + ":" + sec;
    }
}
