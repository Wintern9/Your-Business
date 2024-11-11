using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomsCheck : MonoBehaviour
{
    [SerializeField] private GameObject[] Room1;
    [SerializeField] private GameObject[] Room2;


    public void BuyRoooom()
    {
        if(DBValues.Room == 1)
        {
            foreach(GameObject room in Room1)
            {
                room.SetActive(true);
            }
        }
        if (DBValues.Room == 2)
        {
            foreach (GameObject room in Room2)
            {
                room.SetActive(true);
            }
        }
    }
}
