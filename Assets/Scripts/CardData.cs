using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardData : MonoBehaviour {
    [System.Serializable]
    public struct Data
    {
        public int id;
        public string name;
        public string description;
        public int rarity;
        public bool unlocked;
    }

    public TextAsset d;
    public List<Data> cards = new List<Data>();
    public List<Sprite> cardSprites = new List<Sprite>();

	// Use this for initialization

    public void LoadData()
    {
        string[] allData = d.text.Split(new char[] { '\n' });

        for (int i = 1; i < allData.Length; i++)
        {
            string[] row = allData[i].Split(new char[] { ',' });
            if (row[1] != "")
            {
                Data lineData = new Data();
                int.TryParse(row[0], out lineData.id);
                lineData.name = row[1];
                lineData.description = row[2];
                int.TryParse(row[3], out lineData.rarity);
                bool.TryParse(row[4], out lineData.unlocked);
                //Debug.Log(lineData.id+ " his name is: " +lineData.name + " his description is: " + lineData.description + 
                   // " rarity: " + lineData.rarity + " is unlocked ? : "+ lineData.unlocked );
                cards.Add(lineData);
            }
        }
        //this.GetComponent<GaleryController>().InicializeCardsInGalery();
    }

    public void SetCardInfo(int id, bool u)
    {
        Data d = new Data();
        d.id = cards[id].id;
        d.name = cards[id].name;
        d.description = cards[id].description;
        d.rarity = cards[id].rarity;
        d.unlocked = u;
        cards[id] = d;
    }

    public List<int> SetGridCards(int cardsInGrid, List<Sprite> spriteListRef)
    {
        List<int> ids = new List<int>();
        spriteListRef.Clear();
        
        int j = 0;
        while (j < (cardsInGrid))
        {
            if (ids.Count == cardsInGrid) break;
            int id = Random.Range(0, cardSprites.Count - 1);
            bool repited = false;
            //revisar per bug!
            for (int k = 0; k < ids.Count; k++) if (ids[k] == id || cards[id].rarity > GetComponent<ScoreManager>().GetCurrentDifficult()) {
                    Debug.Log(GetComponent<ScoreManager>().GetCurrentDifficult());
                    repited = true;
                    break;
            }
            if (!repited)
            {
                ids.Add(id);
                spriteListRef.Add(cardSprites[id]);
                j++;
            }
        }
        return ids;
    }
}
