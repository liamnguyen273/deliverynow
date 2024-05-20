using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Doozy.Engine.UI;

public class PopupMissionDetail : MonoBehaviour
{
    [SerializeField] UIButton btAccept;
    [SerializeField] Text Detail;
    public Action OnAccept;
    void Start()
    {
        btAccept.OnClick.OnTrigger.Event.AddListener(OnBtAccept);
        Detail.text = QuestManager.Instance.CurrentQuest.questDetail;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnBtAccept()
    {
        GetComponent<UIPopup>().Hide();
        OnAccept.Invoke();
    }
}
