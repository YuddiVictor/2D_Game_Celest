using System;
using System.Collections;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class PlayerController : MonoBehaviour
{

    [Header("Movement")]
    [SerializeField] public float moveSpeed = 5f;
    private Rigidbody2D rb;

    [Header("Jump")]
    [SerializeField] public int maxJumps = 1;
    [SerializeField] public float jumpForce = 10f;
    [SerializeField] private bool isGrounded = true;
    private int jumpCount;

    [Header("Dashing")]
    [SerializeField] public float dashSpeed = 20f;
    [SerializeField] public float dashDuration = 0.2f;
    [SerializeField] private int dashCount;
    [SerializeField] public int maxDash = 1;
    private Vector2 dashDirection;
    [SerializeField] public bool isDashing = false;
    [SerializeField] public bool canDash = true;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        jumpCount = maxJumps;
        dashCount = maxDash;
    }

    void Update()
    {

        Move();
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }

        if (Input.GetButtonDown("DashInput") && canDash == true)
        {
            Dash();
            if (isDashing == true)
            {
                Dashing();
            }

        }

        void Move()
        {
            float moveInput = Input.GetAxis("Horizontal");
            rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
        }

        void Jump()
        {
            if (Input.GetButtonDown("Jump") && jumpCount > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0);
                rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
                jumpCount--;


                // Notifica o cenário para rotacionar
                FindObjectOfType<ScenarioRotator>().RotateScene();
            }

            if (isGrounded)
            {
                jumpCount = maxJumps;
            }
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Ground"))
            {
                isGrounded = true;
                canDash = true;
                jumpCount = maxJumps;
                dashCount = maxDash;
            }

            // Detecta colisão com o objeto específico para "morrer"
            if (collision.gameObject.CompareTag("Hazard"))
            {
                DieAndRespawn();
            }
        }

        void OnCollisionStay2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Ground"))
            {
                dashCount = maxDash;
            }
        }

        void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Ground"))
            {
                isGrounded = false;
            }
        }

        void Dash()
        {
            isDashing = true;
            canDash = false;
            dashDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            if (dashDirection == Vector2.zero)
            {
                dashDirection = new Vector2(transform.localScale.x, 0);
            }
            StartCoroutine (StopDash());   
        }

        void Dashing()
        {
            rb.velocity = dashDirection.normalized * dashSpeed;
            return;
        }

        IEnumerator StopDash()
        {
            yield return new WaitForSeconds(dashDuration);  
            isDashing = false;
        }


        void DieAndRespawn()
        {
            // Lógica para "morrer" e respawnar
            Debug.Log("Player collided with hazard and will respawn.");
            FindObjectOfType<RespawnController>().TriggerRespawn();
        }

    }
}