using UnityEngine;

public class ObjectClicker : MonoBehaviour
{
    public Camera Camera;

    void Update()
    {
        // ��������� ������� ����� ������ ����
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // ��������� Raycast, ����� ����������, ������ �� �� � ������
            if (Physics.Raycast(ray, out hit))
            {
                // ���������, ����� �� ������ ������ ��� ���������, ������� �� ����� ������������
                ClickableObject clickable = hit.collider.GetComponent<ClickableObject>();
                if (clickable != null)
                {
                    clickable.OnClick();  // �������� ����� ��� �������
                }
            }
        }
    }
}
