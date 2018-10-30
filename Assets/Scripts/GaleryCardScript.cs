using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GaleryCardScript : MonoBehaviour {

    public GameObject unlockedFace;
    public GameObject lockedFace;
    public List<Text> CardTexts;
    public Image monster;
    public Image reverse;
    bool unlocked = false;
    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetCardInfo(CardData.Data cardInfo, Sprite monsterImage, Sprite rarityImage, Color rarity)
    {
        CardTexts[0].text = cardInfo.name;
        CardTexts[1].text = cardInfo.description;
        monster.sprite = monsterImage;
        reverse.sprite = rarityImage;
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
}
