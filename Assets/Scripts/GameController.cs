using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public List<CardScript> listOfCards;
    float deltatime;
	// Use this for initialization
	void Start () {
        deltatime = 0;
        //listOfCards[0].RotateCard();
    }
	
	// Update is called once per frame
	void Update () {
        deltatime += 0.01f;
        if (deltatime > 10)
        {

            deltatime = 0;
            foreach (CardScript card in listOfCards)
            {
                card.RotateCard();
            }
        }
    }
}
