using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Скрипт распалагающийся на сцене StartScene
/// </summary>
public class ScenesManager : MonoBehaviour
{
    [SerializeField] private ScenesLoader _scenesLoader;

    void Awake()
    {
        _scenesLoader.scenesToLoad[0] = ClickableObject.nullObject;
    }
}
