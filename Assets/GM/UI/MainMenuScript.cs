using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour {

    AsyncOperation async;

    // Use this for initialization
    void Start () {
        async = null; ;
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void GameStartButtonClick()
    {
        StartCoroutine(GameSceneChange());
    }

    IEnumerator GameSceneChange()
    {
        if (async == null)
            async = SceneManager.LoadSceneAsync(1);

        yield return null;
    }
}
