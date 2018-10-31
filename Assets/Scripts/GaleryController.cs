using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GaleryController : MonoBehaviour {

    public GameObject galeryCardPrefab;
    public GameObject galeryMenu;

    public List<Sprite> raritySprite;
    public List<Color> rarityColor;

    public List<GameObject> cardsInGalery;

    float distBetweenCards = 0;
    int centredCard = 0;
    public int cardsInMovement;

    bool created = false;
	// Use this for initialization
	void Start () {
        cardsInGalery = new List<GameObject>();
        distBetweenCards = Screen.currentResolution.width / 2.5f;
    }
	
	// Update is called once per frame
	void Update () {
        if (!created)
        {
            if(this.GetComponent<CardData>().cards.Count != 0)
            {
                //Debug.Log("inicializing galery cards");
                InicializeCardsInGalery();
                created = true;
                //unlockAll();
            }
        }
	}

    GameObject InicializeGaleryCard(CardData.Data cardInfo, float pos)
    {
        GameObject card;
        card = (GameObject)Instantiate(galeryCardPrefab, Vector3.zero, transform.rotation);
        card.GetComponent<GaleryCardScript>().SetCardInfo(
                cardInfo, this.GetComponent<CardData>().cardSprites[cardInfo.id], 
                raritySprite[cardInfo.rarity], rarityColor[cardInfo.rarity]);
        card.GetComponent<GaleryCardScript>().controllerRef = this;
        card.transform.SetParent(galeryMenu.transform, false);
        card.transform.position = new Vector3(Screen.width / 2 + pos, Screen.height / 2, 0);

        return card;
    }

    public void InicializeCardsInGalery()
    {
        if(cardsInGalery.Count == 0)
        {
            //Debug.Log(distBetweenCards);
            for (int i = 0; i < this.GetComponent<CardData>().cards.Count; i++)
            {
                cardsInGalery.Add(InicializeGaleryCard(this.GetComponent<CardData>().cards[i], distBetweenCards * i));
            }
        }
        else
        {
            Debug.Log("galery cards still created");
        }
        
    }

    public void ActivateCardsInGalery(bool activate)
    {
        for (int i = 0; i < cardsInGalery.Count; i++) cardsInGalery[i].SetActive(activate);
    }

    public void CleanCardsInGalery()
    {
        for (int i = 0; i < cardsInGalery.Count; i++) {
            Destroy(cardsInGalery[i]);
        }
        cardsInGalery.Clear();
    }

    void unlockAll() {
        for (int i = 0; i < cardsInGalery.Count; i++) cardsInGalery[i].GetComponent<GaleryCardScript>().UnlockCard();
    }

     public void MoveCards(bool right)
    {
        if (cardsInMovement != 0) return;
        float distToMove = distBetweenCards;
        if (!right) distToMove = -distToMove;
        if ((!right && centredCard == (cardsInGalery.Count - 1)) || (right && centredCard == 0))
        {
            Debug.Log("movement bloqued");
            return;
        }
        for (int i=0;i< cardsInGalery.Count ;i++)
        {
            cardsInGalery[i].GetComponent<GaleryCardScript>().MoveCard(distToMove);
        }
        Debug.Log("centred card is " + centredCard);
        if (right) centredCard--;
        else centredCard++;
        if(this.GetComponent<CardData>().cards[centredCard].unlocked) GetComponent<AudioManager>().PlayMonsterSound(centredCard);
    }
}
