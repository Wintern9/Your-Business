using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_BankAssistant : MonoBehaviour
{
    public Camera Camera;

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = Camera.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform == transform)
                {
                    GetComponent<SwitchButtonLogic>().ButtonClick();
                    CameraCityMovement.DraggingFalse();
                }
            }
        }
    }
}
