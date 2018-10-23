using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour {

    public GameObject cardPrefab;
    public List<Sprite> textures;
    public Vector2 gridSize;

    List<GameObject> cardsInGame;
     
	// Use this for initialization
	void Start () {
        cardsInGame = new List<GameObject>();
        GenerateGrid();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void GenerateGrid()
    {
        int idCounter = 0;
        //generate the cards, set the id and the textures
        for (int k = 1; k <= (gridSize.x * gridSize.y); k++)
        {
            idCounter += k%2;
            cardsInGame.Add(InicializeCard(idCounter - 1, textures[0]));
        }

        //shuffle the list
        Shuffle(cardsInGame);

        //give a grid pos to all cards
        Vector2 gp = Vector2.zero;
        for(int i= 0; i< gridSize.x; i++)
        {
            for (int j = 0; j < gridSize.y; j++)
            {
                gp.x = i;
                gp.y = j;
                cardsInGame[(int)gridSize.x * i + j].GetComponentInChildren<CardScript>().SetGridPos(gp);
            }
        }
    }

    public GameObject InicializeCard(int id, Sprite texture)
    {
        GameObject card;
        Vector3 position = Vector3.zero;
        card = (GameObject)Instantiate(cardPrefab, position, transform.rotation);

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
                int x = (int)Random.Range(0, cardsInGame.Count - 1);
                if (!cardsInGame[x].GetComponentInChildren<CardScript>().IsMoving())
                {
                    indexs.Add(x);
                    shuffleing = false;
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
}
