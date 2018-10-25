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
    public bool inGame = false;
    bool initGame = false;

    // Use this for initialization
    void Start () {
        //PlayerSettingsByDificult = new List<GameSettings>();
        deltatime = 0;
    }
	
	// Update is called once per frame
	void Update () {
        if (inGame)
        {
            //Debug.Log(deltatime);
            deltatime += 0.01f;
            if (initGame)
            {
                //Debug.Log("initGame");
                if (deltatime > maxInVisT)
                {
                    GetComponent<GridGenerator>().RotateAllCards();
                    deltatime = 0;
                    initGame = false;
                    GetComponent<ScoreManager>().parseScore = true;
                }
            }
            else
            {
                if (deltatime > tbs && tbs != 1 && 
                    GetComponent<GridGenerator>().GetCardsInMovement()< (GetComponent<GridGenerator>().GetCardsInGame()-2))
                {
                    GetComponent<GridGenerator>().ShuffleTwoCards();
                    deltatime = 0;
                }
            }
            if (CheckEndCondition())
            {
                GetComponent<ScoreManager>().CompareScore();
                GetComponent<InterfaceController>().SetRestartMenu();
            }
        }
    }

    public void StartGame(int d)
    {
        SetGamePref(d);
        initGame = true;
        deltatime = 0.0f;
       
        GetComponent<GridGenerator>().GenerateGrid();
    }
    public void ResetGame()
    {
        inGame = false;
        initGame = false;
        GetComponent<ScoreManager>().parseScore = false;
        deltatime = 0;
        GetComponent<GridGenerator>().CleanGrid();
    }

    public void SetGamePref(int d)
    {
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
        GetComponent<ScoreManager>().maxTimeToComplete = PlayerSettingsByDificult[d].maxGameTime;
    }

    bool CheckEndCondition()
    {
        if (GetComponent<GridGenerator>().GetCardsInGame() <= 0) return true;
        else return false;
    }
}
