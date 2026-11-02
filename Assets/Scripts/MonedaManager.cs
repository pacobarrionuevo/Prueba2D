using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonedaManager : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            UIManager.Instance.AddCoin();
            Destroy(gameObject);
        }
    }
}
