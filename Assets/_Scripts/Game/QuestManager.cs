using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : Singleton<QuestManager>
{
    public List<Quest> quests = new List<Quest>();
    Quest currentQuest;
    int currentQuestIndex;
    void Start()
    {
        DisableAllQuest();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public Quest SelectQuest(int index)
    {
        DisableAllQuest();
        currentQuest = quests[index];
        currentQuest.gameObject.SetActive(true);
        currentQuestIndex = index;
        return currentQuest;
    }

    public Quest CurrentQuest
    {
        get { return currentQuest; }
    }
    public int CurrentQuestIndex
    {
        get { return currentQuestIndex; }
    }

    void DisableAllQuest()
    {
        foreach(var quest in quests)
        {
            quest.gameObject.SetActive(false);
        }
    }

    public Transform GetQuestItem(int index)
    {
        return Instantiate(quests[index].btPrefab);
    }
}
