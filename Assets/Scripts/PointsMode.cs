using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PointsMode : MonoBehaviour
{
    public GameObject[] pointsArray;
    private GameObject[] sortingArray;
    public AudioSource sound_Up;
    public AudioSource sound_Down;

    public bool running;
    public int playerRank = 0;

    void OnEnable()
    {
        Debug.Log("Points running");
        InvokeRepeating("Compare", 1.0f, 0.1f);
    }
    //            
    private void Compare()
    {
        sortingArray = pointsArray.OrderBy(entry => -entry.GetComponent<LeaderboardEntry>().score).ToArray();
        int index = 0;
        foreach (GameObject obj in sortingArray)
        {
            var rt = obj.GetComponent<RectTransform>();
            var pos = rt.position;
            obj.GetComponent<LeaderboardEntry>().ranking = index;
            pos.y = 865 + (-105 * index);
            pos.z = 1;
            rt.position = pos;
            if (obj.GetComponent<LeaderboardEntry>().player == true)
            {
                FeedbackSound(index);
            }
            index++;
        }

    }

    private void FeedbackSound(int rank)
    {
        if (playerRank != rank)
        {
            //    Debug.Log("I should be rank " + rank + " but I am currently rank " + playerRank);
            if (playerRank > rank)
            {
                sound_Up.Play();
                playerRank = rank;
                Debug.Log(playerRank);
                LogScript.WriteNewLogEntry("Points", "Rank up", rank.ToString());
            }
            else
            {
                sound_Down.Play();
                playerRank = rank;

                Debug.Log(playerRank);
                LogScript.WriteNewLogEntry("Points", "Rank down", rank.ToString());
            }

        }
        //        Debug.Log("I am rank " + rank + " and have updated to " + playerRank);
    }


    public void AIbot()
    {
        Debug.Log("AI points have started");
        float randomTimer = Random.Range(3f, 5f);
        int index = 0;
        int randomOutcome;
        foreach (GameObject obj in sortingArray)
        {
            if (obj.GetComponent<LeaderboardEntry>().player != true)
            {

                randomOutcome = Random.Range(0, 3);
                switch (randomOutcome)
                {
                    case (0):
                        obj.GetComponent<LeaderboardEntry>().UpdateScore(-10);
                        break;
                    case (1):
                        obj.GetComponent<LeaderboardEntry>().UpdateScore(5);
                        break;
                    case (2):
                        obj.GetComponent<LeaderboardEntry>().UpdateScore(15);
                        break;

                    default:
                        break;
                }
            }
            index++;
        }
        Invoke("AIbot", Random.Range(3.0f, 5.0f));

    }
}
/*
*/