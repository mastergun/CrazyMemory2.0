using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public struct Score
    {
        public float points;
        public float time;
        public int errors;
    }

    public Text inGameTimeScore;
    List<Score> maxScoreByDif;

    int currentDificult;
    Score currentScore;

    bool firstTimeGame = true;
    public bool parseScore = false;
    // Use this for initialization

    void Start()
    {
        maxScoreByDif = new List<Score>();
        LoadGame();
        ResetCurrentScore();
        //source = GetComponent<AudioSource>();
        if (firstTimeGame)
        {
            Debug.Log("setting first time scores");
            maxScoreByDif.Add(SetScoreInitScore());
            maxScoreByDif.Add(SetScoreInitScore());
            maxScoreByDif.Add(SetScoreInitScore());
            maxScoreByDif.Add(SetScoreInitScore());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (parseScore)
        {
            currentScore.time += Time.deltaTime;
            inGameTimeScore.text = currentScore.time.ToString("F1") + "s";
        }
    }

    void SetMaxScore(float score, float time, int errors, int dificult)
    {
        Score s;
        s.points = score;
        s.time = time;
        s.errors = errors;
        maxScoreByDif[dificult] = s;
        firstTimeGame = false;
        SaveGame();
    }

    public void CompareScore()
    {
        if(currentScore.points > maxScoreByDif[currentDificult].points ||
            currentScore.time < maxScoreByDif[currentDificult].time||
            currentScore.points < maxScoreByDif[currentDificult].errors)
        {
            Debug.Log("max score raised!!");
            SetMaxScore(currentScore.points, currentScore.time, currentScore.errors, currentDificult);
        }
    }

    public void ResetCurrentScore()
    {
        currentScore.points = 0;
        currentScore.time = 0;
        currentScore.errors = 0;
        inGameTimeScore.text = 0.ToString() + "s";
    }

    public void SetScoreScreen(Text points, Text time, Text errors, int dif)
    {
        points.text = maxScoreByDif[dif].points.ToString();
        time.text = maxScoreByDif[dif].time.ToString() + " s";
        errors.text = maxScoreByDif[dif].errors.ToString();
        currentDificult = dif;
        Debug.Log("max score texts setted");
    }

    public void SetCurrentScoreScreen(Text points, Text time, Text errors)
    {
        points.text = currentScore.points.ToString();
        time.text = currentScore.time.ToString() + " s";
        errors.text = currentScore.errors.ToString();
        Debug.Log("max score texts setted");
    }

    private Save CreateSaveGameObject()
    {
        Save save = new Save();
        for(int i=0;i < maxScoreByDif.Count; i++){
            save.sd[i].dif = i;
            save.sd[i].maxScorePoints = maxScoreByDif[i].points;
            save.sd[i].bestTime = maxScoreByDif[i].time;
            save.sd[i].errors = maxScoreByDif[i].errors;
        }
        save.firstTimeGame = firstTimeGame;
        return save;
    }

    private void LoadScoreFromSaveObject(Save save)
    {
        for(int i=0;i < save.sd.Length; i++)
        {
            Score s;
            s.points = save.sd[i].maxScorePoints;
            s.time = save.sd[i].bestTime;
            s.errors = save.sd[i].errors;
            maxScoreByDif.Add(s);
        }
        firstTimeGame = save.firstTimeGame;
    }

    public void SaveGame()
    {
        // create a save data
        Save save = CreateSaveGameObject();

        // create save file
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/gamesave.save");
        bf.Serialize(file, save);
        file.Close();

        //reset variable if you need

    }

    public void LoadGame()
    {
        // search if exist a save file
        if (File.Exists(Application.persistentDataPath + "/gamesave.save"))
        {
            // load file data
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/gamesave.save", FileMode.Open);
            Save save = (Save)bf.Deserialize(file);
            file.Close();

            // set variables
            LoadScoreFromSaveObject(save);
        }
        else
        {
            Debug.Log("No game saved!");
        }
    }

    Score SetScoreInitScore()
    {
        Score score;
        score.points = 0;
        score.time = 0;
        score.errors = 0;
        return score;
    }

    public int GetCurrentDifficult()
    {
        return currentDificult;
    }
}