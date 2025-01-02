using UnityEngine;
using UnityEngine.U2D;

public class CoinLevel : MonoBehaviour
{
    public LevelManager S;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            LevelManager.S.levelEvent_RETURN_TO_MENU();
        }

    }
}
