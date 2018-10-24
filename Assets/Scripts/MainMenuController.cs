using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour {

    enum BUTTONTYPE
    {
        PLAYBUTTON,
        LOWDIFICULTBUTTON,
        UPDIFICULTBUTTON,
        EXITBUTTON,
        GALERYBUTTON,
        TEAMBUTTON,
        SETTINGSBUTTON
    }
    public Image bgRef;
    public Image difImRef;
    public Text dificultTextRef;
    public Text maxScoreTextRef;
    public List<Color> bgDifColors;
    public List<string> difTittles;
    public List<Button> buttons;

    List<float> maxScores; 
    int DifficultSelected = 1;
    // Use this for initialization
    void Start () {
        maxScores = new List<float>();
        if(maxScores.Count == 0)
        {
            for(int i=0;i< 4; i++)
            {
                maxScores.Add(Random.Range(0,200.0f));
            }
        }
        SetDifficult(-1);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetDifficult(int dificult)
    {
        if (dificult < 0 && DifficultSelected > 0) DifficultSelected--;
        else if (dificult > 0 && DifficultSelected < 3) DifficultSelected++;
        else
        {
            Debug.Log("impossible to change dificult");
            return;
        }
        bgRef.color = bgDifColors[DifficultSelected];
        dificultTextRef.text = difTittles[DifficultSelected];
        maxScoreTextRef.text = maxScores[DifficultSelected].ToString();
    }

    public void SetMaxScoreTexts(int dif, float maxScore)
    {
        maxScores[dif] = maxScore;
    }
}
