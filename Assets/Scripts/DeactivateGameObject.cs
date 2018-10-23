using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateGameObject : MonoBehaviour {

    public void Activate(bool activate)
    {
        this.gameObject.SetActive(activate);
    }
}
