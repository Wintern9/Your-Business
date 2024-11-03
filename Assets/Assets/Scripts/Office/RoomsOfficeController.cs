using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RoomsOfficeController : MonoBehaviour
{
    [SerializeField] private GameObject room;
    [SerializeField] private GameObject Corridor;
    [SerializeField] private GameObject roomParent;
    [SerializeField] private List<List<GameObject>> roomsPos;
    void Start()
    {
        List<Rooms> roomsType = DBValues.Rooms;
        roomsType.Add(new Rooms(0, Room.Room, TypeRoom.Empty, 0, new Vector2(0,0)));
        roomsType.Add(new Rooms(1, Room.RoomNT, TypeRoom.Empty, 0, new Vector2(-10,0)));
        
        InstantiateRoom(roomsType);
    }

    void InstantiateRoom(List<Rooms> roomsType)
    {
        foreach (Rooms roomType in roomsType)
        {
            ////Room////

            var (x, z) = Vector2InInt(roomType.posRoom);
            GameObject obj = Instantiate(room);
            obj.transform.SetParent(roomParent.transform);

            Transform transform = obj.GetComponent<Transform>();

            transform.localPosition = new Vector3(x, 0f, z);

            obj.GetComponent<RoomLogic>().roomType = roomType;
            obj.GetComponent<RoomLogic>().RoomActive();

            ////Corridor////

            GameObject corridor = Instantiate(Corridor);
            corridor.transform.SetParent(roomParent.transform);

            Transform transformCorridor = corridor.GetComponent<Transform>();

            transformCorridor.localPosition = new Vector3(x, 0f, z - 10f);
        }
    }

    (int, int) Vector2InInt(Vector2 p)
    {
        return ( (int)p.x, (int)p.y);
    }

    void Update()
    {
        
    }
}
