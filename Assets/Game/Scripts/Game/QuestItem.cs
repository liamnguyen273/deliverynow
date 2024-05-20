using UnityEngine;
using UnityEngine.UI;
public class QuestItem : MonoBehaviour
{
    public Text PickupLoaction, DeliveryLocation, Score, Time;
    public Transform Stars;
    public GameObject Tag, complateIcon;

    public void SetInfo(QuestInfo quest)
    {
        Score.text = quest.score.ToString();
        Time.text = Utils.TimeToString(quest.time);
        complateIcon.SetActive(quest.status == QuestStatus.COMPLETE);
        // PickupLoaction.text = pickupLoaction;
        // DeliveryLocation.text = deliveryLocation;
        SetStar(quest.star);
    }

    void SetStar(int value)
    {
        value = Mathf.Min(5, value);
        for (int i = 0; i < Stars.childCount; i++)
        {
            Color c = Color.white;
            if (i >= value)
            {
                c.a = 0.3f;
            }
            Stars.GetChild(i).GetComponent<Image>().color = c;
        }
    }

    public void NewTag(bool value)
    {
        Tag.SetActive(value);
    }
}
