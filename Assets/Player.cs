using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerStats playerStats;  // Reference to the PlayerStats ScriptableObject
    private Rigidbody rb;            // Player's Rigidbody for physics-based movement
    private bool isGrounded;         // To check if the player is on the ground
    private float moveSpeed = 5f;    // Speed at which the player moves
    private float jumpForce = 10f;   // Jump force to apply when the player jumps
    private float groundCheckRadius = 0.3f;  // Radius for ground detection

    // Ground check layer mask (to check the ground)
    public LayerMask groundLayer;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (playerStats != null)
        {
            playerStats.Initialize();
        }
    }

    private void Update()
    {
        // Handle player movement
        MovePlayer();
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

        // Move the player in the direction of the camera
        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical) * moveSpeed * Time.deltaTime;

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
    }

    private void Jump()
    {
        // Check if the player is grounded and if they pressed the jump button
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void OnCollisionStay(Collision other)
    {
        // Check if the player is touching the ground
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if player is colliding with a hazard
        if (other.CompareTag("Hazard"))
        {
            Hazard hazard = other.GetComponent<Hazard>();
            if (hazard != null)
            {
                TakeDamage(hazard.damageAmount);  // Apply damage from the hazard
            }
        }

        // Check if player has touched the goal
        if (other.CompareTag("Goal"))
        {
            Goal goal = other.GetComponent<Goal>();
            if (goal != null)
            {
                // Proceed to next level (can be modified to match your scene flow)
                goal.CompleteGoal();
            }
        }
    }

    // Function to apply damage to the player
    public void TakeDamage(int damage)
    {
        if (playerStats != null)
        {
            playerStats.TakeDamage(damage);
        }
    }

    // Function to handle game over (e.g., load Game Over scene)
    private void GameOver()
    {
        Debug.Log("Game Over!");
        // Uncomment this to load a game over scene
        // SceneManager.LoadScene("GameOverScene");
    }
}
