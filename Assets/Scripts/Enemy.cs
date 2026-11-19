using UnityEngine;

public class Enemy : Character
{
    [Header("Movimiento")]
    [SerializeField] private float speed = 3.5f;
    [SerializeField] private bool startFacingRight = true;
    [SerializeField] private Rigidbody2D rb;

    [Header("Daño al Player")]
    [SerializeField, Min(0f)] private float contactDamage = 20f;
    [SerializeField] private float damageCooldown = 1f;

    private bool isFacingRight;
    private float nextDamageTime;

    private new void Awake()
    {
        base.Awake();
        isFacingRight = startFacingRight;
        SyncScaleDirection();
    }

    private void FixedUpdate()
    {
        if (IsDead || rb == null) return;

        float direction = isFacingRight ? 1f : -1f;
        rb.velocity = new Vector2(direction * speed, rb.velocity.y);
    }

    public void SetDirection(bool faceRight)
    {
        isFacingRight = faceRight;
        SyncScaleDirection();
    }

    private void SyncScaleDirection()
    {
        Vector3 ls = transform.localScale;
        ls.x = Mathf.Abs(ls.x) * (isFacingRight ? 1f : -1f);
        transform.localScale = ls;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        TryDamagePlayer(collision.collider);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        TryDamagePlayer(collision);
    }

    private void TryDamagePlayer(Collider2D targetCollider)
    {
        if (IsDead || Time.time < nextDamageTime) return;

        if (targetCollider.CompareTag("Player"))
        {
            Character playerCharacter = targetCollider.GetComponentInParent<Character>();
            if (playerCharacter != null && !playerCharacter.IsDead)
            {
                playerCharacter.TakeDamage(contactDamage);
                nextDamageTime = Time.time + damageCooldown;
            }
        }
    }

    protected override void Die()
    {
        rb.velocity = Vector2.zero;
        gameObject.SetActive(false);
    }
}