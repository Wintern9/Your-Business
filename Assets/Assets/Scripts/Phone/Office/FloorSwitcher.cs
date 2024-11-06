using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FloorSwitcher : MonoBehaviour
{
    static int FloorState = 0;

    [SerializeField] private TextMeshProUGUI[] textMeshProUGUIs; 

    void Update()
    {
        
    }

    public void SetState(GameObject buttonObject)
    {
        if(buttonObject.name == "1 Floor")
        {
            FloorState = 0;
        } else if (buttonObject.name == "2 Floor")
        {
            FloorState = 1;
        }
        else if(buttonObject.name == "3 Floor")
        {
            FloorState = 2;
        }
    }

    public void SwitchState()
    {
        for(int i = 0; i < textMeshProUGUIs.Length; i++)
        {
            if(FloorState == i)
            {
                textMeshProUGUIs[i].color = Color.white; 
                textMeshProUGUIs[i].fontSize = 50; 
            } 
            else
            {
                textMeshProUGUIs[i].color = Color.grey;
                textMeshProUGUIs[i].fontSize = 35;
            }
        }
    }
}
