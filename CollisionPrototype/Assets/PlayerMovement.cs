using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float mouseSensitivity = 2f;

    float yRotation = 0f;
    Transform cam;

    //Sliding Changes
    public float slowSlide = 5f;
    public float fastSlide = 15f;

    void Start()
    {
        cam = GetComponentInChildren<Camera>().transform;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // Mouse look
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * 100f * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * 100f * Time.deltaTime;

        yRotation -= mouseY;
        yRotation = Mathf.Clamp(yRotation, -90f, 90f);

        cam.localRotation = Quaternion.Euler(yRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);

        // Movement
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        transform.position += move * moveSpeed * Time.deltaTime;

        //Sliding Trigger
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            slide(move);
        }
    }

    void slide(Vector3 move){
        //If standing still slowSlide
        transform.position += move * slowSlide;
        //If moving fastSlide
    }
}