using UnityEngine;

public class DeadZoneManager : MonoBehaviour
{
    [SerializeField] private Transform spawnLocation;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerController>().transform.position = spawnLocation.position;
        }

    }
}
