using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverCanvas : MonoBehaviour
{
    [Tooltip("Text Mesh Pro UGUI object in canvas where your current time is displayed")]
    public TextMeshProUGUI yourTimeDisplay;
    [Tooltip("Text Mesh Pro UGUI object in canvas where the best historical time is displayed")]
    public TextMeshProUGUI bestTimeDisplay;

    public void SetScore(float yourTime, float bestTime)
    {
        yourTimeDisplay.text = "Your time: " + yourTime.ToString(".##") + " s";
        bestTimeDisplay.text = "Best time: " + bestTime.ToString(".##") + " s";
    }
}
