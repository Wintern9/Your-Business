using UnityEngine;

public class ObjectClicker : MonoBehaviour
{
    [SerializeField] private bool Delay;
    [SerializeField] private float TimeDelay;
    public Camera Camera;
    float Timer;

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (Delay)
            {
                Timer += Time.deltaTime;
            }
            else if (Timer >= 10f)
                Timer = 10f;
            else
            {
                Timer = TimeDelay;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (Timer <= TimeDelay)
            {
                Ray ray = Camera.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    ClickableObject clickable = hit.collider.GetComponent<ClickableObject>();
                    if (clickable != null)
                    {
                        clickable.OnClick();
                    }
                }
            }
            Timer = 0f;
        }
    }
}
