using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class PlatformMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed = 4f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
        }

        if (collision.CompareTag("Platform"))
        {
            // Shift +
            rb.velocity *= -1f;
        }
    }
}
