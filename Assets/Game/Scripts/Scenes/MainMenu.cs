using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Doozy.Engine;
using Doozy.Engine.UI;

public class MainMenu : MonoBehaviour
{
    public Button PlayGame;
    bool isStarted = false;
    void Start()
    {
        
    }

    void OnEnable()
    {
        if(isStarted)
        {
            UIPopup.GetPopup(Define.Popup.MENU).Show();
        }
        else
        {
            isStarted = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
