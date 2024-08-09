using System;
using System.Collections;
using System.Collections.Generic;
using DeliveryNow;
using UnityEngine;
using UnityEngine.UI;

public class BonusUI : MonoBehaviour
{
    private bool isActive = false;
    public static BonusUI Instance{get;private set;}

    [SerializeField] Button closeButton;
    [SerializeField] Button openButton;
    private void Awake(){
        Instance = this;
    }
    private void Start(){
        MainMenuUI.Instance.OnButtonPressed += MainMenuUI_OnButtonPressed;
        closeButton.onClick.AddListener(()=>{
            Hide();
        });
        Hide();
    }

    private void MainMenuUI_OnButtonPressed(object sender, MainMenuUI.buttonMenu e)
    {
        if(e == MainMenuUI.buttonMenu.bonus){
            Show();
        }else{
            Hide();
        }
    }


    private void Show(){
        gameObject.SetActive(true);
        isActive = true;
    }
    private void Hide(){
        gameObject.SetActive(false);
        isActive = false;
    }
}
