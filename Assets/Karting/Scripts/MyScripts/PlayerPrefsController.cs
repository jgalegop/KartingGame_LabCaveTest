using UnityEngine;

public class PlayerPrefsController
{
    // constants
    const string BEST_TIME_KEY = "Best Time";
    const string MOST_COINS_KEY = "Most Coins";

    // get - set
    public static float GetBestTime()
    {
        return PlayerPrefs.GetFloat(BEST_TIME_KEY, 0);
    }

    public static void SetBestTime(float bestTime)
    {
        PlayerPrefs.SetFloat(BEST_TIME_KEY, bestTime);
    }

    public static int GetMostCoins()
    {
        return PlayerPrefs.GetInt(MOST_COINS_KEY, 0);
    }

    public static void SetMostCoins(int mostCoins)
    {
        PlayerPrefs.SetInt(MOST_COINS_KEY, mostCoins);
    }
}
