using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.U2D;

public class BreakableBox : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator box()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine(box());
        }
    }
}
