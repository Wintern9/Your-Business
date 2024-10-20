using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DBSetValues : MonoBehaviour
{

    void Start()
    {
        DBValues.Credit.Add(new Credit(100));
        DBValues.Credit.Add(new Credit(200));
        DBValues.Credit.Add(new Credit(300));

        foreach (var item in DBValues.Credit)
        {
            Debug.Log($"{item.ID}, {item.Money}");
        }
    }

    void Update()
    {
        
    }
}
