using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using KartGame.Track;

public class BestScoreDisplay : MonoBehaviour
{
    // inspector
    [Tooltip("TextMeshPro object for display best Time")]
    [SerializeField] TextMeshProUGUI bestTime = null;
    [Tooltip("TextMeshPro object for display most coins")]
    [SerializeField] TextMeshProUGUI mostCoins = null;


    void Start()
    {
        // we are only interested in this particular track, the time of one lap
        bestTime.text = "Best time: " + TrackRecord.Load("ArtTest", 1).time.ToString(".##") + " s";
        mostCoins.text = "Most coins: " + PlayerPrefsController.GetMostCoins().ToString();
    }
}
