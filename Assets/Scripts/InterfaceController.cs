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

    public List<GameObject> menus;
    
	// Use this for initialization
	void Start () {
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
        //menus[(int)MENUTYPE.MAINMENU].GetComponent<MainMenuController>().SetMaxScoreTexts();
    }

    public void SetGalleryMenu()
    {
        SetMenu(MENUTYPE.GALERYMENU);
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
        GetComponent<ScoreManager>().ResetGameScoreText();
        GetComponent<ScoreManager>().ResetCurrentScore();
        GetComponent<GameController>().StartGame(GetComponent<ScoreManager>().GetCurrentDifficult());
    }

    public void SetRestartMenu()
    {
        SetMenu(MENUTYPE.RESTARTMENU);
        menus[(int)MENUTYPE.RESTARTMENU].GetComponent<MenuController>().SetCurrentScoreTexts();
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
}
