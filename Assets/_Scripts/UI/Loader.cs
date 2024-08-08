using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DeliveryNow
{
    public static class Loader
    {  
        public enum Scene{
            MainMenuScene,
            MainGameScene,
            //LoadingScene,
        }
        public static Scene targetScene;
        public static void Load(Scene targetSceneName){
            Loader.targetScene = targetSceneName;
            SceneManager.LoadScene(targetScene.ToString());
        }
    }
}
