using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;

public class Quest : MonoBehaviour
{
    public SplineComputer path;
    public SplineComputer PickupPath, DeliveryPath;
    public Character character;
    public Transform package;
    public Define.QuestType Type;
    public float time = 10;
    public string questTitle;
    public string questDetail;
    public Transform btPrefab;
    public Transform CoinContainer;

    public void Reset()
    {
        foreach(Transform coin in CoinContainer)
        {
            coin.gameObject.SetActive(true);
        }
    }
}
