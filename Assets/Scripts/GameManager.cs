using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager Instance;
    public LevelDirector LDirector;

    private void Awake()
    {
        Instance = this;
    }

    public static void ResetLevel ()
    {
      
        SceneManager.LoadScene(SceneManager.GetSceneAt(0).name);
    }
}
