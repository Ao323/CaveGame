using UnityEngine;
using UnityEngine.InputSystem;

public class Sprite : MonoBehaviour
{
    public float speed = 1f;

    private Vector2 direction;
    private Rigidbody2D rb;

    InputAction moveAction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // get the rigidbody2d
        direction = new Vector2();

        moveAction = InputSystem.actions.FindAction("Move");

    }

    // Update is called once per frame
    void Update()
    {
        // get the movement input
        direction = moveAction.ReadValue<Vector2>();
        Debug.Log("Direction: " + direction);
    }

    private void FixedUpdate()
    {
        if (rb)
        {
            // move the controller
            rb.MovePosition(rb.position + (direction.normalized * speed * Time.fixedDeltaTime));

        }
        else { Debug.Log("Physics is missing Rigidbody 2D"); }
    }

}