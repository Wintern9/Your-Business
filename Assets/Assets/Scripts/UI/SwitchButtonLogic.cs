using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchButtonLogic : MonoBehaviour
{
    [SerializeField] private List<GameObject> GameObjectOnDisable;
    [SerializeField] private List<GameObject> GameObjectOnEnable;

    [SerializeField] private bool HaveAnimation;
    [SerializeField] private bool PauseGame;

    public void ButtonClick()
    {
        foreach (GameObject dis in GameObjectOnDisable)
        {
            if (dis != null)
                dis.SetActive(false);
        }
        foreach (GameObject en in GameObjectOnEnable)
        {
            if (en != null)
                en.SetActive(true);
        }
    }

    public void DisableComponent()
    {
        DataTypeSelector dts = gameObject.GetComponent<DataTypeSelector>();

    }

    /// <summary>
    /// Скрипт для кнопки
    /// </summary>
    public void EneblePause()
    {
        Time.timeScale = 0f;
    }

    /// <summary>
    /// Скрипт для кнопки
    /// </summary>
    public void DisablePause()
    {
        Time.timeScale = 1f;
    }
}
