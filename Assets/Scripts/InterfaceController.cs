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
        //Debug.Log("current menu type is " + MENUTYPE.MAINMENU);
        //this.GetComponent<GaleryController>().ActivateCardsInGalery(false);
        //menus[(int)MENUTYPE.MAINMENU].GetComponent<MainMenuController>().SetMaxScoreTexts();
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
        Debug.Log("setting game menu");
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
}
