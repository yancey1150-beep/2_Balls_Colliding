using UnityEngine;
using UnityEngine.InputSystem;

public class BallController : MonoBehaviour
{
    public float speed = 10f;
    
    private Rigidbody rb;
    private Vector2 movementInput;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // The PlayerInput component calls this automatically when you use WASD/Arrows/Gamepad
    void OnMove(InputValue value)
    {
        // Capture the 2D input (X and Y) and store it
        movementInput = value.Get<Vector2>();
    }

    void FixedUpdate() 
    {
        // Convert the 2D input into 3D space (X for left/right, Z for forward/back)
        Vector3 movement = new Vector3(movementInput.x, 0.0f, movementInput.y);

        // Apply physics force
        rb.AddForce(movement * speed);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            Debug.Log("The balls collided!");
        }
    }
}
