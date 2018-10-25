using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class SaveData {

    public static Save save = new Save();

    public delegate void DelegateAction();
    //public static event DelegateAction OnLoaded;
    //public static event DelegateAction OnBeforeSave;

    public static void Load(string path)
    {
        //save = LoadScores(path);
    }

    //public static void AddScoreData(ScoreData data)
    //{
    //    save.scores.Add(data);
    //}
    //public static void ClearScoreList()
    //{
    //    save.scores.Clear();
    //}

    public static Save LoadScores(string path)
    {
        string json = File.ReadAllText(path);
        return JsonUtility.FromJson<Save>(json);
    }

    public static void SaveScores(string path, Save scores)
    {
        string json = JsonUtility.ToJson(scores);
        StreamWriter sw = File.CreateText(path);
        sw.Close();

        File.WriteAllText(path, json);
    }




}
