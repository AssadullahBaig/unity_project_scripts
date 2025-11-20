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
    private bool facingRight;
    private Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isGrounded = true;
        facingRight = true;
        animator = this.gameObject.GetComponent<Animator>();

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
        Flip();
        PlayRunAnimation();

    }
    private void FixedUpdate()
    {
        transform.position += new Vector3(movement * movespeed, 0f, 0f) * Time.fixedDeltaTime;
    }

    void PlayRunAnimation()
    {
        if (Mathf.Abs(movement) > 0f)
        {
            animator.SetFloat("Run", 1f);
        }
        else if(movement < 0.1f)
        {
            animator.SetFloat("Run", 0f);
        }
    }  

    void Flip()
    {
        if (movement < 0f && facingRight == true)
        {
            transform.eulerAngles = new Vector3(0f, -180f, 0f);
            facingRight = false;

        }
        else if (movement > 0f && facingRight == false)
        {
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
            facingRight = true;

        }
       
        
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
