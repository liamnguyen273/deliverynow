using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class PopupTutorial : MonoBehaviour
{
    static PopupTutorial Instance;
    void Start()
    {
        Instance = this;
    }

    void OnDestroy()
    {
        Instance = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnGo();
        }
    }

    void OnGo()
    {
        //TODO: hide this shit;
        //GetComponent<UIPopup>().Hide();
        InGame.Instance.PlayBackGroundMusic(false);
        Instance = null;
    }
    public static void Hide()
    {
        if (Instance)
        {
            Instance.OnGo();
        }
    }

    public static bool IsShow()
    {
        return Instance != null;
    }
}
