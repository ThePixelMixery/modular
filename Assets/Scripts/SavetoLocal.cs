using System;
using UnityEngine;

public class SaveToLocal : MonoBehaviour
{
    [Serializable]
    public class Player
    {
        public string Name;

        public int Level;

        public int Experience;

        public float Money;
    }
}
