using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {
    enum effects
    {
        CORRECTANSWER,
        INCORRECTANSWER,
        VICTORY,
        LOSE,
        FLIPCARD,
        CHANGECARD
    }

    public List<AudioClip> gameEffects;
    public List<AudioClip> monsterSounds;
    private AudioSource audioS;
	// Use this for initialization
	void Start () {
        audioS = GetComponent<AudioSource>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PlayMonsterSound(int id)
    {
        float vol = Random.Range(0.5f,0.7f);
        audioS.Stop();
        audioS.PlayOneShot(monsterSounds[id], vol);
    }

    public void PlayGameEffect(int id)
    {
        float vol = Random.Range(0.5f, 0.7f);
        audioS.Stop();
        audioS.PlayOneShot(gameEffects[id], vol);
    }
}
