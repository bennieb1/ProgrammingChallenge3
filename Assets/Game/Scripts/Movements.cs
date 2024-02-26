using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;




public class Movements : MonoBehaviour
{
    private PlayerControls controls;
    private Vector2 moveInput; // This will store the movement input for the D-Pad
    private Rigidbody2D rb;
    public float speed = 5.0f;
    public float jumpForce = 7.0f;
    public TextMeshProUGUI treasureCounterText;
    public Animator animator;
    public LayerMask groundLayer;
    public float groundCheckRadius = 0.2f;
    public bool played = false;
    private int treasureCount = 0;

    void Awake()
    {
        controls = new PlayerControls();

        // Subscribe to the performed and canceled events for the Move action
        controls.Movement.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        controls.Movement.Move.canceled += ctx => moveInput = Vector2.zero;
        controls.Movement.Move.performed += ctx => Jump();

        rb = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        controls.Enable(); // Enable all input actions
    }

    void OnDisable()
    {
        controls.Disable(); // Disable all input actions
    }

    void Start()
    {
        UpdateTreasureCounter();
    }

    void Update()
    {
        MovePlayer();
        // Jump and Attack can be handled here if needed, using the new Input System
    }

    void MovePlayer()
    {
        // Now using moveInput to move the player
        rb.velocity = new Vector2(moveInput.x * speed, rb.velocity.y);

        // Set animation parameters
        animator.SetBool("IsWalking", Mathf.Abs(moveInput.x) > 0);
        animator.SetBool("IsIdle", moveInput.x == 0);
    }

    void Jump()
    {
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        animator.SetBool("IsJumping", true);

        
        
    }

    void Attack()
    {
        
        //animator.SetBool("IsAttacking", true); // Set the trigger to play the attack animation


            animator.SetTrigger("Attack");


        // Reset 'isAttacking' in an animation event at the end of the attack animation
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Lava") || other.gameObject.CompareTag("DeathZone"))
        {
            StartCoroutine(DelayedDie(.1f)); // Call the coroutine with a 2-second delay
        }else if (other.gameObject.CompareTag("EndZone"))
        {
            if (!played)
            {
                played = true;
                StartCoroutine(DelayedDie(1000f));
            }
                
           
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            animator.SetBool("IsJumping", false);
        }
    }

   

    IEnumerator DelayedDie(float delay)
    {
        yield return new WaitForSeconds(delay); // Wait for 'delay' seconds
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload the scene
    }

    public void CollectTreasure()
    {
        treasureCount++; // Increase the count by 1
        UpdateTreasureCounter();
    }

    void UpdateTreasureCounter()
    {
        treasureCounterText.text = "Treasure: " + treasureCount; // Update the TextMeshPro text
    }
}
