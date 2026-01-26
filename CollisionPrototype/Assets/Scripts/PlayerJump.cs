using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider))]
public class PlayerJump : MonoBehaviour
{
    public float jumpHeight = 1.2f;
    public float coyoteTime = 0.1f;
    public float jumpBufferTime = 0.1f;
    public float groundCheckDistance = 0.08f;

    Rigidbody rb;
    CapsuleCollider col;

    float coyoteTimer;
    float jumpBufferTimer;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();

        rb.freezeRotation = true;
        rb.interpolation = RigidbodyInterpolation.Interpolate;
    }

    void Update()
    {
        // Jump input buffering
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpBufferTimer = jumpBufferTime;
        }

        jumpBufferTimer -= Time.deltaTime;
    }

    void FixedUpdate()
    {
        bool grounded = IsGrounded();

        if (grounded)
            coyoteTimer = coyoteTime;
        else
            coyoteTimer -= Time.fixedDeltaTime;

        if (jumpBufferTimer > 0f && coyoteTimer > 0f)
        {
            Jump();
            jumpBufferTimer = 0f;
            coyoteTimer = 0f;
        }
    }

    void Jump()
    {
        // Physics-based jump velocity
        float jumpVelocity = Mathf.Sqrt(2f * Physics.gravity.magnitude * jumpHeight);

        Vector3 velocity = rb.linearVelocity;

        // Cancel downward velocity for consistent jumps
        if (velocity.y < 0f)
            velocity.y = 0f;

        velocity.y = jumpVelocity;
        rb.linearVelocity = velocity;
    }

    bool IsGrounded()
    {
        Vector3 center = transform.TransformPoint(col.center);

        float radius = col.radius * 0.95f;
        float halfHeight = (col.height * 0.5f) - radius;

        Vector3 top = center + Vector3.up * halfHeight;
        Vector3 bottom = center - Vector3.up * halfHeight;

        return Physics.CapsuleCast(
            top,
            bottom,
            radius,
            Vector3.down,
            groundCheckDistance,
            ~0,
            QueryTriggerInteraction.Ignore
        );
    }
}
