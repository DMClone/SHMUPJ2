using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ClassButton : MonoBehaviour
{
    enum classes
    {
        Hunter,
        Builder
    }

    [SerializeField] private classes classOnClick;

    public void ClassButtonClick()
    {
        if (classOnClick == classes.Hunter)
        {
            HunterClick();
        }
        else
        {
            BuilderClick();
        }
    }

    private void HunterClick()
    {
        if (!PlayerPrefs.HasKey("UnlockedHunter"))
        {
            AttemptHunterPurchase();
        }
        else if (PlayerPrefs.GetString("SelectedClass") == "Hunter")
        {
            DeSelectHunter(true);
        }
        else
        {
            SelectHunter();
        }
    }

    private void BuilderClick()
    {
        if (!PlayerPrefs.HasKey("UnlockedBuilder"))
        {
            AttemptBuilderPurchase();
        }
        else if (PlayerPrefs.GetString("SelectedClass") == "Builder")
        {
            DeSelectBuilder(true);
        }
        else
        {
            SelectBuilder();
        }
    }

    private void AttemptHunterPurchase()
    {
        if (transform.parent.GetComponent<CoinScript>().BuyHunter())
        {
            PlayerPrefs.SetString("UnlockedHunter", "true");
            SelectHunter();
        }
    }

    public void SelectHunter()
    {
        PlayerPrefs.SetString("SelectedClass", "Hunter");
        transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Equipped";
        transform.parent.GetComponent<CoinScript>().HunterSelected();
    }

    public void DeSelectHunter(bool selectStarter)
    {
        transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Unlocked";
        if (selectStarter)
        {
            PlayerPrefs.SetString("SelectedClass", "Starter");
        }
    }

    private void AttemptBuilderPurchase()
    {
        if (transform.parent.GetComponent<CoinScript>().BuyBuilder())
        {
            PlayerPrefs.SetString("UnlockedBuilder", "true");
            SelectBuilder();
        }
    }

    public void SelectBuilder()
    {
        PlayerPrefs.SetString("SelectedClass", "Builder");
        transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Equipped";
        transform.parent.GetComponent<CoinScript>().BuilderSelected();
    }

    public void DeSelectBuilder(bool selectStarter)
    {
        transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Unlocked";
        if (selectStarter)
        {
            PlayerPrefs.SetString("SelectedClass", "Starter");
        }
    }
}
