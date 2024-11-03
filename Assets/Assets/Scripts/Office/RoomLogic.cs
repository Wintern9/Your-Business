using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoomLogic : MonoBehaviour
{
    public Rooms roomType;

    /// <summary>
    /// Room, RoomTransp
    /// </summary>
    public List<GameObject> gameObjects;

    public void RoomActive()
    {
        gameObjects[GetIndexRoom()].SetActive(true);
    }

    int GetIndexRoom()
    {
        switch (Transparent())
        {
            case true:
                return 0;
            case false:
                return 1;
        }
    }

    bool Transparent()
    {
        var targetPosition = roomType.posRoom + new Vector2(-10, 0);
        return DBValues.Rooms.Any(r => r.posRoom == targetPosition);
    }

}
