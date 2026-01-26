using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMoveRB : MonoBehaviour
{
    public float moveSpeed = 5f;

    Rigidbody rb;
    float inputX;
    float inputZ;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        rb.interpolation = RigidbodyInterpolation.Interpolate;
    }

    void Update()
    {
        inputX = Input.GetAxisRaw("Horizontal");
        inputZ = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        Vector3 wishDir = (transform.right * inputX + transform.forward * inputZ).normalized;
        Vector3 planarVel = wishDir * moveSpeed;

        rb.linearVelocity = new Vector3(planarVel.x, rb.linearVelocity.y, planarVel.z);
    }
}
