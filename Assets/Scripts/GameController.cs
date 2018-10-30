using System.Collections;
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
        CHANGEMENU
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

    //game variables
    float deltatime;
    float maxInVisT;
    
    float tbs;
    int pl = 0;
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
                break;

            case GameState.ENDGAME:
                GetComponent<InputController>().DeactivateInput(false);
                GetComponent<ScoreManager>().CompareScore();
                GetComponent<GridGenerator>().CleanGrid(true);
                gs = GameState.DISABLED;
                break;

            case GameState.CHANGEMENU:
                GetComponent<InterfaceController>().SetRestartMenu();
                gs = GameState.DISABLED;

                break;
        }
    }

    public void StartGame(int d)
    {
        SetGamePref(d);
        deltatime = 0.0f;
        gs = GameState.DISABLED;
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
        Debug.Log("setting game preferences");
        //set lifes
        pl = PlayerSettingsByDificult[d].playerLifes;
        //set if is infinite game and grid size
        GetComponent<GridGenerator>().SetGridPreferences(
            PlayerSettingsByDificult[d].isInfinite,
            PlayerSettingsByDificult[d].gridSize);
        //set max init visible time cards
        maxInVisT = PlayerSettingsByDificult[d].maxInitVisibleTime;
        //set time btw shuffle
        tbs = PlayerSettingsByDificult[d].timeBtwShuffle;
        GetComponent<ScoreManager>().SetScoreValues(PlayerSettingsByDificult[d].maxGameTime, d);
    }

    bool CheckEndCondition()
    {
        //if (GetComponent<GridGenerator>().GetCardsInGame() <= 0) return true;
        if(GetComponent<GridGenerator>().AllCardsUncoveredCorrectly()) return true;
        else return false;
    }
}
