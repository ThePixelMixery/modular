using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderboardEntry : MonoBehaviour
{
    public int ranking;
    public int score;


    public void UpdateRank(int NewRank)
    {
        ranking = NewRank;
    }
    public void UpdateScore( int NewScore)
    {
        score = NewScore;
    }
}
