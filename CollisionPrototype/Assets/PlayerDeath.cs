using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("dead");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
