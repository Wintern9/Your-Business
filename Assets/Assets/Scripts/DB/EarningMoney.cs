using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EarningMoney : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(ExecuteEveryTwoSeconds());
    }

    void Update()
    {
        if(DBValues.CountItem >= 0)
        {
            DBValues.CountItem -= 1;
            DBValues.Player.Money += 2;     
        }
    }

    IEnumerator ExecuteEveryTwoSeconds()
    {
        while (true)
        {
            DBValues.Player.Save();
            yield return new WaitForSeconds(2f);
        }
    }

}
