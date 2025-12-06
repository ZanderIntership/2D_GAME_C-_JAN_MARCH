using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelStar : MonoBehaviour
{
    public string nextSceneName;
    public float loadDelay = 0.2f;

    private bool collected;
    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position; 
    }

    void Update()
    {
        if (collected) return; 


        float bob = Mathf.Sin(Time.time * 3f) * 0.1f;  
        transform.position = startPos + Vector3.up * bob;

        // Spin
        transform.Rotate(0, 0, 120 * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (collected) return;
        if (!other.CompareTag("Player")) return;

        collected = true;

        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;

        Invoke(nameof(CompleteLevel), loadDelay);
    }

    void CompleteLevel()
    {
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            int i = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(i + 1);
        }
    }
}
