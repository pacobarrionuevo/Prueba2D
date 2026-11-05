using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float horizontal;
    private float speed = 8f;

    private float jumpPower = 16f;

    private bool isFacingRight = true;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    /////////////////////

    private bool canDash = true;
    private bool isDashing;
    private float dashingPower = 24f;
    private float dashTime = 0.2f;
    private float dashCooldown = 1f;

    [SerializeField] private TrailRenderer tr;

    /////////////////////



    // Update is called once per frame
    private void Update()
    {
        if (isDashing) return;

        Move();
        Jump();
        Flip();
        // ESTO
        CanDash();

        // O ESTO
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }
    }

    private void Move()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;

            Vector3 ls = transform.localScale;
            ls.x *= -1f;
            transform.localScale = ls;
        }
    }

    private void CanDash()
    {
        // Si pulsa Shift y puede dashear, empezamos la corutina
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private int numeroMonedas = 0;

    public static PlayerController Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddCoin()
    {
        numeroMonedas++;
        UIManager.Instance.UpdateCoinText(numeroMonedas);
    }

    ////////////////////////////
    ///

    private IEnumerator Dash()
    {
        // Ha pulsado Dash, es decir, ya está 'dasheando' y no puede otra vez
        canDash = false;
        isDashing = true;

        // Guardamos la gravedad original y la setteamos a 0
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;

        // Cambiamos la velocidad
        rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);

        // Abrimos el trail y contamos la duración
        tr.emitting = true;
        yield return new WaitForSeconds(dashTime);

        // Cuando termine, desactivamos el trail, devolvemos la gravedad original  y ya no dasheamos
        tr.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing = false;

        // Cooldown para evitar Spam
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
}
