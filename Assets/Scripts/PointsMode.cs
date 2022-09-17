using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PointsMode : MonoBehaviour
{
    public GameObject[] pointsArray;
    public GameObject[] sortedArray;
    public AudioSource Sound_Up;
    public AudioSource Sound_Down;

    void Start()
    {
        sortedArray = pointsArray;
    }

    // Update is called once per frame
    void Update()
    {
        //        if Comparison
    }


    public void Comparison()
    {
        for (var i = 0; i < pointsArray.Length; i++)
        {
            if (pointsArray[i].GetComponent<LeaderboardEntry>().score != sortedArray[i].GetComponent<LeaderboardEntry>().score) { SortListing(); }
        }
    }

    public void SortListing()
    {
        sortedArray = pointsArray.OrderBy(entry => -entry.GetComponent<LeaderboardEntry>().score).ToArray();
        int i = 0;
        foreach (GameObject obj in sortedArray)
        {
            var rt = obj.GetComponent<RectTransform>();
            var pos = rt.position;
            if (sortedArray[i].GetComponent<LeaderboardEntry>().player == true)
            {
                Debug.Log("I am panel " + i);
                if (sortedArray[i].GetComponent<LeaderboardEntry>().ranking > i)
                {
                    Sound_Up.Play();
                    LogScript.WriteNewLogEntry("Points", "Rank up", i.ToString());
                }
                if (sortedArray[i].GetComponent<LeaderboardEntry>().ranking < i)
                {
                    Sound_Down.Play();
                    LogScript.WriteNewLogEntry("Points", "Rank down", i.ToString());
                }
            }
            sortedArray[i].GetComponent<LeaderboardEntry>().ranking = i;
            pos.y = 865 - (105 * i);
            pos.z = 1;
            rt.position = pos;
            i++;
        }
        pointsArray = sortedArray;
    }
}