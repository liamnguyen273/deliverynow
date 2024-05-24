using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupMissionFail : MonoBehaviour
{
    [SerializeField] Button btRetry;
    static int FailCount = 0;
    void Start()
    {

        btRetry.onClick.AddListener(OnRetry);
        InGame.Instance.PlayBackGroundMusic(true);

        FailCount++;
        /*if (FailCount % 2 == 0)
        {
            IronSourceAds.Instance.ShowInterstitial("mission_fail", (result) =>
            {

            });
        }*/
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
        //TODO: Hide this shit
        //GetComponent<UIPopup>().Hide();
        InGame.Instance.PlayBackGroundMusic(false);
    }
}
