using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Define
{
    public class Popup
    {
        public static string MENU = "Menu";
        public static string MISSION_FAIL = "MissionFail";
        public static string MISSION_COMPLETE = "MissionComplete";
        public static string NEW_MISSION = "NewMission";
        public static string TUTORIAL_CAR = "TutorialCar";
        public static string TUTORIAL_FLY = "TutorialFly";
    }
    public class Camera
    {
        public enum Type
        {
            CAR,
            DRONE,
            HELICOPTER,
            CHARACTER_PICKUP,
            CHARACTER_DELIVERY,
            CHARACTER_HELICOPTER
        }
        public static string CAR = "Car Cam";
        public static string DRONE = "Drone Cam";
        public static string HELICOPTER = "Helicopter Cam";
        public static string CHARACTER_PICKUP = "Char Cam Pickup";
        public static string CHARACTER_DELIVERY = "Char Cam Delivery";
        public static string CHARACTER_HELICCOPTER = "Char Cam Helicopter";
        
    }
    public enum Character
    {
        Men,
        Women
    }
    public enum QuestType

    {
        Car,
        Drone,
        Airplane,
        Helicopter
    }

    public class Game
    {
        public static int QUEST_COUNT = 9;
        public static int BONUS = 100;
        public static int WATCH_ADS_BONUS = 150;
    }
    public enum AdsRewardType
    {
        COIN
    }
    public enum AdsResult
    {
        Watched,
        Fail,
        Closed
    }
}
