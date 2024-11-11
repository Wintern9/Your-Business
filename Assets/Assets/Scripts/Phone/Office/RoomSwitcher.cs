using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RoomSwitcher : MonoBehaviour
{
    static int FloorState = 0;

    [SerializeField] private GameObject[] gameObjects;

    public void SetState(GameObject buttonObject)
    {
        if (buttonObject.GetComponentInParent<BuyRoom>().RoomState == 1)
        {
            if (buttonObject.name == "Room1")
            {
                FloorState = 0;
            }
            else if (buttonObject.name == "Room2")
            {
                FloorState = 1;
            }
            else if (buttonObject.name == "Room3")
            {
                FloorState = 2;
            }
            else if (buttonObject.name == "Room4")
            {
                FloorState = 3;
            }
            else if (buttonObject.name == "Room5")
            {
                FloorState = 4;
            }
            else if (buttonObject.name == "Room6")
            {
                FloorState = 5;
            }

            SwitchState();
        }
    }

    public void SwitchState()
    {
        for(int i = 0; i < gameObjects.Length; i++)
        {
            if(FloorState == i)
            {
                gameObjects[i].SetActive(true);
            } 
            else
            {
                gameObjects[i].SetActive(false);
            }
        }
    }

    [SerializeField] private Image Obj1;
    [SerializeField] private Image Obj2;
    [SerializeField] private Image Obj3;

    [SerializeField] private Sprite[] sprites;
    [SerializeField] private GameObject Corridor1;
    [SerializeField] private GameObject Corridor2;

    int ObCount = 0;

    public void SwitchSprite1()
    {
        ObCount++;
        if (ObCount == 1)
        {
            Obj2.sprite = sprites[1];
            Obj1.sprite = sprites[2];
            Corridor1.SetActive(true);
        } 
        else if(ObCount == 2)
        {
            Obj3.sprite = sprites[1];
            Obj2.sprite = sprites[2];
            Corridor2.SetActive(true);
        }
    }
}
