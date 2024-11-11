using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceCheck : MonoBehaviour
{
    [SerializeField] private GameObject[] Place1;
    [SerializeField] private GameObject[] Desigh;


    void Update()
    {
        if (DBValues.CountPlaces[0] >= 1)
        {
            Place1[DBValues.CountPlaces[0]].SetActive(true);
        }
        if (DBValues.CountPlaces[1] >= 1)
        {
            foreach (GameObject room in Desigh)
            {
                room.SetActive(true);
            }
        }
    }
}
