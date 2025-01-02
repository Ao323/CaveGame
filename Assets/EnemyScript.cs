using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.U2D;
using UnityEngine.UI;

public class EnemyScript : MonoBehaviour
{
    public float speed;
    public bool faceLeft = false;

    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Animator animator;

    private bool isAlive = true;

    public Controller2D contr;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //contr = FindFirstObjectByType<Controller2D>();
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

    public IEnumerator stunnedEnemy() 
    {
        yield return new WaitForSeconds(1f);
        sprite.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "TurnAround")
        {
            faceLeft = !faceLeft; // toggle the faceleft value
        }

        if (collision.gameObject.tag == "Player")
        {
            animator.SetTrigger("IsHurt");
             
            rb.isKinematic = true;
            rb.velocity = Vector3.zero;

            Collider2D[] colliders = GetComponents<Collider2D>();
            foreach (Collider2D collider in colliders) { collider.enabled = false; }

            StartCoroutine(stunnedEnemy());

            isAlive = false;
            SoundManager.S.kill();
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            SoundManager.S.stun();
            contr.stunnedPlayer();
        }
    }
}
