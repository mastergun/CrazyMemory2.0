using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour {

    public GameObject cardPrefab;
    public List<Sprite> textures;
    public Vector2 gridSize;

    List<GameObject> cardsInGame;
    int idCounter = 0;
	// Use this for initialization
	void Start () {
        GenerateGrid();
        cardsInGame = new List<GameObject>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void GenerateGrid()
    {
        for(int k = 1; k <= (gridSize.x * gridSize.y); k++)
        {
            idCounter += k%2;
            Debug.Log("the id is : " + (idCounter -1));
            //cardsInGame.Add(InicializeCard(idCounter -1, textures[0]));
        }

        for(int i= 0; i< gridSize.x; i++)
        {
            for (int j = 0; j < gridSize.y; j++)
            {

            }
        }
    }

    GameObject InicializeCard(int id, Sprite texture)
    {
        GameObject card;
        Vector3 position = Vector3.zero;
 
        card = (GameObject)Instantiate(cardPrefab, position, transform.rotation);

        card.GetComponentInChildren<CardScript>().id = id;
        card.GetComponentInChildren<CardScript>().SetCardTexture(texture);
        return card;
    }
}
