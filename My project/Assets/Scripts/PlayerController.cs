using System;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public int maxJumps = 1;
    public int maxDash = 1;
    public float dashSpeed = 20f;
    public float dashDuration = 0.2f;

    private int jumpCount;
    private int dashCount;
    private Rigidbody2D rb;
    private bool isGrounded;
    private bool isDashing;
    private float dashTimeLeft;
    private Vector2 dashDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        jumpCount = maxJumps;
        dashCount = maxDash;
    }

    void Update()
    {
        if (!isDashing)
        {
            Move();
            if (Input.GetButtonDown("Jump"))
            {
                Jump();
            }
            if (Input.GetButtonDown("Fire3")) // Fire3 é normalmente o botão "Shift Esquerdo" ou "Ctrl Esquerdo"
            {
                StartDash();
            }
        }
        else
        {
            Dash();
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

    void StartDash()
    {
        isDashing = true;
        dashTimeLeft = dashDuration;

        float moveInputX = Input.GetAxis("Horizontal");
        float moveInputY = Input.GetAxis("Vertical");

        // Definir a direção do dash com base nos inputs horizontal e vertical
        dashDirection = new Vector2(moveInputX, moveInputY).normalized;
    }

    void Dash()
    {
        if (dashTimeLeft > 0 && dashCount > 0)
        {
            rb.velocity = dashDirection * dashSpeed;
            dashTimeLeft -= Time.deltaTime;
            dashCount--;
        }
        else
        {
            isDashing = false;
        }
    }

    void DieAndRespawn()
    {
        // Lógica para "morrer" e respawnar
        Debug.Log("Player collided with hazard and will respawn.");
        FindObjectOfType<RespawnController>().TriggerRespawn();
    }
}
