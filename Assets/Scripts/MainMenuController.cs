using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MenuController {

    // Use this for initialization
    void Start () {
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

        // set background color depending wich dificult had been selected
        
        // set dificult button text depending wich dificult had been selected
        

        //set max score texts
        Debug.Log("dificult selected is "+ difTittles[DifficultSelected]);
        SetMaxScoreTexts();
    }
}
