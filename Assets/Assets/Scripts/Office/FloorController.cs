using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class FloorController : MonoBehaviour
{
    [SerializeField] private Button[] buttonFloor;
    [SerializeField] private GameObject[] objFloor;

    static public int FloorNum = 1;

    void Start()
    {
        for (int i = 0; i < buttonFloor.Length; i++)
        {
            int index = i;
            buttonFloor[i].onClick.AddListener(() => ButtonEvent(buttonFloor[index].GetComponentInChildren<TextMeshProUGUI>(), index));
        }

        objFloor[0].SetActive(true);
        objFloor[1].SetActive(false);
        objFloor[2].SetActive(false);
    }


    void ButtonEvent(TextMeshProUGUI text, int num)
    {
        objFloor[0].SetActive(false);
        objFloor[1].SetActive(false);
        objFloor[2].SetActive(false);

        for(int i=0; i <= num; i++)
        {
            objFloor[i].SetActive(true);
        }

        foreach (var item in buttonFloor)
        {
            var it = item.GetComponentInChildren<TextMeshProUGUI>();
            if (it == buttonFloor[num].GetComponentInChildren<TextMeshProUGUI>())
            {
                it.fontSize = 34;
            } else
            {
                it.fontSize = 24;
            }
        }
    }
}
