using System.Collections;
using Doozy.Engine;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoading : MonoBehaviour
{

    [SerializeField] private Slider _loading;
    private AsyncOperation asyncLoad;
    private float _currentProgress;
    private float _targetProgress;

    private const float _processComplete = 0.9f;

    [SerializeField, Range(0.1f, 1f)] 
    private float _processMultiplier = 0.30f;


    private void Start()
    {
        if(LoadSceneManager.SceneName != "")
        {
            StartCoroutine(LoadScene());
        }
        else
        {
            Debug.LogError("Scene name is empty. Please set the scene name in LoadSceneManager.");
            return;
        }
    }

    private void Update()
    {
        _targetProgress = asyncLoad.progress / _processComplete;

        _currentProgress = Mathf.MoveTowards(_currentProgress, _targetProgress, Time.deltaTime * _processMultiplier);
        _loading.value = _currentProgress;

        if (Mathf.Approximately(_currentProgress, 1f))
        {
            _loading.value = 1f;
            GameEventMessage.SendEvent("InGame");
            //asyncLoad.allowSceneActivation = true; 
        }
    }

    private IEnumerator LoadScene()
    {
        asyncLoad = SceneManager.LoadSceneAsync(LoadSceneManager.SceneName);
        //asyncLoad.allowSceneActivation = false;

        while(!asyncLoad.isDone)
        {
            yield return null;
        }
        GameEventMessage.SendEvent("InGame");
    }
}
