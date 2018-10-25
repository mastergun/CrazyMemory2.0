using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Save
{
    public List<ScoreData> scores = new List<ScoreData>();
    // Use this for initialization

    //public List<scoreData> sd = new scoreData[4];
    public bool firstTimeGame = false;
}

