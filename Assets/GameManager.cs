using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using UnityEngine.XR;
using static UnityEngine.Rendering.VirtualTexturing.Debugging;

public enum GameState { None, TitleMenu, Playing}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public LevelManager S;
    
    //UI
    public int LivesLeft = 3;
    public int ScorePoint = 0;

    public int time = 60;
    public GameState currentState = GameState.TitleMenu;
    

    //Text scoreMessage;


    private void Awake()
    {
        if ((Instance != null) && (Instance != this))
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        
    }



    void Start()
    {
        StartCoroutine(Countdown());
    }

    // Update is called once per frame
    void Update()
    {
        //scoreMessage.text = "Score: " + ScorePoint;
        if (time <= 0) {
            LevelManager.S.levelEvent_RELOAD_LEVEL();
            time = 60;
            lostLife();
        }

        if (Keyboard.current.escapeKey.wasPressedThisFrame) 
        {
            LevelManager.S.levelEvent_RETURN_TO_MENU();
        }
    }

    public IEnumerator Countdown()
    {
        //Text countDown = GameObject.Find("TimerController").GetComponent<Text>();

        while (time >= 0)
        {
            //countDown.text = time.ToString();
            yield return new WaitForSeconds(1.0f);
            if (currentState != GameState.TitleMenu)
                time--;
        }
    }

    public void lostLife()
    {
        if (LivesLeft <= 1) {
            LevelManager.S.LoseScreen();
            GameManager.Instance.currentState = GameState.TitleMenu;
        } else { 
            LivesLeft--;
        }
    }

    public void newLife(int life)
    {
        LivesLeft = life;
    }

    public void ScoreGained()
    {
        ScorePoint = ScorePoint + 1;
    }
}
