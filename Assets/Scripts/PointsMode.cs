using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PointsMode : MonoBehaviour
{
    public GameObject[] pointsArray;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var sortedArray = pointsArray.OrderBy(element => element.GetComponent<LeaderboardEntry>().score).ToArray();
        int i=0;
        foreach (GameObject obj in sortedArray)
        {
            Debug.Log("I am panel "+i);
            pointsArray[i].GetComponent<LeaderboardEntry>().ranking = i;
            MovePositions(i);
            i++;
        }
    }

    void MovePositions(int rank){
        foreach(GameObject obj in pointsArray){
        var rt = obj.GetComponent<RectTransform>();
        var pos = rt.position;
            switch (rank)
            {
                case(0):
                pos.x=315;
                break;
                case(1):
                pos.x=210;
                break;
                case(2):
                pos.x=105;
                break;
                case(3):
                pos.x=0;
                break;
                case(4):
                pos.x=-105;
                break;
                case(5):
                pos.x=-210;
                break;
                case(6):
                pos.x=-315;
                break;
                default:
                break;
            }
            rt.position = pos;
            Debug.Log("Panel "+rank+ "has moved");
        }
    }
}

