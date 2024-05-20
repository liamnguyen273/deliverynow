using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Doozy.Engine;
using Doozy.Engine.UI;
using UnityEngine.UI;

public class InGame : Singleton<InGame>
{
    [SerializeField] Slider progress;
    [SerializeField] Text percent, timeText, coin;
    [SerializeField] AudioClip bgMusic;
    [SerializeField] UIButton btHome;
    bool isStarted = false;
    public int Coins = 0;
    public int DeadCount = 0;
    void Start()
    {
        btHome.OnClick.OnTrigger.Event.AddListener(OnHomeClick);
    }

    void OnEnable()
    {
        if (isStarted)
        {
            //UIPopup.GetPopup(Define.Popup.MENU).Show();
            RefreshUI(Profile.Instance.Coin);
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

    public void UpdateProgressBar(float value)
    {
        if (progress)
        {
            progress.value = value;
            percent.text = Mathf.RoundToInt(value * 100).ToString() + "%";
        }
    }
    public void UpdateTime(float time)
    {
        if (timeText)
        {
            timeText.text = Utils.TimeToString(time);
        }
    }

    public void PlayBackGroundMusic(bool value)
    {
        if(value)
        {
            bgMusic.PlayMusic();
        }
        else
        {
            bgMusic.StopMusic();
        }
    }

    void OnHomeClick()
    {

    }

    public void RefreshUI(int value)
    {
        if(coin)
        {
            coin.text = value.ToString();
        }
    }
    public void AddScore(int value)
    {
        Coins += value;
        RefreshUI(Coins);
    }
    public void Reset()
    {
        Coins = 0;
        DeadCount = 0;
        RefreshUI(Coins);
    }

    public int GetStar()
    {
        return Mathf.Max(1, 5 - DeadCount);
    }

}
