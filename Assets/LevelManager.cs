using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;


public class LevelManager : MonoBehaviour
{
    public static LevelManager S;

    private void Update()
    {

    }

    private void Awake()
    {
        if (S)
        {
            Destroy(this.gameObject);
        }
        else 
        {
            S = this;
            // DontDestroyOnLoad(this.gameObject);
        } // singleton definition
    }

    public void levelEvent_END_OF_LEVEL()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        GameManager.Instance.currentState = GameState.Playing;
    }

    public void levelEvent_RELOAD_LEVEL()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        GameManager.Instance.time = 60;
        GameManager.Instance.currentState = GameState.Playing;
    }

    public void levelEvent_RETURN_TO_MENU()
    {
        SceneManager.LoadScene("OpeningScreen");
        GameManager.Instance.currentState = GameState.TitleMenu;
    }
    public void btn_StartTheGame()
    {
        // when pressed, go to the first level
        SceneManager.LoadScene("SpritesEx");
        GameManager.Instance.LivesLeft = 3;
        GameManager.Instance.ScorePoint = 0;
        GameManager.Instance.time = 60;
        GameManager.Instance.currentState = GameState.Playing;
    }

    public void btn_StartTheTutorial()
    {
        // when pressed, go to the tutorial level
        SceneManager.LoadScene("Tutorial");
        GameManager.Instance.LivesLeft = 3;
        GameManager.Instance.ScorePoint = 0;
        GameManager.Instance.time = 60;
        GameManager.Instance.currentState = GameState.Playing;
    }

    public void btn_StartTheCredits()
    {
        // when pressed, go to the tutorial level
        SceneManager.LoadScene("Credits");
    }

    public void WinScreen()
    {
        // when pressed, go to the tutorial level
        SceneManager.LoadScene("Win");
    }

    public void LoseScreen()
    {
        // when pressed, go to the tutorial level
        SceneManager.LoadScene("Lose");
    }

    public void btn_Quit()
    {
        Application.Quit();
    }

}