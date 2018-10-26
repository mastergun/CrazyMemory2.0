﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardData : MonoBehaviour {
    public struct Data
    {
        public int id;
        public string name;
        public string description;
        public bool unlocked;
    }

    public List<Data> cards = new List<Data>();
    public List<Sprite> cardSprites = new List<Sprite>();

	// Use this for initialization

    public void LoadData()
    {
        TextAsset d = Resources.Load<TextAsset>("CardsData");
        string[] allData = d.text.Split(new char[] { '\n' });

        for (int i = 0; i < allData.Length - 1; i++)
        {
            string[] row = allData[i].Split(new char[] { ',' });
            if (row[1] != "")
            {
                Data lineData = new Data();
                int.TryParse(row[0], out lineData.id);
                lineData.name = row[1];
                lineData.description = row[2];

                cards.Add(lineData);
            }
        }
    }

    public void SetCardInfo(int id, bool u)
    {
        Data d = new Data();
        d.id = cards[id].id;
        d.name = cards[id].name;
        d.description = cards[id].description;
        d.unlocked = u;
        cards[id] = d;
    }

    public List<int> SetGridCards(int cardsInGrid, List<Sprite> spriteListRef)
    {
        List<int> ids = new List<int>();
        spriteListRef.Clear();

        int j = 0;
        while (j < (cardsInGrid -1))
        {
            if (ids.Count == cardsInGrid) break;
            int id = Random.Range(0, cardSprites.Count - 1);
            for (int k = 0; k < ids.Count; k++) if (ids[k] == id) break;
            ids.Add(id);
            spriteListRef.Add(cardSprites[id]);
            j++;
        }
        return ids;
    }
}