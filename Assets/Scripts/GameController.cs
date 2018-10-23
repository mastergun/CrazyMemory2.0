using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
    float deltatime;
	// Use this for initialization
	void Start () {
        deltatime = 0;
    }
	
	// Update is called once per frame
	void Update () {
        deltatime += 0.01f;
        if (deltatime > 0.5)
        {
            GetComponent<GridGenerator>().ShuffleTwoCards();
            deltatime = 0;
        }
    }
}
