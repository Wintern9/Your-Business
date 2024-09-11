using UnityEngine;

public class CameraCityMovement : MonoBehaviour
{

    public float moveSpeed = 10f;
    public float scrollSpeed = 5f;
    public float rotationSpeed = 3f;

    public Vector2 zoomRange = new Vector2(5f, 50f);

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

        transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);
    }

    void MoveCameraWithMouse()
    {
        Vector3 difference = Input.mousePosition - dragOrigin;
        dragOrigin = Input.mousePosition;

        Vector3 move = new Vector3(-difference.x, 0, -difference.y) * moveSpeed * Time.deltaTime;

        move = Quaternion.Euler(0, transform.eulerAngles.y, 0) * move;

        transform.Translate(move, Space.World);
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
