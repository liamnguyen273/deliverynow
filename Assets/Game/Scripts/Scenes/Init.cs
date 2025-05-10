using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Doozy.Engine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class Init : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject _loadingCanvas;
    [SerializeField] Image _logo;
    [SerializeField] private Slider _loading;
    private AsyncOperation asyncLoad;
    private float _currentProgress;
    private float _targetProgress;

    private const float _processComplete = 0.9f;

    [SerializeField, Range(0.1f, 1f)]
    private float _processMultiplier = 0.30f;
    void Start()
    {
        _loadingCanvas.SetActive(false);


        _logo.transform.DOScale(1, 0.5f).SetEase(Ease.OutBack).OnComplete(() =>
        {
            _logo.transform.DOScale(0, 0.5f).SetEase(Ease.InBack).OnComplete(() =>
            {
                _logo.gameObject.SetActive(false);
                _loadingCanvas.gameObject.SetActive(true);
                StartCoroutine(Load());

            });
        });

    }

    private void Update()
    {
        if (asyncLoad == null)
        {
            return;
        }
        _targetProgress = asyncLoad.progress / _processComplete;

        _currentProgress = Mathf.MoveTowards(_currentProgress, _targetProgress, Time.deltaTime * _processMultiplier);
        _loading.value = _currentProgress;

        if (Mathf.Approximately(_currentProgress, 1f))
        {
            _loading.value = 1f;
            //GameEventMessage.SendEvent("InGame");
            asyncLoad.allowSceneActivation = true;
        }
    }


    IEnumerator Load()
    {
        asyncLoad = SceneManager.LoadSceneAsync("InGame");
        asyncLoad.allowSceneActivation = false;
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        _loading.value = 1f;
        _loadingCanvas.gameObject.SetActive(false);
        GameEventMessage.SendEvent("InGame");
        //asyncLoad.allowSceneActivation = true;
        //asyncLoad = null;
    }
}
