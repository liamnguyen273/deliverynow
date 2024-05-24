using DG.Tweening;
using Owlet.Systems.SaveLoad;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace Owlet.Systems.SceneTransistions
{
    public class SceneTransistion : Singleton<SceneTransistion>
    {
        bool isTransistioning = false;
        [SerializeField] private CanvasGroup background;
        [SerializeField] SceneAnimation sceneAnimation;
        public static float animTime = 0.5f; //DO NOT CHANGE

        protected override void Init()
        {
            base.Init();
            background.alpha = 1;
            background.blocksRaycasts = true;   
        }
        private void OnDestroy()
        {
            background.alpha = 0;
        }

        public void ShowGameplayScene()
        {
            background.DOComplete();
            background.blocksRaycasts = false;
            sceneAnimation.Animation(false, background);
        }
        public void HideGameplayScene()
        {
            background.DOComplete();
            background.blocksRaycasts = true;
            sceneAnimation.Animation(true, background);
        }


        public void ChangeScene(string sceneName, Action<float> onProgressUpdate = null)
        {
            if (isTransistioning) return;
            StartCoroutine(ChangeSceneCoroutine(sceneName, onProgressUpdate));
        }

        IEnumerator ChangeSceneCoroutine(string sceneName, Action<float> onProgressUpdate = null)
        {
            isTransistioning = true;

            HideGameplayScene();
            yield return new WaitForSeconds(0.5f);
            yield return LoadScene(sceneName, onProgressUpdate);
            yield return new WaitForSeconds(0.5f);
            ShowGameplayScene();

            isTransistioning = false;
        }


        IEnumerator LoadScene(string scene, Action<float> onProgressUpdate = null)
        {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene);
            asyncLoad.allowSceneActivation = false;
            float time = 0;
            float progress = 0;
            float minLoadTime = 1f;
            // Wait until the asynchronous scene fully loads
            while (!asyncLoad.isDone)
            {
                time += Time.deltaTime;
                time = Mathf.Clamp(time, 0, minLoadTime);
                progress = time / minLoadTime / 4;
                progress += asyncLoad.progress / 0.9f / 4 * 3;
                onProgressUpdate?.Invoke(progress);
                if (progress >= 1f)
                {
                    asyncLoad.allowSceneActivation = true;
                }

                yield return null;
            }
        }


    }
}
