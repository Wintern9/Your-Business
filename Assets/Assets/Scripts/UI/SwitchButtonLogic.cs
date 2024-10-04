using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchButtonLogic : MonoBehaviour
{
    [SerializeField] private List<GameObject> GameObjectOnDisable;
    [SerializeField] private List<GameObject> GameObjectOnEnable;

    [SerializeField] private bool haveAnimation;

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
}
