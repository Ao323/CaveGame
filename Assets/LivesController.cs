using UnityEngine;
using UnityEngine.UI;

public class LivesController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Text lives = gameObject.GetComponent<Text>();
        lives.text = "Lives: " + GameManager.Instance.LivesLeft.ToString();
    }
}