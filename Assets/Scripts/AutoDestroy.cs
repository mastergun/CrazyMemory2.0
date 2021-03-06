﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoDestroy : MonoBehaviour {

    public float lifeTime;
    public float speed;
    public string textIn;
    float dt = 0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        dt += 0.01f;
        this.transform.position = new Vector2(transform.position.x, transform.position.y + speed);
        if (dt > lifeTime / 2) GetComponent<Text>().text = textIn;
        if (dt > lifeTime) Destroy(this.gameObject);
	}
}
