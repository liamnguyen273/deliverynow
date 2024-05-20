using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Doozy.Engine.UI;

public class PopupMenu: MonoBehaviour
{
    static PopupMenu Instance;
    [SerializeField] Transform btnQuestPrefab;
    [SerializeField] Transform Container;
    [SerializeField] AudioClip audioClipNewOrder;
    void Start()
    {
        Instance = this;
        Utils.DestroyAllChild(Container);
        int index = 0;
        foreach(var quest in QuestManager.Instance.quests)
        {
            if(Profile.Instance.IsQuestUnlocked(index) || Profile.Instance.IsQuestComplete(index))
            {
                QuestItem item = Instantiate(quest.btPrefab, Container).GetComponent<QuestItem>();
                //btn.Find("Title").GetComponent<Text>().text = quest.questTitle;
                item.name = index.ToString();
                item.SetInfo(Profile.Instance.GetQuestInfo(index));
                item.GetComponent<UIButton>().OnClick.OnTrigger.Event.AddListener(()=>{
                    OnQuestSelect(int.Parse(item.name));
                });
                item.NewTag(Profile.Instance.LastUnlockedIndex == index);
            }
            index++;
        }

        audioClipNewOrder.PlaySfx();
        InGame.Instance.PlayBackGroundMusic(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnQuestSelect(int index)
    {
        Debug.Log("ON QUEST SELECT:" + index);
        GameManager.Instance.StartQuest(index);
        GetComponent<UIPopup>().Hide();
    }

    void Hide()
    {
        GetComponent<UIPopup>().Hide();
        Instance = null;
    }

    public static void Show()
    {
        if(Instance == null)
        {
            UIPopup popup = UIPopup.GetPopup(Define.Popup.MENU);
            popup.Show();
        }
    }
}
