using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.U2D;

public class EnemyScript2 : MonoBehaviour
{
    public float speed;
    public bool faceLeft = false;

    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Animator animator;

    private bool isAlive = true;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (faceLeft == sprite.flipX) { sprite.flipX = !sprite.flipX; }
        FixedUpdate();
    }

    private void FixedUpdate()
    {
        if (isAlive)
        {
            float direction = faceLeft ? -1f : 1f;
            rb.velocity = new Vector2(speed * direction, rb.velocity.y);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "TurnAround")
        {
            faceLeft = !faceLeft; // toggle the faceleft value
        }

        
        if (collision.gameObject.tag == "Player")
        {

            rb.isKinematic = true;
            rb.velocity = Vector3.zero;

            Collider2D[] colliders = GetComponents<Collider2D>();
            foreach (Collider2D collider in colliders) { collider.enabled = false; }

            sprite.enabled = false;
            isAlive = false;
            SoundManager.S.kill();
        }

        if (collision.gameObject.tag == "Rock")
        {
            Destroy(gameObject);
            SoundManager.S.kill();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            animator.SetTrigger("Attack");
            SoundManager.S.EnemyHit();
            GameManager.Instance.lostLife();
        }
    }
}
