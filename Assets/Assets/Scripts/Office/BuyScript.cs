using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyScript : MonoBehaviour
{
    public int Money;

    public void Buy()
    {
        DBValues.Player.Money -= (float)Money;
        DBValues.Player.Save();
    }
}
