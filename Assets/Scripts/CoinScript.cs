using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinScript : MonoBehaviour
{
    private int coins;
    private void Awake()
    {
        if (PlayerPrefs.HasKey("Coins"))
        {
            coins = PlayerPrefs.GetInt("Coins");
        }
        else
        {
            coins = 0;
        }
    }


    [SerializeField] private GameObject coinDisplay;

    private void RefreshCoinDisplay()
    {
        coinDisplay.GetComponent<TextMeshProUGUI>().text = "Coins: " + coins;
    }


    public void BuyHunter()
    {
        if (coins >= 100)
        {
            RefreshCoinDisplay();
            PlayerPrefs.SetInt("Coins", coins -= 100);
        }
    }
}
