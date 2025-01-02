using UnityEngine;
using UnityEngine.InputSystem;

public class SoundManager : MonoBehaviour
{
    public static SoundManager S; 

    [SerializeField]
    private AudioSource backgroundMusic;

    public AudioClip enemyHitClip;
    public AudioClip enemyHit2Clip;
    public AudioClip coinClip;
    public AudioClip stunClip;
    public AudioClip killClip;
    public AudioSource walkingClip;

    private void Awake()
    {
        S = this;
    }

    private void Update()
    {
        if ((Input.GetKeyDown(KeyCode.RightArrow)) || (Input.GetKeyDown(KeyCode.LeftArrow)))
        {
            walkingClip.Play();
        }
        else {
            walkingClip.Stop();
        }

    }

    public void StopBackgroundMusic()
    {
        backgroundMusic.Stop();
    }

    public void StartBackgroundMusic()
    {
        backgroundMusic.Play();
    }

    public void EnemyHit()
    {
        backgroundMusic.PlayOneShot(enemyHitClip);
    }

    public void Enemy2Hit()
    {
        backgroundMusic.PlayOneShot(enemyHit2Clip);
    }

    public void coin()
    {
        backgroundMusic.PlayOneShot(coinClip);
    }

    public void stun()
    {
        backgroundMusic.PlayOneShot(stunClip);
    }

    public void kill()
    {
        backgroundMusic.PlayOneShot(killClip);
    }

}
