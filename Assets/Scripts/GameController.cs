﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    enum Dificult
    {
        EASY,
        MEDIUM,
        CRAZY,
        INFINITE
    }

    public enum GameState
    {
        DISABLED,
        INITGAME,
        GAMELOOP,
        ENDGAME,
        CHANGEMENU,
        RESET
    }

    [System.Serializable]
    public struct GameSettings
    {
        public int playerLifes;
        public Vector2 gridSize;
        public bool isInfinite;
        public float maxInitVisibleTime;
        public float timeBtwShuffle;
        public float maxGameTime;
    }

    public List<GameSettings> PlayerSettingsByDificult;
    public MenuController restartMenu;
    //game variables
    public int playerLives;
    float deltatime;
    float maxInVisT;
    
    float tbs;
    int publiCounter = 0;
    public bool loseGame = false;
    public GameState gs = GameState.DISABLED;
    //bool initGame = false;

    // Use this for initialization
    void Start () {
        //PlayerSettingsByDificult = new List<GameSettings>();
        SetGamePref(GetComponent<ScoreManager>().GetCurrentDifficult());
        deltatime = 0;
    }
	
	// Update is called once per frame
	void FixedUpdate()
    {
        switch (gs)
        {
            case GameState.DISABLED:
                break;

            case GameState.INITGAME:
                deltatime += 0.01f;
                if (deltatime > maxInVisT)
                {
                    deltatime = 0;
                    gs = GameState.DISABLED;
                    GetComponent<GridGenerator>().RotateAllCards();
                    GetComponent<GridGenerator>().gridState = GridGenerator.GridState.ALLCARDSSTOPED;
                    GetComponent<ScoreManager>().parseScore = true;
                }
                break;

            case GameState.GAMELOOP:
                deltatime += 0.01f;

                if (deltatime > tbs && tbs != 1 &&
                    GetComponent<GridGenerator>().GetCardsInMovement() < (GetComponent<GridGenerator>().GetCardsInGame() - 2))
                {
                    GetComponent<GridGenerator>().ShuffleTwoCards();
                    deltatime = 0;
                }
                if (CheckEndCondition()) gs = GameState.ENDGAME;
                if (GetComponent<GridGenerator>().AllCardsUncoveredCorrectly() && 
                    GetComponent<GridGenerator>().isInfinite) gs = GameState.RESET;
                break;

            case GameState.ENDGAME:
                if (!this.GetComponent<CardData>().cards[this.GetComponent<GridGenerator>().LastIdMonsterUncovered].unlocked &&
                    !GetComponent<GridGenerator>().isInfinite && GetComponent<GridGenerator>().AllCardsUncoveredCorrectly())
                {
                    int i = this.GetComponent<GridGenerator>().LastIdMonsterUncovered;
                    this.GetComponent<CardData>().SetCardInfo(i, true);
                    this.GetComponent<GaleryController>().cardsInGalery[i].GetComponent<GaleryCardScript>().UnlockCard();
                    restartMenu.SetCardUnlockedInfo(GetComponent<CardData>().cards[i],
                                                    GetComponent<CardData>().cardSprites[i],
                                                    GetComponent<GaleryController>().rarityColor[GetComponent<CardData>().cards[i].rarity]);
                    restartMenu.ActivateNewCardUnlocked();
                }

                GetComponent<InputController>().DeactivateInput(false);
                GetComponent<InputController>().ResetInputController();
                if(!loseGame && !GetComponent<GridGenerator>().isInfinite) GetComponent<ScoreManager>().CompareScore();
                else if(GetComponent<GridGenerator>().isInfinite) GetComponent<ScoreManager>().CompareScore(); 
                GetComponent<ScoreManager>().SaveGame();
                GetComponent<InterfaceController>().ResetLives();
                GetComponent<GridGenerator>().CleanGrid(true);
                gs = GameState.DISABLED;
                break;

            case GameState.CHANGEMENU:
                GetComponent<InterfaceController>().SetRestartMenu();
                if(loseGame) GetComponent<AudioManager>().PlayGameEffect(3);
                else GetComponent<AudioManager>().PlayGameEffect(2);
                if (publiCounter % 3 == 2) GetComponent<InicializerScript>().ShowInterstitial();
                publiCounter++;
                gs = GameState.DISABLED;

                break;

            case GameState.RESET:
                GetComponent<InputController>().DeactivateInput(false);
                GetComponent<InputController>().ResetInputController();
                GetComponent<GridGenerator>().CleanGrid(true);
                gs = GameState.DISABLED;
                GetComponent<GridGenerator>().GenerateGrid();
                break;
        }
    }

    public void StartGame(int d)
    {
        SetGamePref(d);
        loseGame = false;
        GetComponent<InicializerScript>().ShowBanner();
        deltatime = 0.0f;
        if (GetComponent<GridGenerator>().isInfinite)
        {
            GetComponent<InterfaceController>().InitLives(PlayerSettingsByDificult[d].playerLifes);
        }
        gs = GameState.DISABLED;
        if (publiCounter % 3 == 2) GetComponent<InicializerScript>().PrepareInterstitial();
        GetComponent<GridGenerator>().GenerateGrid();
    }

    public void ResetGame()
    {
        gs = GameState.DISABLED;
        GetComponent<ScoreManager>().parseScore = false;
        deltatime = 0;
        GetComponent<GridGenerator>().CleanGrid(false);
    }

    public void SetGamePref(int d)
    {
        //Debug.Log("setting game preferences");
        //set lifes
        playerLives = PlayerSettingsByDificult[d].playerLifes;
        //set if is infinite game and grid size
        GetComponent<GridGenerator>().SetGridPreferences(
            PlayerSettingsByDificult[d].isInfinite,
            PlayerSettingsByDificult[d].gridSize);
        //set max init visible time cards
        maxInVisT = PlayerSettingsByDificult[d].maxInitVisibleTime;
        //set time btw shuffle
        tbs = PlayerSettingsByDificult[d].timeBtwShuffle;
        GetComponent<ScoreManager>().SetScoreValues(PlayerSettingsByDificult[d].maxGameTime, d);
        GetComponent<ScoreManager>().SetDificultText();
    }

    bool CheckEndCondition()
    {
        if (GetComponent<GridGenerator>().AllCardsUncoveredCorrectly() && !GetComponent<GridGenerator>().isInfinite) return true;
        else if (playerLives <= 0 && GetComponent<GridGenerator>().isInfinite) return true;
        else return false;
    }
}
