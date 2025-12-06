using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class Elevator2D : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float speed = 2f;
    public float waitTime = 0.2f;

    private Rigidbody2D rb;
    private Vector2 target;
    private bool goingToB = true;
    private float waitTimer = 0f;

    public Vector2 PlatformVelocity { get; private set; }
    private Vector2 lastPos;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
        target = pointB.position;
        lastPos = rb.position;
    }

    void FixedUpdate()
    {
        if (waitTimer > 0f)
        {
            waitTimer -= Time.fixedDeltaTime;
            PlatformVelocity = Vector2.zero;
            lastPos = rb.position;
            return;
        }

        Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
        rb.MovePosition(newPos);

        PlatformVelocity = (newPos - lastPos) / Time.fixedDeltaTime;
        lastPos = newPos;

        if (Vector2.Distance(newPos, target) < 0.01f)
        {
            goingToB = !goingToB;
            target = goingToB ? (Vector2)pointB.position : (Vector2)pointA.position;
            waitTimer = waitTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (!col.collider.CompareTag("Player")) return;

        
        foreach (var contact in col.contacts)
        {
            if (contact.normal.y < -0.5f)
            {
                col.collider.transform.SetParent(transform);
                break;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.collider.CompareTag("Player"))
            col.collider.transform.SetParent(null);
    }
}
