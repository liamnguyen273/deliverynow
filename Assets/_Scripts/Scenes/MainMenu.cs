using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
            //TODO: Show menu popup
            //UIPopup.GetPopup(Define.Popup.MENU).Show();
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
