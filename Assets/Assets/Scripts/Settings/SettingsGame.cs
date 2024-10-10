using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsGame : MonoBehaviour
{
    static public bool LoadingSceneWithImage = true;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
