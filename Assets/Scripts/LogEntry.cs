using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogEntry : MonoBehaviour
{
    public static string gameEvent;

    public static string tag1;

    public static string tag2;

    public LogEntry()
    {
    }

    public static void EntryCode(
        string newGameEvent,
        string newTag1,
        string newTag2
    )
    {
        gameEvent = newGameEvent;
        tag1 = newTag1;
        tag2 = newTag2;
    }
}
