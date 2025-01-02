using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.U2D;
using UnityEngine.UI;

public class Animation : MonoBehaviour
{
    private Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Keyboard.current.rightArrowKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
        {
            animator.ResetTrigger("IsIdle");
            animator.SetBool("IsRunning", true);
        }
        else if (!Input.anyKey)
        {
            animator.SetBool("IsRunning", false);
            animator.SetTrigger("IsIdle");
        }
        else if (Keyboard.current.spaceKey.isPressed)
        {
            animator.SetBool("IsRunning", false);
            animator.SetTrigger("IsJumping");
        }
        
    }
    
    public IEnumerator hurtPlayer()
    {
        animator.SetTrigger("Hurt");
        yield return new WaitForSeconds(2f);
        animator.SetTrigger("IsIdle");
    }
    
}
