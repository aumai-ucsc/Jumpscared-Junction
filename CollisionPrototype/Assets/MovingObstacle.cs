using UnityEngine;

public class MovingObstacle : MonoBehaviour
{
    public Vector3 startPoint;
    public Vector3 endPoint;
    public float speed = 1f;

    float t = 0f;

    void Start()
    {
        startPoint = transform.position;
    }
    void Update()
    {
        t += Time.deltaTime * speed;
        transform.position = Vector3.Lerp(startPoint, endPoint, Mathf.PingPong(t, 1));
    }
}