using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Save
{

    public struct scoreData
    {
        public int dif;
        public float maxScorePoints;
        public float bestTime;
        public int errors;
    }
    // Use this for initialization

    //public List<scoreData> sd = new scoreData[4];
    public bool firstTimeGame = false;
}

