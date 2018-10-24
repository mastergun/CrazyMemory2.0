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

    public List<Text> textsInScreen;
    public List<Color> bgDifColors;
    public List<string> difTittles;

    public int DifficultSelected = 1;
    // Use this for initialization
    void Start () {
        //SetCurrentDifficult();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetCurrentDifficult()
    {
        DifficultSelected = smRef.GetCurrentDifficult();
        GetComponent<GameController>().SetGamePref(DifficultSelected);
        SetCurrentScoreTexts();
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

    public void SetCurrentScoreTexts()
    {
        bgRef.color = bgDifColors[DifficultSelected];
        textsInScreen[(int)TEXTS.DIFFICULT].text = difTittles[DifficultSelected];
        smRef.SetCurrentScoreScreen(textsInScreen[(int)TEXTS.SCOREPOINTS],
                             textsInScreen[(int)TEXTS.SCORETIME],
                             textsInScreen[(int)TEXTS.SCOREERRORS]);
    }
}
