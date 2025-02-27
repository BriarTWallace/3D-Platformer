using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.XInput;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public PlayerStats playerStats;  // Reference to the PlayerStats ScriptableObject
    private Rigidbody rb;            // Player's Rigidbody for physics-based movement
    private bool isGrounded;         // To check if the player is on the ground
    private float moveSpeed = 5f;    // Speed at which the player moves
    [SerializeField] float jumpForce = 10f;   // Jump force to apply when the player jumps
    private float groundCheckRadius = 0.3f;  // Radius for ground detection
    public float turnSpeed = 700f;

    // Ground check layer mask (to check the ground)
    public LayerMask groundLayer;

    [SerializeField] Animator myAnimator;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (playerStats != null)
        {
            playerStats.Initialize();
        }
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void Update()
    {
        // Handle jumping
        Jump();

        // Check if player health is zero or less
        if (playerStats.currentHealth <= 0)
        {
            GameOver();
        }
    }


    private void MovePlayer()
    {
        // Get input for movement
        float moveHorizontal = Input.GetAxis("Horizontal"); // A/D or Left/Right Arrow
        float moveVertical = Input.GetAxis("Vertical");     // W/S or Up/Down Arrow


        // Make sure the player moves based on camera orientation
        Vector3 cameraForward = Camera.main.transform.forward;
        cameraForward.y = 0;  // Ignore vertical direction
        cameraForward.Normalize();

        Vector3 cameraRight = Camera.main.transform.right;
        cameraRight.y = 0;  // Ignore vertical direction
        cameraRight.Normalize();

        // Combine camera direction with movement inputs
        Vector3 movementDirection = (cameraForward * moveVertical + cameraRight * moveHorizontal).normalized;

        rb.MovePosition(transform.position + movementDirection * moveSpeed * Time.deltaTime);
        
        if( moveHorizontal != 0 || moveVertical != 0 )
        { 
             myAnimator.SetBool("isRunning", true);
        }
        else
        {
            myAnimator.SetBool("isRunning", false);
        }

        if (movementDirection.magnitude >= 0.1f)
        {
            // Calculate the rotation agle based on input
            float targetAngle = Mathf.Atan2(movementDirection.x, movementDirection.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSpeed, 0.1f);

            // Rotate character
            transform.rotation = Quaternion.Euler(0, angle, 0);


            myAnimator.SetFloat("Speed", movementDirection.magnitude);
        }
        else
        {
            myAnimator.SetFloat("Speed", 0f);
        }

    }

    private void Jump()
    {
        // Check if the player is grounded and if they pressed the jump button
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            myAnimator.SetTrigger("Jump");
        }
        
    }

    private void OnCollisionStay(Collision other)
    {
        // Check if the player is touching the ground
        
           isGrounded = true;
        
    }

    private void OnCollisionExit(Collision other)
    {
        //if (other.gameObject.CompareTag("Ground"))
        
           isGrounded = false;
        
    }

    

    // Function to apply damage to the player
   public void TakeDamage(int damage)
    {
        if (playerStats != null)
        {
            playerStats.TakeDamage(damage);
        }
    }

    // Function to handle game over 
    private void GameOver()
    {
        Debug.Log("Game Over!");
        SceneManager.LoadScene("GameOver");
    }
}
