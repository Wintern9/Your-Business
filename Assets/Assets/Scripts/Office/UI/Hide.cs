using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hide : MonoBehaviour
{
    [SerializeField] private GameObject[] RankiObj;
    [SerializeField] private Image ImageCom;
    [SerializeField] private Sprite EyeClose;
    [SerializeField] private Sprite EyeOpen;

    bool HideUI = false;

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(ButtonClick);
        ImageCom = GetComponent<Image>();
    }


    void ButtonClick()
    {
        if (HideUI) 
        {
            foreach (GameObject obj in RankiObj)
            {
                obj.SetActive(true);
            }
            ImageCom.sprite = EyeOpen;
            HideUI = false;
        } else
        {
            foreach (GameObject obj in RankiObj)
            {
                obj.SetActive(false);
            }
            ImageCom.sprite = EyeClose;
            HideUI = true;
        }
    }
}
