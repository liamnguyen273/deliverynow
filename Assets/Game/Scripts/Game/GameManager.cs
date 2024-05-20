using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Doozy.Engine;
using Doozy.Engine.UI;
using Cinemachine;
using Dreamteck.Splines;

public class GameManager : Singleton<GameManager>
{
    enum State
    {
        INIT,
        SHOW_QUEST,
        PICKUP,
        CARRY,
        DELIVERY,
        DELIVERED
    }
    public Car Car;
    public Character character;
    public Drone drone;
    public Helicopter helicopter;
    [SerializeField] GameObject CoinPrefab;
    [SerializeField] Transform CoinContainer;
    List<Vehicle> allVehicle = new List<Vehicle>();
    [SerializeField] Animator cinemachineAnimator;
    [SerializeField] List<CinemachineVirtualCamera> charCameraList = new List<CinemachineVirtualCamera>();
    State state;
    Quest quest;
    float questTime;
    bool ShouldCheckQuestTime = false;
  
    void Start()
    {
        SetState(State.INIT);
    }

    // Update is called once per frame
    void Update()
    {
        if (ShouldCheckQuestTime)
        {
            questTime += Time.deltaTime;
            if (questTime >= quest.time)
            {
                MissionFail();
            }
            UpdateTime(quest.time - questTime);
        }
    }
    void SetState(State state)
    {
        switch (state)
        {
            case State.INIT:
                ShowMenu();
                break;
            case State.SHOW_QUEST:
                ShowQuest();
                break;
            case State.PICKUP:
                if (quest.Type == Define.QuestType.Drone)
                {
                    drone.StartPickup(quest.PickupPath);
                    SwitchCamera(Define.Camera.Type.DRONE);
                }
                else if (quest.Type == Define.QuestType.Helicopter)
                {
                    character.StartPickup(quest.PickupPath);
                    helicopter.StartPickup(quest.path);
                    SwitchCamera(Define.Camera.Type.CHARACTER_HELICOPTER);
                }
                else
                {
                    character.StartPickup(quest.PickupPath);
                    SwitchCamera(Define.Camera.Type.CHARACTER_PICKUP);
                }

                break;
            case State.CARRY:
                if (quest.Type == Define.QuestType.Drone)
                {
                    drone.StartCarry(quest.path);
                    SwitchCamera(Define.Camera.Type.DRONE);
                }
                else if (quest.Type == Define.QuestType.Helicopter)
                {
                    helicopter.StartCarry(quest.path);
                    SwitchCamera(Define.Camera.Type.HELICOPTER);
                }
                else
                {
                    Car.StartCarry(quest.path);
                    SwitchCamera(Define.Camera.Type.CAR);
                }
                questTime = 0;
                ShouldCheckQuestTime = true;
                break;
            case State.DELIVERY:
                if (quest.Type == Define.QuestType.Drone)
                {
                    drone.StartDelivery();
                    SwitchCamera(Define.Camera.Type.DRONE);
                }
                else if (quest.Type == Define.QuestType.Helicopter)
                {
                    helicopter.StartDelivery();
                    SwitchCamera(Define.Camera.Type.CHARACTER_HELICOPTER);
                }
                else
                {
                    character.StartDelivery(quest.DeliveryPath);
                    SwitchCamera(Define.Camera.Type.CHARACTER_DELIVERY);
                }
                ShouldCheckQuestTime = false;
                break;
            case State.DELIVERED:
                ShouldCheckQuestTime = false;
                //SwitchCamera(Define.Camera.Type.CAR);
                MissionComplete();
                break;
        }
    }

    public void ShowQuest()
    {
        UIPopup popup = UIPopup.GetPopup(Define.Popup.NEW_MISSION);
        popup.Show();
        popup.GetComponent<PopupMissionDetail>().OnAccept += StartPickUp;
    }

