using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

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

    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private TextMeshProUGUI timeText;

    private float time = 0f;

    public void UpdateCoinText(int numeroMonedas) 
    {
        coinText.text = numeroMonedas.ToString();
    }

    private void Update()
    {
        CalculateTime();
    }

    private void CalculateTime()
    {
        time += Time.deltaTime;

        float minutos = Mathf.FloorToInt(time / 60);
        float segundos = Mathf.FloorToInt(time % 60); // % se escribe dandole a Shift 5

        timeText.text = minutos + ":" + segundos;
    }
}
