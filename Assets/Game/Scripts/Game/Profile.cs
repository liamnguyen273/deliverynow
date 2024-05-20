using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding.Serialization.JsonFx;

[System.Serializable]
public enum QuestStatus
{
    LOCK,
    UNLOCK,
    COMPLETE
}
[System.Serializable]
public class QuestInfo
{
    public QuestStatus status;
    public int score;
    public int star;
    public float time;
}
class DataProfile
{
    public QuestInfo[] quest;
    public int lastUnlockedQuest = 1;
    public int coin = 0;
    public int version = 2;
}
public class Profile : Singleton<Profile>
{
    string KEY = "profile";
    DataProfile data;
    override protected void Awake()
    {
        base.Awake();
        Load();
    }

    void Start()
    {

    }

    void Load()
    {
        NewProfile();
        bool shouldUnlockFirstQuest = true;
        if (PlayerPrefs.HasKey(KEY))
        {
            DataProfile loaddata = JsonReader.Deserialize<DataProfile>(PlayerPrefs.GetString(KEY));
            if (loaddata != null && loaddata.version >= data.version)
            {
                shouldUnlockFirstQuest = false;
                data = loaddata;
                Save();
            }
        }
#if USE_CHEAT
        UnlockAllQuest();
#else
        if (shouldUnlockFirstQuest)
        {
            QuestUnlocked(0);
            QuestUnlocked(1);
        }
#endif
    }
    void NewProfile()
    {
        data = new DataProfile();
        data.quest = new QuestInfo[Define.Game.QUEST_COUNT];
        for (int i = 0; i < Define.Game.QUEST_COUNT; i++)
        {
            data.quest[i] = new QuestInfo();
        }
    }
    public void Save()
    {
        string str = JsonWriter.Serialize(data);
        PlayerPrefs.SetString(KEY, str);
    }

    public QuestInfo GetQuestInfo(int index)
    {
        return data.quest[index];
    }

    public bool IsQuestUnlocked(int index)
    {
        return data.quest[index].status == QuestStatus.UNLOCK;
    }
    public bool IsQuestComplete(int index)
    {
        return data.quest[index].status == QuestStatus.COMPLETE;
    }
    int QuestCompleteCount()
    {
        int index = 0;
        foreach (QuestInfo value in data.quest)
        {
            if (value.status == QuestStatus.COMPLETE)
            {
                index++;
            }
        }
        return index;
    }
    int QuestUnlockCount()
    {
        int index = 0;
        foreach (QuestInfo value in data.quest)
        {
            if (value.status == QuestStatus.UNLOCK)
            {
                index++;
            }
        }
        return index;
    }
    void QuestUnlocked(int index)
    {
        data.quest[index].status = QuestStatus.UNLOCK;
        data.lastUnlockedQuest = index;
        Save();
    }
    void UnlockAllQuest()
    {
        for (int i = 0; i < data.quest.Length; i++)
        {
            data.quest[i].status = QuestStatus.UNLOCK;
        }
        Save();
    }
    public void QuestComplete(int index, int score, int star, float time)
    {
        if (data.quest[index].status == QuestStatus.UNLOCK)
        {
            data.quest[index].status = QuestStatus.COMPLETE;
            data.quest[index].score = score;
            data.quest[index].star = star;
            data.quest[index].time = time;
            Save();
            UnlockNextQuest();
        }
    }
    void UnlockNextQuest()
    {
        if (QuestUnlockCount() < 2)
        {
            int index = 0;
            foreach (QuestInfo value in data.quest)
            {
                if (value.status == QuestStatus.LOCK)
                {
                    QuestUnlocked(index);
                    return;
                }
                index++;
            }
        }
    }
    public int LastUnlockedIndex
    {
        get { return data.lastUnlockedQuest; }
    }
    public int Coin
    {
        get { return data.coin; }
        set { data.coin = value; Save(); }
    }
}
