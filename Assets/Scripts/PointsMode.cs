using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PointsMode : MonoBehaviour
{
    public GameObject[] pointsArray;

    private GameObject[] sortingArray;

    public AudioSource sound_Up;

    public AudioSource sound_Down;

    public bool running = false;

    public int playerRank = 0;

    void Start()
    {
        sortingArray =
            pointsArray
                .OrderBy(entry => -entry.GetComponent<LeaderboardEntry>().score)
                .ToArray();
        int index = 0;
        foreach (GameObject obj in sortingArray)
        {
            //                if (obj.GetComponent<LeaderboardEntry>().player == true)
            //              {
            //                FeedbackSound (index);
            //          }
            var rt = obj.GetComponent<RectTransform>();
            obj.GetComponent<LeaderboardEntry>().ranking = index;
            var pos = rt.position;
            pos.y = 915 + (-105 * index);
            pos.z = 1;

            Debug
                .Log("Index is at " +
                index +
                ", X: " +
                pos.x +
                ", Y: " +
                pos.y);
            rt.position = pos;

            index++;
        }
        Debug.Log("Points running");
    }

    public void RunIt()
    {
        running = true;
    }

    //
    private void FeedbackSound(int rank)
    {
        if (playerRank != rank)
        {
            //    Debug.Log("I should be rank " + rank + " but I am currently rank " + playerRank);
            if (playerRank > rank)
            {
                sound_Up.Play();
                playerRank = rank;
                Debug.Log (playerRank);
                LogScript
                    .WriteNewLogEntry("Points", "Rank up", rank.ToString());
            }
            else
            {
                sound_Down.Play();
                playerRank = rank;

                Debug.Log (playerRank);
                LogScript
                    .WriteNewLogEntry("Points", "Rank down", rank.ToString());
            }
        }
        //        Debug.Log("I am rank " + rank + " and have updated to " + playerRank);
    }

    public void AIbot()
    {
        //      if (running == true)
        //      {
        Debug.Log("AI Bot Started");
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
        sortingArray =
            pointsArray
                .OrderBy(entry => -entry.GetComponent<LeaderboardEntry>().score)
                .ToArray();
        index = 0;
        foreach (GameObject obj in sortingArray)
        {
            //                if (obj.GetComponent<LeaderboardEntry>().player == true)
            //              {
            //                FeedbackSound (index);
            //          }
            var rt = obj.GetComponent<RectTransform>();
            obj.GetComponent<LeaderboardEntry>().ranking = index;
            var pos = rt.position;
            pos.y = 915 + (-105 * index);
            pos.z = 1;

            Debug
                .Log("Index is at " +
                index +
                ", X: " +
                pos.x +
                ", Y: " +
                pos.y);
            rt.position = pos;

            index++;
        }

        //            Invoke("AIbot", Random.Range(3.0f, 5.0f));
    }
    //  }
}
/*
*/
