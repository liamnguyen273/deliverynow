using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Doozy.Engine.UI;
using UnityEngine.UI;
using DG.Tweening;
using System;
public class PopupMissionComplete : MonoBehaviour
{
    [SerializeField] UIButton btNext, btWatchAds;
    [SerializeField] AudioClip audioClipReward;
    [SerializeField] Text coins;
    [SerializeField] Transform questContainer;
    int coinValue = 0;
    static int CompleteCount = 0;
    void Start()
    {
        if(InGame.Instance)
        {
            Profile.Instance.Coin += InGame.Instance.Coins;
            coinValue = InGame.Instance.Coins;
        }
        coins.text = coinValue.ToString();
        Utils.DestroyAllChild(questContainer);

        btNext.OnClick.OnTrigger.Event.AddListener(OnNext);
        btWatchAds.OnClick.OnTrigger.Event.AddListener(onWatchAds);

        if (IronSourceAds.Instance.RewardVideoAvailable)
        {
            btWatchAds.EnableButton();
        }
        else
        {
            btWatchAds.DisableButton();
        }

        DG.Tweening.DOVirtual.DelayedCall(2, () =>
        {
            AddBonus(Define.Game.BONUS, QuestComplete);
        });
        InGame.Instance.PlayBackGroundMusic(true);
        
        CompleteCount++;
        if (CompleteCount % 2 == 0)
        {
            IronSourceAds.Instance.ShowInterstitial("mission_complate", (result) =>
            {

            });
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnNext()
    {
        GameManager.Instance.ShowMenu();
        GetComponent<UIPopup>().Hide();
    }

    void onWatchAds()
    {
        btWatchAds.DisableButton();
        IronSourceAds.Instance.ShowRewardVideo(Define.AdsRewardType.COIN, "mission_completed", (result) =>
        {
            if (result == Define.AdsResult.Watched)
            {
                AddBonus(Define.Game.WATCH_ADS_BONUS, () =>
                {
                    DG.Tweening.DOVirtual.DelayedCall(1, OnNext);
                });
            }
        });
    }

    void AddBonus(int value, Action OnCompleted)
    {

        DOTween.To(
                () => coinValue,
                x =>
                {
                    coins.text = x.ToString();
                    coinValue = x;
                },
                coinValue + value,
                2
            ).OnComplete(() =>
            {
                Profile.Instance.Coin += value;
                InGame.Instance.RefreshUI(Profile.Instance.Coin);
                audioClipReward.PlaySfx();
                OnCompleted.Invoke();
            });
    }

    void QuestComplete()
    {
        Profile.Instance.QuestComplete(QuestManager.Instance.CurrentQuestIndex, InGame.Instance.Coins, InGame.Instance.GetStar(), GameManager.Instance.GetTime());

        Transform questItem = QuestManager.Instance.GetQuestItem(Profile.Instance.LastUnlockedIndex);
        questItem.SetParent(questContainer);
        questItem.localPosition = Vector3.zero;
        questItem.Find("Tag").gameObject.SetActive(true);
        questItem.DOPunchScale(new Vector3(0.1f, 0.1f, 0.1f), 0.5f);
        questItem.GetComponent<UIButton>().OnClick.OnTrigger.Event.AddListener(OnNext);
    }
}
