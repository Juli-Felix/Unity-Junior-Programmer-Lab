using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed = 10f;
    public InputAction moveAction;
    public Vector2 moveInput;
    private Rigidbody playerRb;
    private float zBound = 9f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        moveAction.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        moveInput = moveAction.ReadValue<Vector2>();
        ConstraintPlayer();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        float horizontalInput = moveInput.x;
        float verticalInput = moveInput.y;

        Vector3 moveDirection = new Vector3(horizontalInput, 0f, verticalInput);

        playerRb.AddForce(moveDirection * speed);
    }

    private void ConstraintPlayer()
    {
        if (transform.position.z < -zBound)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -zBound);
            playerRb.linearVelocity = new Vector3(playerRb.linearVelocity.x, playerRb.linearVelocity.y, 0);
        }

        if (transform.position.z > zBound)
        {
            
            transform.position = new Vector3(transform.position.x, transform.position.y, zBound);
            playerRb.linearVelocity = new Vector3(playerRb.linearVelocity.x, playerRb.linearVelocity.y, 0);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("The player has collided with the enemy");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Powerup"))
        {
            Destroy(other.gameObject);
        }
    }
}
