using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InterfaceController : MonoBehaviour {

    public enum MENUTYPE
    {
        MAINMENU,
        GALERYMENU,
        TEAMMENU,
        SETTINGSMENU,
        GAMEMENU,
        RESTARTMENU,
        PAUSEMENU
    }

    public GameObject livePrefab;
    public GameObject livePanelRoot;
    public GameObject liveInitPos;
    public float distanceBTWLives;
    List<GameObject> livesInGame;

    public List<GameObject> menus;
    bool openingGame = false;
    
	// Use this for initialization
	void Start () {
        livesInGame = new List<GameObject>();
        SetMainMenu();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetMainMenu()
    {
        SetMenu(MENUTYPE.MAINMENU);
        GetComponent<GameController>().ResetGame();
        GetComponent<ScoreManager>().ResetCurrentScore();
        //Debug.Log("current menu type is " + MENUTYPE.MAINMENU);
        //this.GetComponent<GaleryController>().ActivateCardsInGalery(false);
        if (openingGame) menus[(int)MENUTYPE.MAINMENU].GetComponent<MainMenuController>().SetMaxScoreTexts();
        else openingGame = true;
    }

    public void SetGalleryMenu()
    {
        SetMenu(MENUTYPE.GALERYMENU);
        //this.GetComponent<GaleryController>().ActivateCardsInGalery(true);
    }

    public void SetTeamMenu()
    {
        SetMenu(MENUTYPE.TEAMMENU);
    }

    public void SetSettingsMenu()
    {
        SetMenu(MENUTYPE.SETTINGSMENU);
    }

    public void SetGameMenu()
    {
        SetMenu(MENUTYPE.GAMEMENU);
        GetComponent<ScoreManager>().ResetCurrentScore();
        GetComponent<ScoreManager>().ResetGameScoreText();
        //Debug.Log("current dificult is" + GetComponent<ScoreManager>().GetCurrentDifficult());
        GetComponent<GameController>().StartGame(GetComponent<ScoreManager>().GetCurrentDifficult());
    }

    public void SetRestartMenu()
    {
        Debug.Log("setting restart menu");
        SetMenu(MENUTYPE.RESTARTMENU);
        menus[(int)MENUTYPE.RESTARTMENU].GetComponent<MenuController>().SetCurrentScoreTexts(
                                            GetComponent<ScoreManager>().GetCurrentDifficult());
        GetComponent<GameController>().ResetGame();
    }

    void SetMenu(MENUTYPE type)
    {
        for(int i=0;i < menus.Count; i++)
        {
            if(i == (int)type) menus[i].SetActive(true);
            else menus[i].SetActive(false);
        }
    }

    public void InitLives(int lives)
    {
        for (int i = 0; i < lives; i++) livesInGame.Add(InicializeLiveHUD(distanceBTWLives * i));
    }

    GameObject InicializeLiveHUD(float pos)
    {
        GameObject liveHUD;
        liveHUD = (GameObject)Instantiate(livePrefab, Vector3.zero, transform.rotation);
        liveHUD.transform.SetParent(livePanelRoot.transform, false);
        liveHUD.transform.position = new Vector3(liveInitPos.transform.position.x, liveInitPos.transform.position.y - pos, 0);
        liveHUD.transform.localScale *= 3;
        return liveHUD;
    }

    public void RemoveLife()
    {
        if (livesInGame.Count != 0)
        {
            Destroy(livesInGame[livesInGame.Count - 1]);
            livesInGame.RemoveAt(livesInGame.Count - 1);
        }

        else Debug.Log("the player doesnt have more lives");
    }

    public void ResetLives()
    {
        for (int i = 0; i < livesInGame.Count; i++) Destroy(livesInGame[i]);
        livesInGame.Clear();
    }
}
