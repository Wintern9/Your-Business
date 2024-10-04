using UnityEngine;

public class CameraCityMovement : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float scrollSpeed = 5f;
    public float rotationSpeed = 3f;

    public Vector2 zoomRange = new Vector2(5f, 50f);

    // Ограничения по перемещению камеры по X и Z
    public Vector2 xLimits = new Vector2(-50f, 50f);  // Ограничения по X
    public Vector2 zLimits = new Vector2(-50f, 50f);  // Ограничения по Z

    private Vector3 dragOrigin;

    private void Update()
    {
        MoveCameraWithKeyboard();

        if (Input.GetMouseButtonDown(1))
        {
            dragOrigin = Input.mousePosition;
        }
        if (Input.GetMouseButton(1))
        {
            MoveCameraWithMouse();
        }

        //ZoomCamera();
    }

    void MoveCameraWithKeyboard()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(moveHorizontal, 0, moveVertical);

        direction = Quaternion.Euler(0, transform.eulerAngles.y, 0) * direction;

        // Перемещение камеры с учетом ограничений
        Vector3 newPosition = transform.position + direction * moveSpeed * Time.deltaTime;

        // Ограничиваем перемещение по X и Z
        newPosition.x = Mathf.Clamp(newPosition.x, xLimits.x, xLimits.y);
        newPosition.z = Mathf.Clamp(newPosition.z, zLimits.x, zLimits.y);

        transform.position = newPosition;
    }

    void MoveCameraWithMouse()
    {
        Vector3 difference = Input.mousePosition - dragOrigin;
        dragOrigin = Input.mousePosition;

        Vector3 move = new Vector3(-difference.x, 0, -difference.y) * moveSpeed * Time.deltaTime;

        move = Quaternion.Euler(0, transform.eulerAngles.y, 0) * move;

        // Перемещение камеры с учетом ограничений
        Vector3 newPosition = transform.position + move;

        // Ограничиваем перемещение по X и Z
        newPosition.x = Mathf.Clamp(newPosition.x, xLimits.x, xLimits.y);
        newPosition.z = Mathf.Clamp(newPosition.z, zLimits.x, zLimits.y);

        transform.position = newPosition;
    }

    void ZoomCamera()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        Vector3 position = transform.position;

        position.y -= scroll * scrollSpeed;
        position.y = Mathf.Clamp(position.y, zoomRange.x, zoomRange.y);

        transform.position = position;
    }
}