    public void ShowMenu()
    {
        PopupMenu.Show();
    }
    public void MissionFail()
    {
        ShouldCheckQuestTime = false;
        UIPopup.GetPopup(Define.Popup.MISSION_FAIL).Show();
    }

    void MissionComplete()
    {
        UIPopup.GetPopup(Define.Popup.MISSION_COMPLETE).Show();
    }
    public void StartPickUp()
    {
        SetState(State.PICKUP);
    }
    public void EndPickUp()
    {
        SetState(State.CARRY);
    }
    public void EndCarry()
    {
        SetState(State.DELIVERY);
    }
    public void EndDelivery()
    {
        SetState(State.DELIVERED);
    }

    public void AddVehicle(Vehicle vehicle)
    {
        if (!allVehicle.Contains(vehicle))
        {
            allVehicle.Add(vehicle);
        }
    }
    public void Retry()
    {
        if (quest.Type == Define.QuestType.Drone)
        {
            drone.Reset();
        }
        else if (quest.Type == Define.QuestType.Helicopter)
        {
            helicopter.Reset();
        }
        else
        {
            Car.Reset();
            allVehicle.ForEach((v) =>
            {
                v.Reset();
            });
        }
        questTime = 0;
        ShouldCheckQuestTime = true;
        quest.Reset();
        InGame.Instance.Reset();
    }

    public void UpdateProgress(float value)
    {
        InGame.Instance.UpdateProgressBar(value);
    }
    public void UpdateTime(float time)
    {
        InGame.Instance.UpdateTime(time);
    }

    void SwitchCamera(Define.Camera.Type type)
    {
        switch (type)
        {
            case Define.Camera.Type.CAR:
                cinemachineAnimator.Play(Define.Camera.CAR);
                break;
            case Define.Camera.Type.DRONE:
                cinemachineAnimator.Play(Define.Camera.DRONE);
                break;
            case Define.Camera.Type.HELICOPTER:
                cinemachineAnimator.Play(Define.Camera.HELICOPTER);
                break;
            case Define.Camera.Type.CHARACTER_PICKUP:
                cinemachineAnimator.Play(Define.Camera.CHARACTER_PICKUP);
                break;
            case Define.Camera.Type.CHARACTER_DELIVERY:
                cinemachineAnimator.Play(Define.Camera.CHARACTER_DELIVERY);
                break;
            case Define.Camera.Type.CHARACTER_HELICOPTER:
                cinemachineAnimator.Play(Define.Camera.CHARACTER_HELICCOPTER);
                break;
        }

    }

    public void GetPackageOK()
    {
        SetState(State.CARRY);
    }

    public void StartQuest(int index)
    {
        quest = QuestManager.Instance.SelectQuest(index);
        quest.Reset();
        SetCharacter(quest.character);
        Car.SetActive(false);
        character.SetActive(false);

        if (quest.Type == Define.QuestType.Drone)
        {
            drone.SetPath(quest.PickupPath);
            SwitchCamera(Define.Camera.Type.DRONE);
        }
        else if (quest.Type == Define.QuestType.Helicopter)
        {
            helicopter.SetPath(quest.path);
            SwitchCamera(Define.Camera.Type.HELICOPTER);
            character.SetActive(true);
        }
        else
        {
            SwitchCamera(Define.Camera.Type.CAR);
            Car.SetPath(quest.path);
            Car.SetActive(true);
            character.SetActive(true);
        }
        //SetState(State.SHOW_QUEST);
        StartPickUp();
        InGame.Instance.Reset();
    }
    public void SetCharacter(Character character)
    {
        this.character = character;
        foreach(var cam in charCameraList)
        {
            cam.Follow = character.transform;
            cam.LookAt = character.transform;
        }
    }

    public void GenerateCoin(SplineComputer splineComputer)
    {
        Utils.DestroyAllChild(CoinContainer);
    }

    public float GetTime()
    {
        return questTime;
    }
}
