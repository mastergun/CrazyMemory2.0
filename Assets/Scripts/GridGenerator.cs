using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour {

    public GameObject cardPrefab;
    public List<Sprite> textures;
    public Vector2 gridSize;

    List<GameObject> cardsInGame;
    bool isInfinite = false;
    bool generatingGrid = false;
    int cardsInMovement = 0;
	// Use this for initialization
	void Start () {
        cardsInGame = new List<GameObject>();
        //GenerateGrid();
    }
	
	// Update is called once per frame
	void Update () {
        if (generatingGrid && cardsInMovement == 0)
        {
            //start rotate cards
            for(int i = 0;i < cardsInGame.Count; i++)
            {
                if (!cardsInGame[i].GetComponentInChildren<CardScript>().CanMove())
                {
                    Debug.Log("wait until all cards are seted");
                    break;
                }
                if(i == (cardsInGame.Count - 1))
                {
                    RotateAllCards();
                    generatingGrid = false;
                    
                }
            }
            
        }
    }

    public void GenerateGrid()
    {
        int idCounter = 0;
        //generate the cards, set the id and the textures
        for (int k = 1; k <= (gridSize.x * gridSize.y); k++)
        {
            idCounter += k%2;
            cardsInGame.Add(InicializeCard(idCounter - 1, textures[(idCounter - 1)%4]));
        }
        //shuffle the list
        Shuffle(cardsInGame);

        //give a grid pos to all cards
        Vector2 gp = Vector2.zero;
        for (int i = 0; i < gridSize.x; i++)
        {
            for (int j = 0; j < gridSize.y; j++)
            {
                gp.x = i;
                gp.y = j;
                cardsInGame[(int)gridSize.y * i + j].GetComponentInChildren<CardScript>().SetGridPos(gp);
                cardsInMovement++;
            }
        }
        //make all the cards visible
        generatingGrid = true;
        GetComponent<GameController>().inGame = true;
    }

    public GameObject InicializeCard(int id, Sprite texture)
    {
        GameObject card;
        Vector3 position = Vector3.zero;
        card = (GameObject)Instantiate(cardPrefab, position, transform.rotation);
        card.GetComponentInChildren<CardScript>().gridRef = this;
        card.GetComponentInChildren<CardScript>().id = id;
        card.GetComponentInChildren<CardScript>().SetCardTexture(texture);
        return card;
    }

    public void ShuffleTwoCards()
    {
        int numCardsShuffled = 2;
        List<int> indexs = new List<int>();

        bool shuffleing = false;
        for (int i = 0;i<numCardsShuffled;i++)
        {
            shuffleing = true;
            while (shuffleing)
            {
                if (cardsInGame.Count == 0) break;
                int x = (int)Random.Range(0, cardsInGame.Count - 1);
                if(cardsInGame[x] != null)
                {
                    if (cardsInGame[x].GetComponentInChildren<CardScript>().CanMove() && 
                        !cardsInGame[x].GetComponentInChildren<CardScript>().reversed)
                    {
                        indexs.Add(x);
                        cardsInMovement++;
                        shuffleing = false;
                    }
                }
            }
        }

        Vector2 c = cardsInGame[indexs[0]].GetComponentInChildren<CardScript>().GetGridPos(true);
        cardsInGame[indexs[0]].GetComponentInChildren<CardScript>().SetGridPos(
            cardsInGame[indexs[1]].GetComponentInChildren<CardScript>().GetGridPos(true));
        cardsInGame[indexs[1]].GetComponentInChildren<CardScript>().SetGridPos(c);
    }

    public static void Shuffle(List<GameObject> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = (int)Random.Range(0,n-1);
            GameObject value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    public void RotateCard(GameObject card)
    {
        for (int i = 0;i < cardsInGame.Count ;i++)
        {
            if(cardsInGame[i] == card) cardsInGame[i].GetComponentInChildren<CardScript>().RotateCard();
        }
    }

    public void RotateRandomCard()
    {
        int x = (int)Random.Range(0, cardsInGame.Count - 1);
        cardsInGame[x].GetComponentInChildren<CardScript>().RotateCard();
    }

    public void RotateAllCards()
    {
        for (int i = 0; i < cardsInGame.Count; i++)
        {
            cardsInGame[i].GetComponentInChildren<CardScript>().RotateCard();
        }
        //rotateall = true;
        //cardsCounter = cardsInGame.Count;
    }

    public void SetGridPreferences(bool i, Vector2 gs)
    {
        isInfinite = i;
        gridSize = gs;
    }
    
    public void CardEndMovement()
    {
        cardsInMovement--;
    }

    public void CleanGrid()
    {
        for (int i = 0; i < cardsInGame.Count; i++)
        {
            Destroy(cardsInGame[i]);
        }
        cardsInGame.Clear();
    }

    public void RemoveTwoCards(CardScript card1, CardScript card2)
    {
        card1.AutoDestroy();
        card2.AutoDestroy();
        Debug.Log("cards in game " + cardsInGame.Count);
    }

    public void RemoveCardFromPull(GameObject obj)
    {
        cardsInGame.Remove(obj);
    }

    public int GetCardsInGame()
    {
        return cardsInGame.Count;
    }

    public int GetCardsInMovement()
    {
        return cardsInMovement;
    }
}
