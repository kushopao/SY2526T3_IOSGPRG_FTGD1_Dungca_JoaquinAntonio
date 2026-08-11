 using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;


 public enum SceneType
{
    MAIN_MENU,
    CHARACTER_SELECT,
    IN_GAME,
    GAME_OVER
}

public class SceneManager : Singleton<SceneManager>
{
    [SerializeField] GameObject[] scenes;

    // Start is called before the first frame update
    void Start()
    {
        SwitchScene((int)SceneType.MAIN_MENU);
    }

    public void SwitchScene(int index)
    {
        foreach (GameObject sceneObj in scenes)
        {
            sceneObj.SetActive(false);
        }


        scenes[index].SetActive(true);
    }
}
