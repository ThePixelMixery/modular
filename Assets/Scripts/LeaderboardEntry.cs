using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LeaderboardEntry : MonoBehaviour
{
    public int ranking;
    public int score;
    public bool player;
    public TextMeshProUGUI scoreText;

    public void UpdateRank(int NewRank)
    {
        ranking = NewRank;
    }
    public void UpdateScore( int NewScore)
    {
        score = +NewScore;
        scoreText.text = score.ToString();
    }
}
