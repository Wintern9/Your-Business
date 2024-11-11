using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class RoomsOfficeController : MonoBehaviour
{
    [SerializeField] private GameObject room;
    [SerializeField] private GameObject Corridor;
    [SerializeField] private GameObject roomParent;
    [SerializeField] private List<List<GameObject>> roomsPos;

    [SerializeField] private List<GameObject> FloorPerent;
    void Start()
    {
        List<Rooms> roomsType = DBValues.Rooms.ToList<Rooms>();
        //roomsType.Add(new Rooms(0, Room.Room, TypeRoom.Empty, 0, new Vector2(0,0)));
        //roomsType.Add(new Rooms(1, Room.RoomNT, TypeRoom.Empty, 0, new Vector2(-10,0)));
        
        InstantiateRoom(roomsType);
    }

    void InstantiateRoom(List<Rooms> roomsType)
    {
        int i = 0;
        foreach (Rooms roomType in roomsType)
        {   
            if (true || roomType.level != 0)
            {
                ////Room////

                var (x, y) = Vector2InInt(roomType.posRoom);
                GameObject obj = Instantiate(room);
                obj.transform.SetParent(FloorPerent[GetIndexFloor(i)].transform);

                Transform transform = obj.GetComponent<Transform>();

                transform.localPosition = new Vector3(x, y/2, 0f);

                obj.GetComponent<RoomLogic>().roomType = roomType;
                obj.GetComponent<RoomLogic>().RoomActive();

                ////Corridor////

                GameObject corridor = Instantiate(Corridor);
                corridor.transform.SetParent(FloorPerent[GetIndexFloor(i)].transform);

                Transform transformCorridor = corridor.GetComponent<Transform>();

                transformCorridor.localPosition = new Vector3(x, y / 2, -10f);
            }
            i++;
        }
    }

    (int, int) Vector2InInt(Vector2 p)
    {
        return ( (int)p.x, (int)p.y);
    }

    int GetIndexFloor(int i)
    {
        return (i / 6);
    }

    void Update()
    {
        
    }
}
