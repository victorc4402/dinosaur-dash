using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class Player : MonoBehaviour
{
    public float jumpForce = 10f;
    private Rigidbody2D rb;
    private bool isGrounded;
    
    public int score = 0;
    public int scoreInterval = 1;
    public int scorePerTick = 10;
    private float scoreTimer = 0f;
    private bool isScoring = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Keyboard.current.spaceKey.isPressed && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;
            AudioManager.Instance.PlaySoundEffect("jump"); 
        }

        if (isScoring)
        {
            scoreTimer += Time.deltaTime;
            while (scoreTimer >= scoreInterval)
            {
                score += scorePerTick;
                scoreTimer -= scoreInterval;
            }
        }
        Debug.Log("Score: " + score);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        isGrounded = true;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Obstacle"))
        {
            isScoring = false;
            Debug.Log("Final Score: " + score);
            AudioManager.Instance.StopMusic();
            AudioManager.Instance.PlaySoundEffect("death");
            SceneManager.LoadScene("deadasf");
        }
    }
}
