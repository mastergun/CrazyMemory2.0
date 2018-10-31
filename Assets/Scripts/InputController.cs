using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour {

    public Camera cameraRef;
    List<CardScript> cardsTurned;
    int turnedCardsCount = 0;
    bool activatedInput = false;
	void Start () {
        cardsTurned = new List<CardScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (activatedInput)
        {
            if (Input.GetMouseButtonDown(0) && turnedCardsCount < 2)
            {
                RaycastHit hit;
                Ray ray = cameraRef.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit) && hit.transform.tag == "Card")
                {
                    if (hit.transform.GetComponent<CardScript>().CanMove() &&
                        !AllreadyCliked(hit.transform.GetComponent<CardScript>()) &&
                        !hit.transform.GetComponent<CardScript>().reversed)
                    {
                        hit.transform.GetComponent<CardScript>().RotateCard();
                        cardsTurned.Add(hit.transform.GetComponent<CardScript>());
                        turnedCardsCount++;
                    }
                    else
                    {
                        //Debug.Log("this card is already in execution");
                    }
                }
            }
            else if (turnedCardsCount == 2)
            {
                if (cardsTurned[0].id == cardsTurned[1].id)
                {
                    //add score
                    //remove cards from game
                    this.GetComponent<GridGenerator>().LastIdMonsterUncovered = cardsTurned[0].spriteId;
                    cardsTurned[0].bloqued = true;
                    cardsTurned[1].bloqued = true;
                    turnedCardsCount = 0;
                    cardsTurned.Clear();
                    GetComponent<ScoreManager>().AddScore(true);
                }
                else
                {
                    if (cardsTurned[0].CanMove() && cardsTurned[1].CanMove())
                    {
                        cardsTurned[0].RotateCard();
                        cardsTurned[1].RotateCard();
                        turnedCardsCount = 0;
                        cardsTurned.Clear();
                        GetComponent<ScoreManager>().AddScore(false);
                        if (GetComponent<GridGenerator>().isInfinite)
                        {
                            GetComponent<GameController>().playerLives--;
                            GetComponent<InterfaceController>().RemoveLife();
                        }
                    }
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

    public void DeactivateInput(bool activate)
    {
        activatedInput = activate;
    }

    public void ResetInputController()
    {
        turnedCardsCount = 0;
        cardsTurned.Clear();
    }
}
