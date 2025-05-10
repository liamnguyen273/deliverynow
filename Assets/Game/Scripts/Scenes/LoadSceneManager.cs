using Doozy.Engine;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;

public class LoadSceneManager : Singleton<LoadSceneManager>
{
    public static string SceneName { get; private set; }

    private void Start()
    {
        SceneName = "";

    }

    public void LoadScene(string sceneName)
    {
        SceneName = sceneName;
        StartCoroutine(LoadSceneCoroutine(sceneName));
    }

    public IEnumerator LoadSceneCoroutine(string sceneName)
    {
        SceneName = sceneName;

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(LoadSceneManager.SceneName);
        //asyncLoad.allowSceneActivation = false;

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        GameEventMessage.SendEvent("InGame");

    }

}
