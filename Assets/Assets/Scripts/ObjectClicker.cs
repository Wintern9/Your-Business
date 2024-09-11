using UnityEngine;

public class ObjectClicker : MonoBehaviour
{
    void Update()
    {
        // ��������� ������� ����� ������ ����
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
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
