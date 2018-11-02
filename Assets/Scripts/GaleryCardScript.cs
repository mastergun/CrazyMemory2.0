using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GaleryCardScript : MonoBehaviour {

    public GameObject unlockedFace;
    public GameObject lockedFace;
    public GaleryController controllerRef;
    public List<Text> CardTexts;

    public Image monster;
    public Image reverse;

    public float speed;
    bool unlocked = false;
    bool moving = false;
    Vector3 desiredPos;
    Vector3 dir;
    

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (moving)
        {
            Vector3 newDir = (desiredPos - this.transform.position).normalized;
            if (Mathf.Cos(Vector3.Angle(dir, newDir)) < 0)
            {
                this.transform.position = desiredPos;
                moving = false;
                controllerRef.cardsInMovement--;
                dir = Vector2.zero;
            }
            else this.transform.position += dir * speed * (Screen.currentResolution.width/5);
        }
	}

    public void SetCardInfo(CardData.Data cardInfo, Sprite monsterImage, Color rarity, Sprite rarityImage = null)
    {
        CardTexts[0].text = cardInfo.name;
        CardTexts[1].text = cardInfo.description;
        monster.sprite = monsterImage;
        if(rarityImage != null)reverse.sprite = rarityImage;
        this.GetComponent<Image>().color = rarity;
        if (cardInfo.unlocked) UnlockCard();
    }

    public void UnlockCard()
    {
        unlocked = true;
        lockedFace.SetActive(false);
        unlockedFace.SetActive(true);
    }

    public void Autodestroy()
    {
        Destroy(transform.parent);
    }

    public void MoveCard(float dist)
    {
        desiredPos.x = this.transform.position.x + dist;
        desiredPos.y = this.transform.position.y;
        dir = (desiredPos - this.transform.position).normalized;
        controllerRef.cardsInMovement++;
        moving = true;
    }
}
