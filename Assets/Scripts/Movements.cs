using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;




public class Movements : MonoBehaviour
{
    public float speed = 5.0f;
    public float jumpForce = 7.0f;



    public int treasureCount = 0;
    public TextMeshProUGUI treasureCounterText;
    public Animator animator;
    public bool played = false;
    public LayerMask groundLayer;
    public float groundCheckRadius = 0.2f;

    private Rigidbody2D rb;

    private bool isAttacking = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        UpdateTreasureCounter();
    }

    void Update()
    {
        MovePlayer();
        if (Input.GetButtonDown("Jump") && !isAttacking)
        {
            Jump();
        }
        if (Input.GetButtonDown("Fire1"))
        {
       
            Attack();
            
            
        }

        // Update the IsJumping animation parameter
        animator.GetBool("IsJumping");

        
    }

    void MovePlayer()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveHorizontal * speed, rb.velocity.y);

        // Set animation parameters
        animator.SetBool("IsWalking", Mathf.Abs(moveHorizontal) > 0);
        if (moveHorizontal > 0)
        {
            animator.SetBool("IsIdle", false);
            
        }

        if (moveHorizontal == 0)
        {
            animator.SetBool("IsIdle", true);
        }
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
