using UnityEngine;

public class ObjectClicker : MonoBehaviour
{
    void Update()
    {
        // Проверяем нажатие левой кнопки мыши
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Выполняем Raycast, чтобы определить, попали ли мы в объект
            if (Physics.Raycast(ray, out hit))
            {
                // Проверяем, имеет ли объект скрипт или компонент, который мы хотим активировать
                ClickableObject clickable = hit.collider.GetComponent<ClickableObject>();
                if (clickable != null)
                {
                    clickable.OnClick();  // Вызываем метод при нажатии
                }
            }
        }
    }
}
