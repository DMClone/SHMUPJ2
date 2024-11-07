using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinScript : MonoBehaviour
{
    [SerializeField] private int coins;
    [SerializeField] private GameObject hunterField;
    [SerializeField] private GameObject builderField;
    private void Awake()
    {
        if (PlayerPrefs.HasKey("Coins"))
        {
            coins = PlayerPrefs.GetInt("Coins");
        }
        RefreshCoinDisplay();

        if (PlayerPrefs.GetString("SelectedClass") == "Hunter")
        {
            hunterField.GetComponent<ClassButton>().SelectHunter();
        }
        else if (PlayerPrefs.GetString("SelectedClass") == "Builder")
        {
            builderField.GetComponent<ClassButton>().SelectBuilder();
        }
    }

    [SerializeField] private GameObject coinDisplay;

    private void RefreshCoinDisplay()
    {
        coinDisplay.GetComponent<TextMeshProUGUI>().text = "Coins: " + coins;
    }


    public bool BuyHunter()
    {
        if (coins >= 100)
        {
            Debug.Log("Hunter Bought");
            coins -= 100;
            RefreshCoinDisplay();
            PlayerPrefs.SetInt("Coins", coins);
            return true;
        }
        return false;
    }

    public bool BuyBuilder()
    {
        if (coins >= 200)
        {
            Debug.Log("Builder Bought");
            coins -= 200;
            RefreshCoinDisplay();
            PlayerPrefs.SetInt("Coins", coins);
            return true;
        }
        else
        {
            return false;
        }
    }

    public void HunterSelected()
    {
        if (PlayerPrefs.HasKey("UnlockedBuilder"))
        {
            builderField.GetComponent<ClassButton>().DeSelectBuilder(false);
        }
    }

    public void BuilderSelected()
    {
        if (PlayerPrefs.HasKey("UnlockedHunter"))
        {
            hunterField.GetComponent<ClassButton>().DeSelectHunter(false);
        }
    }
}
