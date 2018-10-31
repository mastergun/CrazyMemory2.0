using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour {
    public enum GridState
    {
        DISABLED,
        GENERATINGGRID,
        ROTATINGALLCARDS,
        CLEANED,
        ALLCARDSSTOPED
    }

    public GameObject cardPrefab;
    public List<Sprite> textures;
    public Vector2 gridSize;
    public CardData cardsData;

    List<GameObject> cardsInGame;
    public bool isInfinite = false;
    //bool generatingGrid = false;
    public GridState gridState = GridState.DISABLED;
    int cardsInMovement = 0;

    public int LastIdMonsterUncovered = 0;
	// Use this for initialization
	void Start () {
        cardsInGame = new List<GameObject>();
        //GenerateGrid();
    }
	
	// Update is called once per frame
	void FixedUpdate() {
        switch (gridState)
        {
            case GridState.DISABLED:
                break;

            case GridState.GENERATINGGRID:
                if (cardsInMovement == 0) gridState = GridState.ROTATINGALLCARDS;
                break;

            case GridState.ROTATINGALLCARDS:
                RotateAllCards();
                gridState = GridState.DISABLED;
                GetComponent<GameController>().gs = GameController.GameState.INITGAME;
                break;

            case GridState.CLEANED:
                GetComponent<GameController>().gs = GameController.GameState.CHANGEMENU;
                cardsInMovement = 0;
                gridState = GridState.DISABLED;
                break;

            case GridState.ALLCARDSSTOPED:
                
                if (cardsInMovement == 0)
                {
                    //Debug.Log("all cards still stoped");
                    gridState = GridState.DISABLED;
                    GetComponent<InputController>().DeactivateInput(true);
                    GetComponent<GameController>().gs = GameController.GameState.GAMELOOP;
                }
                break;
        }
    }

    public void GenerateGrid()
    {
        int idCounter = 0;

        //create a list of cards
        List<int> cardIdsInGame = cardsData.SetGridCards((((int)gridSize.x * (int)gridSize.y) / 2), textures);
        Debug.Log("cards loaded "+ cardIdsInGame.Count);
        //generate the cards, set the id and the textures
        for (int k = 1; k <= (gridSize.x * gridSize.y); k++)
        {
            idCounter += k%2;
            //Debug.Log("idCounter is : " + idCounter);
            cardsInGame.Add(InicializeCard(idCounter - 1,cardIdsInGame[idCounter -1], textures[(idCounter - 1)]));
            //cardsInGame.Add(InicializeCard(idCounter - 1,cardIds[idCounter-1], textures[idCounter - 1]));
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
        gridState = GridState.GENERATINGGRID;
        
    }

    public GameObject InicializeCard(int id,int spriteId, Sprite texture)
    {
        GameObject card;
        Vector3 position = Vector3.zero;
        card = (GameObject)Instantiate(cardPrefab, position, transform.rotation);
        //card.GetComponentInParent<Transform>().localScale = new Vector2(Screen.width /(512f), Screen.width / (512f));
        card.GetComponentInChildren<CardScript>().gridRef = this;
        card.GetComponentInChildren<CardScript>().id = id;
        //Debug.Log("grid id: "+id);
        //Debug.Log("sprite id: "+ spriteId);
        card.GetComponentInChildren<CardScript>().spriteId = spriteId;
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


    public void RotateRandomCard()
    {
        int x = (int)Random.Range(0, cardsInGame.Count - 1);
        cardsInGame[x].GetComponentInChildren<CardScript>().RotateCard();
    }

    public void RotateAllCards()
    {
        if(cardsInGame.Count != 0 && cardsInMovement ==0)
        {
            for (int i = 0; i < cardsInGame.Count; i++)
            {
                cardsInGame[i].GetComponentInChildren<CardScript>().RotateCard();
            }
        }
        else
        {
            Debug.Log("marti ets un gañan");
        }
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

    public void CleanGrid(bool inGame)
    {
        if(cardsInGame.Count != 0)
        {
            for (int i = 0; i < cardsInGame.Count; i++)
            {
                Destroy(cardsInGame[i]);
            }
            cardsInGame.Clear();
        }
        if(inGame)gridState = GridState.CLEANED;
        else gridState = GridState.DISABLED;
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

    public bool AllCardsUncoveredCorrectly()
    {
        for (int i = 0;i < cardsInGame.Count ;i++)
        {
            if (!cardsInGame[i].GetComponentInChildren<CardScript>().bloqued)
            {
                return false;
            }
        }
        return true;
    }
}
