using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShaker : MonoBehaviour {

    public Camera camera; // set this via inspector
    Vector3 initPos;
    float shake = 0;
    public float shakeAmount = 0.2f;
    public float timeOnShake = 0.5f;
    public float decreaseFactor= 2.0f;
 
    // Use this for initialization
    void Start () {
        initPos = camera.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        if (shake > 0)
        {
            camera.transform.localPosition = Random.insideUnitSphere * shakeAmount;
            shake -= Time.deltaTime * decreaseFactor;

        }
        else
        {
            shake = 0.0f;
            camera.transform.position = initPos;
        }
    }

    public void MakeCameraShake()
    {
        shake = timeOnShake;
    }
}
