using UnityEngine;
using UnityEngine.EventSystems;

public class CameraCityMovement : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float moveSpeedKeyboard = 25f;
    public float scrollSpeed = 5f;
    public float rotationSpeed = 3f;

    public Vector2 zoomRange = new Vector2(5f, 50f);

    public Vector2 xLimits = new Vector2(-50f, 50f);
    public Vector2 zLimits = new Vector2(-50f, 50f);

    private Vector3 dragOrigin;
    static private bool isDragging = false;

    public EventSystem ESPhone;
    public EventSystemObject CPOPhone;

    public bool permitionMove = true;

    private void Start()
    {
        if (CPOPhone != null)
            ESPhone = CPOPhone.IsPointerOverUIElement();
        else
            Debug.LogError("Не загружается сцена телефона или не установлен объект");

        if (ESPhone == null)
        {
            Debug.LogError("ESPhone не содержит объект");
        }
    }

    void Update()
    {
        if (ESPhone != null && !ESPhone.IsPointerOverGameObject() && permitionMove)
        {
            MoveCamera();
        } else if(ESPhone == null)
        {
            ESPhone = CPOPhone.IsPointerOverUIElement();
        }
    }

    void MoveCamera()
    {
        MoveCameraWithKeyboard();

        if (Input.GetMouseButtonDown(0))
        {
            dragOrigin = Input.mousePosition;
            isDragging = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }

        if (isDragging)
        {
            MoveCameraWithMouse();
        }
    }

    void MoveCameraWithKeyboard()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(moveHorizontal, 0, moveVertical);

        direction = Quaternion.Euler(0, transform.eulerAngles.y, 0) * direction;

        Vector3 newPosition = transform.position + direction * moveSpeedKeyboard * Time.fixedDeltaTime;

        newPosition.x = Mathf.Clamp(newPosition.x, xLimits.x, xLimits.y);
        newPosition.z = Mathf.Clamp(newPosition.z, zLimits.x, zLimits.y);

        transform.position = newPosition;
    }

    void MoveCameraWithMouse()
    {
        Vector3 difference = Input.mousePosition - dragOrigin;
        dragOrigin = Input.mousePosition;

        Vector3 move = new Vector3(-difference.x, 0, -difference.y) * moveSpeed * Time.fixedDeltaTime;
        move = Quaternion.Euler(0, transform.eulerAngles.y, 0) * move;

        Vector3 newPosition = transform.position + move;

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

    static public void DraggingFalse()
    {
        isDragging = false;
    }
}
