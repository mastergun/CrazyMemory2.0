using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardScript : MonoBehaviour {

    public int id;
    public Sprite cardTexture;
    public SpriteRenderer spriteRef;
    Vector2 gridPos;
   
    // Use this for initialization
    void Start () {
        if(cardTexture != null) spriteRef.sprite = cardTexture;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ResetCard()
    {
        this.GetComponent<Animator>().SetBool("Rotate", false);
    }

    public void RotateCard()
    {
        Debug.Log("rotate");
        this.GetComponent<Animator>().SetBool("Reversed", !this.GetComponent<Animator>().GetBool("Reversed"));
        this.GetComponent<Animator>().SetBool("Rotate", true);
    }

    public void SetCardTexture(Sprite texture)
    {
        spriteRef.sprite = texture;
    }
}
