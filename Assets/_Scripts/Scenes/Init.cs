using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Init : MonoBehaviour
{
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
        //TODO: send sth to invoke the start game event
        //GameEventMessage.SendEvent("InGame");
    }
}
