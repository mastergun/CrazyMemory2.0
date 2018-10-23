using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour {

    public Camera cameraRef;
    List<CardScript> cardsTurned;
    int turnedCardsCount = 0;
	void Start () {
        cardsTurned = new List<CardScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && turnedCardsCount < 2)
        {
            RaycastHit hit;
            Ray ray = cameraRef.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit) && hit.transform.tag == "Card"){
                if (hit.transform.GetComponent<CardScript>().CanMove() && 
                    !AllreadyCliked(hit.transform.GetComponent<CardScript>()) &&
                    !hit.transform.GetComponent<CardScript>().reversed)
                {
                    //Debug.Log("adding card, count:" + turnedCardsCount);
                    //Debug.Log("id : " + hit.transform.GetComponent<CardScript>().id);
                    hit.transform.GetComponent<CardScript>().RotateCard();
                    cardsTurned.Add(hit.transform.GetComponent<CardScript>());
                    turnedCardsCount++;
                }
                else
                {
                    //Debug.Log("this card is already in execution");
                }
            }
        }else if(turnedCardsCount == 2)
        {
            if(cardsTurned[0].id == cardsTurned[1].id)
            {
                //add score
                //remove cards from game
                GetComponent<GridGenerator>().RemoveTwoCards(
                                                cardsTurned[0],
                                                cardsTurned[1]);
                turnedCardsCount = 0;
                cardsTurned.Clear();
            }
            else
            {
                if (cardsTurned[0].CanMove() && cardsTurned[1].CanMove())
                {
                    cardsTurned[0].RotateCard();
                    cardsTurned[1].RotateCard();
                    turnedCardsCount = 0;
                    cardsTurned.Clear();
                }       
            }
        }
    }

    bool AllreadyCliked(CardScript card)
    {
        for(int i= 0;i < cardsTurned.Count; i++)
        {
            if (cardsTurned[i] == card) return true;
        }
        return false;
    }
}
