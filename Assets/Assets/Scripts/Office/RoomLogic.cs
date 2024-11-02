using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomLogic : MonoBehaviour
{
    public Rooms roomType;

    /// <summary>
    /// Room, RoomL, RoomR, RoomLR
    /// </summary>
    public List<GameObject> gameObjects;

    public void RoomActive()
    {
        gameObjects[GetIndexRoom()].SetActive(true);
    }

    int GetIndexRoom()
    {
        switch (roomType.room)
        {
            case Room.Room:
                return 0;
            case Room.RoomLeft:
                return 1;
            case Room.RoomRight:
                return 2;
            case Room.RoomLeftRight:
                return 3;
            default:
                return 0;
        }
    }
}
