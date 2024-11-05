using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LivesCanvas : MonoBehaviour
{
    public static LivesCanvas instance;

    [SerializeField] private GameObject healthBar;
    [SerializeField] private GameObject healthNumbers;


    private int maxHealth;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        maxHealth = PlayerPlane.instance.GetComponent<UnitStats>().maxHealth;
        UpdateHealthBar(PlayerPlane.instance.GetComponent<UnitStats>().maxHealth);
    }


    public void UpdateHealthBar(float lives)
    {
        Debug.Log(lives);
        healthBar.GetComponent<Image>().fillAmount = lives / maxHealth;
        healthNumbers.GetComponent<TextMeshProUGUI>().text = lives + " / " + maxHealth;
    }
}
