using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PermitionPhone : MonoBehaviour
{
    public CameraCityMovement CCMObject;

    public List<MonoBehaviour> list;

    private void Start()
    {
        CCMObject = GameObject.FindFirstObjectByType<CameraCityMovement>();
    }

    public void PermitionTrue()
    {
        CCMObject.permitionMove = true;
        foreach (MonoBehaviour obj in list)
        {
            obj.enabled = true;
        }
    }

    public void PermitionFalse()
    {
        CCMObject.permitionMove = false;
        foreach (MonoBehaviour obj in list)
        {
            obj.enabled = false;
        }
    }
}
