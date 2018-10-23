using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardScript : MonoBehaviour {

    public int id;
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
    public bool reversed = false;

    // Use this for initialization
    void Start () {
        if(cardTexture != null) spriteRef.sprite = cardTexture;
    }
	
	// Update is called once per frame
	void Update () {
        if (moving)
        {
            Vector3 newDir = (desiredPos - root.position).normalized;
            if (Mathf.Cos(Vector3.Angle(dir, newDir)) < 0)
            {
                root.position = desiredPos;
                moving = false;
                dir = Vector2.zero;
                gridRef.CardEndMovement();
            }
            else root.position += dir * speed;
        }
	}

    public void ResetCard()
    {
        b = false;
        this.GetComponent<Animator>().SetBool("Rotate", false);
        Debug.Log("unlocked");
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
        if (!moving && !b) return true;
        else return false;
    }

    public void AutoDestroy()
    {
        gridRef.RemoveCardFromPull(this.transform.parent.gameObject);
        Destroy(this.transform.parent.gameObject);
    }
}
