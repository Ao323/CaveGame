 using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class enemyFollow : MonoBehaviour
{
    public bool faceLeft = false;
    public GameObject player;

    private Rigidbody2D rb;
    private SpriteRenderer sprite;

    [SerializeField]
    private Animator animator;

    public int speedX;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator.SetBool("isIdle", true);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 enemyPosition = transform.position;
        float dist = Vector3.Distance(player.transform.position, transform.position);


        if (dist < 6) 
        {
            animator.SetBool("isIdle", false);
            if (player.transform.position.x > enemyPosition.x + 0.5)
            {
                if (faceLeft == false)
                {
                    transform.Rotate(new Vector3(0, -180, 0));
                }
                transform.Translate(speedX * Time.deltaTime, 0, 0);
                faceLeft = true;
            }
            else if (player.transform.position.x < enemyPosition.x - 0.5)
            {
                if (faceLeft == true)
                {
                    transform.Rotate(new Vector3(0, -180, 0));
                }
                faceLeft = false;
                transform.Translate(speedX * Time.deltaTime, 0, 0);
            }
        }

        if (transform.position.y < -20f)
        {
            Destroy(gameObject);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            SoundManager.S.kill();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            SoundManager.S.Enemy2Hit();
            GameManager.Instance.lostLife();
        }
    }
}
