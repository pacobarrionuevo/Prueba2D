using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] private TextMeshProUGUI text;

    private int numeroMonedas = 0;

    public void AddCoin()
    {
        numeroMonedas++;
        Debug.Log(numeroMonedas);
    }
}
