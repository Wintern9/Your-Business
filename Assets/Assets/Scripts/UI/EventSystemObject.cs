using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EventSystemObject : MonoBehaviour
{
    public EventSystem IsPointerOverUIElement()
    {
        return EventSystem.current;
    }
}
