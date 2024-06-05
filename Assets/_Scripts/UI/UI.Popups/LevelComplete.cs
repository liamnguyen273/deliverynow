using DG.Tweening;
using Owlet.UI.Popups;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DeliveryNow.UI.Popups
{
    public class LevelComplete : Popup
    {
        [SerializeField] Button btnContinue;
        [SerializeField] Button btnRetry;

        [SerializeField] Button btnClaim;
        [SerializeField] Button btnClaimAds;

        [SerializeField] CanvasGroup baseBtnGroup;
        [SerializeField] CanvasGroup rewardBtnGroup;
        

        private void Start()
        {
            btnContinue.onClick.AddListener(Continue);
            btnRetry.onClick.AddListener(Retry);

            btnClaim.onClick.AddListener(ClaimReward);
        }
        protected override void OnEnableUI()
        {
            ResetState();
            base.OnEnableUI();
        }
        void ResetState()
        {
            rewardBtnGroup.interactable = true;
            rewardBtnGroup.blocksRaycasts = true;
            baseBtnGroup.blocksRaycasts = false;
            baseBtnGroup.interactable = false;

        }

        void Continue()
        {
            GameManager.instance.StartNextLevel();
            SelfClosing();
        }

        void Retry()
        {
            GameManager.instance.RestartLevel();
            SelfClosing();
        }

        void ClaimReward()
        {
            StartCoroutine(ClaimRewardAnimation());
        }

        IEnumerator ClaimRewardAnimation()
        {
            //TODO: Coin animation here;
            Debug.Log("Claim animation");
            rewardBtnGroup.interactable = false;
            rewardBtnGroup.blocksRaycasts = false;

            rewardBtnGroup.DOFade(0, 0.2f);
            yield return new WaitForSeconds(0.2f);
            baseBtnGroup.DOFade(1, 0.2f);

            baseBtnGroup.interactable = true;
            baseBtnGroup.blocksRaycasts = true;
        }
    }
}
