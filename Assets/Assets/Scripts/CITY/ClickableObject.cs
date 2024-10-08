using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableObject : MonoBehaviour
{
    public string nameObject;

    public void OnClick()
    {
        Debug.Log(nameObject);
    }
}
