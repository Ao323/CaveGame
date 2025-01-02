using UnityEngine;
using UnityEngine.U2D;

public class CoinScript : MonoBehaviour
{
    private SpriteRenderer sprite;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Collider2D[] colliders = GetComponents<Collider2D>();
            foreach (Collider2D collider in colliders) { collider.enabled = false; }
            SoundManager.S.coin();
            GameManager.Instance.ScoreGained();
            sprite.enabled = false;
        }

    }
}
