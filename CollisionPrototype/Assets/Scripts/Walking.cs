using UnityEngine;

public class Walking : MonoBehaviour
{
    public float moveSpeed = 7f;
    public float drag = 50f;
    public float mouseSensitivity = 2f;
    public float gravity = -9.81f;

    private CharacterController characterController;
    private Camera playerCamera;
    private float verticalRotation = 0f;
    private Vector3 currentVelocity;
    private Vector3 verticalVelocity;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        playerCamera = GetComponentInChildren<Camera>();

        if (characterController == null)
        {
            Debug.LogError("CharacterController not found on the " + gameObject.name);
        }

        if (playerCamera == null)
        {
            Debug.LogError("Camera component not found as child of " + gameObject.name);
            playerCamera = Camera.main;
            if (playerCamera == null)
            {
                Debug.LogError("Main camera not found either bruh");
            }
        }
        else
        {
            Debug.Log("Camera found: " + playerCamera.name);
        }

        Cursor.lockState = CursorLockMode.Locked;
        currentVelocity = Vector3.zero;
        verticalVelocity = Vector3.zero;
    }
    void Update()
    {
        HandleMouseLook();
        HandleMovement();
        HandleGravity();
    }

    // Mouse Settings
    // Feel free to adjust the settings but its easier in the panel thing
    void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // Rotate the camera vertically
        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);

        playerCamera.transform.localEulerAngles = new Vector3(verticalRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX); //rotate horizontally the player
    }

    // Player Movement (drag version) // not sure if we will need the sliding version
    //it's just so that direction matches where the player is looking, thought it would better for fast movement
    void HandleMovement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 movement = new Vector3(horizontal, 0f, vertical).normalized;
        Vector3 worldMovement = transform.TransformDirection(movement);
        Vector3 desiredVelocity = worldMovement * moveSpeed;

        currentVelocity = Vector3.MoveTowards(
            currentVelocity,
            desiredVelocity,
            drag * Time.deltaTime
        );

        if (characterController != null && characterController.enabled)
        {
            Vector3 finalVelocity = currentVelocity + verticalVelocity;
            characterController.Move(finalVelocity * Time.deltaTime);
        }
        else
        {
            transform.Translate(currentVelocity * Time.deltaTime, Space.World);
        }
    }

    // player walks now, gravity handling fixed that floating issue

    void HandleGravity()
    {
        if (characterController != null && characterController.enabled)
        {
            if (!characterController.isGrounded)
            {
                verticalVelocity.y += gravity * Time.deltaTime;
            }
            else
            {
                verticalVelocity.y = -0.5f; // to keep the player grounded cause it kept floating, resets vertical
            }
        }
    }
}