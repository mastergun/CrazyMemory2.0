﻿using System.Collections;
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

    public MenuController restartMenu;
    public CardData cardsInfo;
    public Text inGameDificult;
    public Text inGameTimeScore;
    List<Score> maxScoreByDif;
    public List<string> dificultNames;

    int currentDificult;
    Score currentScore;

    bool firstTimeGame = true;
    public bool parseScore = false;
    public float pointsPerCard = 50;
    public float maxTimeToComplete = 0;
    float currentMaxTime = 0;
    float deltaTimeScore = 0;

    // Use this for initialization

    void Start()
    {
        maxScoreByDif = new List<Score>();
        LoadGame();
        ResetCurrentScore();
        //source = GetComponent<AudioSource>();
        if (firstTimeGame)
        {
            maxScoreByDif.Add(SetScoreInitScore());
            maxScoreByDif.Add(SetScoreInitScore());
            maxScoreByDif.Add(SetScoreInitScore());
            maxScoreByDif.Add(SetScoreInitScore());
            cardsInfo.LoadData();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (parseScore)
        {
            currentScore.time += Time.deltaTime;
            deltaTimeScore += Time.deltaTime;
            inGameTimeScore.text = currentScore.time.ToString("F1") + "s/ " + currentMaxTime + "s";
            if(deltaTimeScore > 5)
            {
                currentScore.points -= 10;
                if (currentScore.points < 0) currentScore.points = 0;
                deltaTimeScore = 0;
                //Debug.Log("losedPoints per time");
            }

            if(currentMaxTime > 0)
            {
                if (currentScore.time >= currentMaxTime)
                {
                    //Debug.Log("player lose the game");
                    SetLoseScore();
                    GetComponent<GameController>().loseGame = true;
                    GetComponent<GameController>().gs = GameController.GameState.ENDGAME;
                }
            }
        }
    }

    public void SetScoreValues(float maxTime, int dif)
    {
        maxTimeToComplete = maxTime;
        currentMaxTime = maxTimeToComplete;
        ResetGameScoreText();
        currentDificult = dif;
    }

    void SetMaxScore(float score, float time, int errors, int dificult)
    {
        Score s;
        s.points = score;
        s.time = time;
        s.errors = errors;
        maxScoreByDif[dificult] = s;
        firstTimeGame = false;
        //SaveGame();
    }

    public bool CompareScore()
    {
        if(currentScore.points > maxScoreByDif[currentDificult].points)
        {
            //Debug.Log("max score raised!! time : " + currentScore.time + " points : " + currentScore.points);
             
            SetMaxScore(currentScore.points, currentScore.time, currentScore.errors, currentDificult);
            restartMenu.ActivateHighScoreBG(true);
            return true;
        }
        if(currentScore.points >= maxScoreByDif[currentDificult].points &&currentScore.time < maxScoreByDif[currentDificult].time)
        {
            SetMaxScore(currentScore.points, currentScore.time, currentScore.errors, currentDificult);
            restartMenu.ActivateHighScoreBG(true);
            return true;
        }
        return false;
    }

    public void ResetCurrentScore()
    {
        currentScore.points = 0;
        currentScore.time = 0;
        currentScore.errors = 0;
        inGameTimeScore.text = 0.ToString() + "s";
        currentMaxTime = maxTimeToComplete;
    }

    public void AddScore(bool add)
    {
        if (add)
        {
            currentScore.points += pointsPerCard;
            if(currentMaxTime > 0) currentMaxTime += 5;
        }
        else
        {
            currentScore.errors++;
            currentScore.points -= 20;
            if (currentScore.points < 0) currentScore.points = 0;
        } 
    }

    public void SetLoseScore()
    {
        parseScore = false;
        currentScore.points = 0;
        currentMaxTime = maxTimeToComplete;
    }

    public void ResetGameScoreText()
    {
        inGameTimeScore.text = currentScore.time.ToString("F1") + "s/ " + currentMaxTime + "s";
    }

    public void SetScoreScreen(Text points, Text time, Text errors, int dif)
    {
        if(points != null && maxScoreByDif !=null) points.text = maxScoreByDif[dif].points.ToString();
        if (time != null && maxScoreByDif != null) time.text = maxScoreByDif[dif].time.ToString("F1") + " s";
        if (errors != null && maxScoreByDif != null) errors.text = maxScoreByDif[dif].errors.ToString();
        currentDificult = dif;
        //Debug.Log("max score texts setted");
    }

    public void SetCurrentScoreScreen(Text points, Text time, Text errors)
    {
        points.text = currentScore.points.ToString();
        time.text = currentScore.time.ToString("F1") + " s";
        errors.text = currentScore.errors.ToString();
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

    //bug to save
    private Save CreateSaveGameObject()
    {
        Save save = new Save();
        for (int i = 0; i < maxScoreByDif.Count; i++)
        {
            ScoreData score = new ScoreData();
            score.dif = i;
            score.maxScorePoints = maxScoreByDif[i].points;
            score.bestTime = maxScoreByDif[i].time;
            score.errors = maxScoreByDif[i].errors;
            save.scores.Add(score);
        }
        save.cards = cardsInfo.cards;
        save.unsortedIds = cardsInfo.unSortedCardIds;
        save.firstTimeGame = firstTimeGame;
        return save;
    }

    //bug to load
    private void LoadScoreFromSaveObject(Save save)
    {
        for (int i = 0; i < save.scores.Count; i++)
        {
            Score s;
            s.points = save.scores[i].maxScorePoints;
            s.time = save.scores[i].bestTime;
            s.errors = save.scores[i].errors;
            maxScoreByDif.Add(s);
        }
        cardsInfo.cards = save.cards;
        cardsInfo.unSortedCardIds = save.unsortedIds;
        firstTimeGame = save.firstTimeGame;
        cardsInfo.SortCardSprites();
        GetComponent<AudioManager>().SortMonsterAudios(cardsInfo.unSortedCardIds);
    }

    //bug to create save
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
            //Debug.Log("No game saved!");
        }
    }

    public void SetDificultText()
    {
        inGameDificult.text = dificultNames[currentDificult];
    }

}

[System.Serializable]
public class ScoreData
{
    public int dif;
    public float maxScorePoints;
    public float bestTime;
    public int errors;
}