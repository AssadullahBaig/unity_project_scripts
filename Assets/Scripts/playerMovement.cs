using UnityEngine;

public class playerMovement : MonoBehaviour
{


    public Rigidbody2D rb; 
    public float jumpHeight = 10f;
    public float movespeed = 5f;
    public Transform groundCheckPosition;
    public float groundCheckRadius = 0.2f;
    public LayerMask whatIsGround;

    private float movement;
    private bool isGrounded;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isGrounded = true;

    }

    // Update is called once per frame
    void Update()
    {
        movement = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump(); 
        }
        Collider2D callInfo = Physics2D.OverlapCircle(groundCheckPosition.position, groundCheckRadius, whatIsGround);
        if (callInfo == true)
        {
            isGrounded = true;
        }

    }
    private void FixedUpdate()
    {
        transform.position += new Vector3(movement * movespeed, 0f, 0f) * Time.fixedDeltaTime;
    }

    void Jump()
    {
        if (isGrounded == true)
        {

            Vector2 velocity = rb.linearVelocity;
            velocity.y = jumpHeight;
            rb.linearVelocity = velocity;
            isGrounded = false;

        }


    }
    private void OnDrawGizmosSelected()
    {
        if (groundCheckPosition == null){
            
            return;
        }
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheckPosition.position, groundCheckRadius);
    }


}
