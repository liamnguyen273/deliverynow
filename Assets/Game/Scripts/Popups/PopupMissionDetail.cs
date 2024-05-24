using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupMissionDetail : MonoBehaviour
{
    [SerializeField] Button btAccept;
    [SerializeField] Text Detail;
    public Action OnAccept;
    void Start()
    {
        btAccept.onClick.AddListener(OnBtAccept);
        Detail.text = QuestManager.Instance.CurrentQuest.questDetail;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnBtAccept()
    {
        //TODO: Close the Popup
        //GetComponent<UIPopup>().Hide();
        OnAccept.Invoke();
    }
}
