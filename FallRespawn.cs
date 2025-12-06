using UnityEngine;

public class FallRespawn : MonoBehaviour
{
    public float killY = -10f;   
    private Vector3 startPos;
    private Rigidbody2D rb;

    void Awake()
    {
        startPos = transform.position; 
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (transform.position.y < killY)
        {
            Respawn();
        }
    }

    void Respawn()
    {
        transform.position = startPos;

      
        if (rb != null)
            rb.linearVelocity = Vector2.zero;
    }
}
