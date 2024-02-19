using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : PersistentSingleton<SceneController>
{

    public void ChangeScene()
    {
        SceneManager.LoadScene(1);
    }

    public void ChangeScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
