using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public float mouseSensitivity = 2f;
    public float maxLookAngle = 90f;

    float yRotation = 0f;
    Transform cam;

    void Start()
    {
        cam = GetComponentInChildren<Camera>().transform;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * 100f * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * 100f * Time.deltaTime;

        yRotation -= mouseY;
        yRotation = Mathf.Clamp(yRotation, -maxLookAngle, maxLookAngle);

        cam.localRotation = Quaternion.Euler(yRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }
}
