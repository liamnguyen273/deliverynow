using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Doozy.Engine;
using UnityEngine.SceneManagement;

public class Init : MonoBehaviour
{
    // Start is called before the first frame update
    TextAsset doozyFile;
    void Start()
    {
        StartCoroutine(Load());
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator Load()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("InGame");
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        GameEventMessage.SendEvent("InGame");
    }
}
