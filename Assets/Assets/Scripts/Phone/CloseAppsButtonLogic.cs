using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseAppsButtonLogic : MonoBehaviour
{
    public GameObject thisApp;

    public void CloseApp()
    {
        if(thisApp != null)
        {
            thisApp.SetActive(false);
            thisApp = null;
        }
    }
}
