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
        PAUSEMENU
    }

    //[System.Serializable]
    //public struct Menu{
    //    public GameObject menu;
    //    public Image Background;
    //    public MENUTYPE type;
    //    public List<Button> buttons;
    //}

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
        menus[(int)MENUTYPE.MAINMENU].SetActive(true);
        menus[(int)MENUTYPE.GALERYMENU].SetActive(false);
        menus[(int)MENUTYPE.TEAMMENU].SetActive(false);
        menus[(int)MENUTYPE.SETTINGSMENU].SetActive(false);
        GetComponent<GameController>().ResetGame();
    }

    public void SetGalleryMenu()
    {
        menus[(int)MENUTYPE.MAINMENU].SetActive(false);
        menus[(int)MENUTYPE.GALERYMENU].SetActive(true);
        menus[(int)MENUTYPE.TEAMMENU].SetActive(false);
        menus[(int)MENUTYPE.SETTINGSMENU].SetActive(false);
        //GetComponent<GameController>().ResetGame();
    }

    public void SetTeamMenu()
    {
        menus[(int)MENUTYPE.MAINMENU].SetActive(false);
        menus[(int)MENUTYPE.GALERYMENU].SetActive(false);
        menus[(int)MENUTYPE.TEAMMENU].SetActive(true);
        menus[(int)MENUTYPE.SETTINGSMENU].SetActive(false);
        //GetComponent<GameController>().ResetGame();
    }

    public void SetSettingsMenu()
    {
        menus[(int)MENUTYPE.MAINMENU].SetActive(false);
        menus[(int)MENUTYPE.GALERYMENU].SetActive(false);
        menus[(int)MENUTYPE.TEAMMENU].SetActive(false);
        menus[(int)MENUTYPE.SETTINGSMENU].SetActive(true);
        //GetComponent<GameController>().ResetGame();
    }
}
