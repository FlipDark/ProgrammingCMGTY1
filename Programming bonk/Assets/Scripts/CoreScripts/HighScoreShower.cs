using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HighScoreShower : MonoBehaviour
{
    public TMP_Text finalScoreText;
    void Start()
    {
        int finalScore = PlayerPrefs.GetInt("FinalScore", 0);
        finalScoreText.text = "Final Score: " + finalScore;
    }
}