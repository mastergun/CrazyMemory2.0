using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {

    public enum TEXTS
    {
        SCOREPOINTS,
        SCORETIME,
        SCOREERRORS,
        DIFFICULT
    }

    public ScoreManager smRef;
    public Image bgRef;
    public GameObject HighScore;

    public List<Text> textsInScreen;
    public List<Color> bgDifColors;
    public List<string> difTittles;

    public GaleryCardScript unlockedCardDispalayer;
    public GameObject UCMenu;

    public int DifficultSelected = 1;

    public bool newCardUnlocked;
    public float maxTimeVisible = 5;
    // Use this for initialization
    void Start () {
        //SetCurrentDifficult();
        //UCMenu.SetActive(false);
        //newCardUnlocked = false;
    }
	
	// Update is called once per frame
	void Update () {
        if(Input.GetMouseButtonDown(0) && newCardUnlocked)
        {
            UCMenu.SetActive(false);
            newCardUnlocked = false;
        }
    }

    public void SetCurrentDifficult()
    {
        DifficultSelected = smRef.GetCurrentDifficult();
        GetComponent<GameController>().SetGamePref(DifficultSelected);
        SetCurrentScoreTexts(DifficultSelected);
    }

    public void SetMaxScoreTexts()
    {
        bgRef.color = bgDifColors[DifficultSelected];
        textsInScreen[(int)TEXTS.DIFFICULT].text = difTittles[DifficultSelected];
        smRef.SetScoreScreen(textsInScreen[(int)TEXTS.SCOREPOINTS],
                             textsInScreen[(int)TEXTS.SCORETIME],
                             textsInScreen[(int)TEXTS.SCOREERRORS],
                             DifficultSelected);
    }

    public void SetCurrentScoreTexts(int d)
    {
        DifficultSelected = d;
        bgRef.color = bgDifColors[DifficultSelected];
        textsInScreen[(int)TEXTS.DIFFICULT].text = difTittles[DifficultSelected];
        smRef.SetCurrentScoreScreen(textsInScreen[(int)TEXTS.SCOREPOINTS],
                             textsInScreen[(int)TEXTS.SCORETIME],
                             textsInScreen[(int)TEXTS.SCOREERRORS]);
    }

    public void ActivateHighScoreBG(bool activate)
    {
        HighScore.SetActive(activate);
    }

    public void ActivateNewCardUnlocked()
    {
        newCardUnlocked = true;
        UCMenu.SetActive(true);
        Debug.Log("unlocked menu activated");
    }

    public void SetCardUnlockedInfo(CardData.Data cardInfo, Sprite monsterImage, Color rarity)
    {
        unlockedCardDispalayer.SetCardInfo(cardInfo, monsterImage, rarity);
        Debug.Log("info card seted");
    }
}
