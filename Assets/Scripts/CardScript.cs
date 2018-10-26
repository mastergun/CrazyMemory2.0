using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardScript : MonoBehaviour {
    enum CardState
    {
        WAITING,
        BLOQUED,
        MOVING,
        DYING,
    }

    public int id;
    public int spriteId;
    public Sprite cardTexture; //deprecated ?

    public SpriteRenderer spriteRef;
    public Vector2 cardSize;
    public float speed;
    public Transform root;

    public GridGenerator gridRef;

    Vector2 gridPos;
    Vector3 desiredPos;
    Vector3 dir;
    bool moving = false;
    bool b = false;
    public bool bloqued = false;
    public bool reversed = false;

    CardState cs = CardState.WAITING;

    // Use this for initialization
    void Start () {
        if(cardTexture != null) spriteRef.sprite = cardTexture;
    }
	
	// Update is called once per frame
	void Update () {
        switch (cs)
        {
            case CardState.WAITING:
                break;
            case CardState.MOVING:
                Vector3 newDir = (desiredPos - root.position).normalized;
                if (Mathf.Cos(Vector3.Angle(dir, newDir)) < 0)
                {
                    root.position = desiredPos;
                    moving = false;
                    cs = CardState.WAITING;
                    dir = Vector2.zero;
                    gridRef.CardEndMovement();
                }
                else root.position += dir * speed;

                break;
            case CardState.BLOQUED:
                break;
            case CardState.DYING:
                //if(!b) Destroy(this.transform.parent.gameObject);
                break;

        }
    }

    public void ResetCard()
    {
        b = false;
        this.GetComponent<Animator>().SetBool("Rotate", false);
    }

    public void RotateCard()
    {
        b = true;
        reversed = !reversed;
        this.GetComponent<Animator>().SetBool("Reversed", !this.GetComponent<Animator>().GetBool("Reversed"));
        this.GetComponent<Animator>().SetBool("Rotate", true);
    }

    public void SetCardTexture(Sprite texture)
    {
        spriteRef.sprite = texture;
    }

    public void SetGridPos(Vector2 gp)
    {
        if(gp != gridPos)
        {
            gridPos = gp;
            moving = true;
            cs = CardState.MOVING;
            desiredPos.x = gridPos.x * cardSize.x;
            desiredPos.y = gridPos.y * cardSize.y;
            dir = (desiredPos - root.position).normalized;
        }
        else gridRef.CardEndMovement();
    }

    public Vector2 GetGridPos(bool grid)
    {
        Vector2 gp = gridPos;
        if (!grid) gp *= cardSize;
        return gp;
    }

    public bool CanMove()
    {
        if (!moving && !b && !bloqued) return true;
        //if (cs == CardState.WAITING) return true;
        else return false;
    }

    public void AutoDestroy()
    {
        gridRef.RemoveCardFromPull(this.transform.parent.gameObject);
        //cs = CardState.DYING;
        Destroy(this.transform.parent.gameObject);
    }
}
