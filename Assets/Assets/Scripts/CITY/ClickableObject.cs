using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClickableObject : MonoBehaviour
{
    static public string nullObject = "City";
    public string nameObject;

    public void OnClick()
    {
        SceneManager.LoadSceneAsync("StartScene");
        nullObject = nameObject;
    }
}
