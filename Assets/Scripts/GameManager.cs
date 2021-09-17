using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public GameObject CurrentContent;

    private void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }

    public void ChangeScene(string scenName)
    {
        SceneManager.LoadScene(scenName, LoadSceneMode.Single);
    }
}
