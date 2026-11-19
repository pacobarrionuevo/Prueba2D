using UnityEngine;

public class DeadZoneManager : MonoBehaviour
{
    [SerializeField] private Transform spawnLocation;
    [SerializeField, Min(0f)] private float damage = 25f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerController>().transform.position = spawnLocation.position;

            Character target = collision.GetComponentInParent<Character>();

            if (target != null)
            {
                target.TakeDamage(damage);
            }
        }

    }
}
