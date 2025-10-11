using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Doozy.Engine.UI;
using ADS;

public class PopupMissionFail : MonoBehaviour
{
    [SerializeField] UIButton btRetry;
    static int FailCount = 0;
    void Start()
    {

        btRetry.OnClick.OnTrigger.Event.AddListener(OnRetry);
        InGame.Instance.PlayBackGroundMusic(true);

        FailCount++;
        AdsManager.Instance.AdEvents(AdType.GAME_LEVEL_FAILED);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnRetry();
        }
    }

    void OnRetry()
    {
        GameManager.Instance.Retry();
        GetComponent<UIPopup>().Hide();
        InGame.Instance.PlayBackGroundMusic(false);
    }
}
