using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinManager : MonoBehaviour
{
    // inspector
    [Tooltip("The text displaying the number of coins gathered in the current run")]
    public TextMeshProUGUI coinsNumberText = null;

    int coinsNumber = 0;

    const string TOTAL_COIN_NUMBER_KEY = "total coin number";

    public void PickedUpCoin()
    {
        coinsNumber++;
        UpdateCoinsNumberDisplay();
    }

    void Start()
    {
        UpdateCoinsNumberDisplay();
    }

    void UpdateCoinsNumberDisplay()
    {
        coinsNumberText.text = "Coins " + coinsNumber.ToString();
    }

    public void SetCoinsNumber()
    {
        int oldCoinsNumber = PlayerPrefs.GetInt(TOTAL_COIN_NUMBER_KEY, 0);
        PlayerPrefs.SetInt(TOTAL_COIN_NUMBER_KEY, oldCoinsNumber + coinsNumber);
    }

    public static int GetCoinsNumber()
    {
        return PlayerPrefs.GetInt(TOTAL_COIN_NUMBER_KEY, 0);
    }
}
