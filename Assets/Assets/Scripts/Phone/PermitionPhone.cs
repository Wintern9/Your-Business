using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PermitionPhone : MonoBehaviour
{
    public CameraCityMovement CCMObject;

    private void Start()
    {
        CCMObject = GameObject.FindFirstObjectByType<CameraCityMovement>();
    }

    public void PermitionTrue()
    {
        CCMObject.permitionMove = true;
    }

    public void PermitionFalse()
    {
        CCMObject.permitionMove = false;
    }
}
