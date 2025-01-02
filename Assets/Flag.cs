using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Flag : MonoBehaviour
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

    private IEnumerator waitreturn()
    {
        yield return new WaitForSeconds(1f);
        LevelManager.S.WinScreen();
        GameManager.Instance.currentState = GameState.TitleMenu;
    }

    private IEnumerator waitnew()
    {
        yield return new WaitForSeconds(1f);
        LevelManager.S.levelEvent_END_OF_LEVEL();
        GameManager.Instance.currentState = GameState.Playing;
    }

    private IEnumerator waitmenu()
    {
        yield return new WaitForSeconds(1f);
        LevelManager.S.levelEvent_RETURN_TO_MENU();
        GameManager.Instance.currentState = GameState.TitleMenu;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            
            if (SceneManager.GetActiveScene().buildIndex == 4)
            {
                StartCoroutine(waitreturn());
            }
            else if (SceneManager.GetActiveScene().buildIndex == 1)
            {
                StartCoroutine(waitmenu());
            }
            else {
                StartCoroutine(waitnew());
                GameManager.Instance.time = 60;
            } 
        }
    }
}
