using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RoomSwitcher : MonoBehaviour
{
    static int FloorState = 0;

    [SerializeField] private GameObject[] gameObjects; 

    public void SetState(GameObject buttonObject)
    {
        if(buttonObject.name == "Room1")
        {
            FloorState = 0;
        } else if (buttonObject.name == "Room2")
        {
            FloorState = 1;
        }
        else if(buttonObject.name == "Room3")
        {
            FloorState = 2;
        }
        else if(buttonObject.name == "Room4")
        {
            FloorState = 3;
        }
        else if(buttonObject.name == "Room5")
        {
            FloorState = 4;
        }
        else if (buttonObject.name == "Room6")
        {
            FloorState = 5;
        }

        SwitchState();
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
}
