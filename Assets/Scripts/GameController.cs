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
    }

    public List<GameSettings> PlayerLifesByDificult;

    //game variables
    float deltatime;
    float maxInVisT;
    float tbs;
    int pl = 0;
    bool inGame = false;
    bool initGame = false;

    // Use this for initialization
    void Start () {
        deltatime = 0;
    }
	
	// Update is called once per frame
	void Update () {
        if (inGame)
        {
            deltatime += 0.01f;
            if (initGame)
            {
                if (deltatime > maxInVisT)
                {
                    GetComponent<GridGenerator>().RotateAllCards();
                    deltatime = 0;
                    initGame = false;
                }
            }
            else
            {
                if (deltatime > tbs)
                {
                    GetComponent<GridGenerator>().ShuffleTwoCards();
                    deltatime = 0;
                }
            }
        }
    }

    public void StartGame(int d)
    {
        Debug.Log((Dificult)d);
        SetGamePref((Dificult)d);
        inGame = true;
        initGame = true;
        deltatime = 0;
        GetComponent<GridGenerator>().GenerateGrid();
    }
    public void ResetGame()
    {
        inGame = false;
        initGame = false;
        deltatime = 0;
        GetComponent<GridGenerator>().CleanGrid();
    }

    void SetGamePref(Dificult d)
    {
        pl = PlayerLifesByDificult[(int)d].playerLifes;
        GetComponent<GridGenerator>().SetGridPreferences(
            PlayerLifesByDificult[(int)d].isInfinite, 
            PlayerLifesByDificult[(int)d].gridSize);
        maxInVisT = PlayerLifesByDificult[(int)d].maxInitVisibleTime;
        tbs = PlayerLifesByDificult[(int)d].timeBtwShuffle;
    }
}
