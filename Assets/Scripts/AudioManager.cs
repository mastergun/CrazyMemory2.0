using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour {
    enum effects
    {
        CORRECTANSWER,
        INCORRECTANSWER,
        VICTORY,
        LOSE,
        FLIPCARD,
        CHANGECARD,
        CLICKBUTTON
    }

    public List<AudioClip> gameEffects;
    public List<AudioClip> monsterSounds;
    public Slider effectsSlider;
    public Slider musicSlider;
    private AudioSource audioS;

    float effectsVol;
    float musicVol;
	// Use this for initialization
	void Start () {
        audioS = GetComponent<AudioSource>();

    }
	
	// Update is called once per frame
	void Update () {
        effectsVol = effectsSlider.value;
        musicVol = musicSlider.value;

    }

    public void PlayMonsterSound(int id)
    {
        //float vol = Random.Range(0.5f,0.7f);
        audioS.Stop();
        audioS.PlayOneShot(monsterSounds[id], effectsVol);
    }

    public void PlayGameEffect(int id)
    {
        //float vol = Random.Range(0.5f, 0.7f);
        audioS.Stop();
        audioS.PlayOneShot(gameEffects[id], effectsVol);
    }

    public void SortMonsterAudios(List<int> ids)
    {
        List<AudioClip> newLS = new List<AudioClip>();
        for (int i = 0; i < monsterSounds.Count; i++)
        {
            newLS.Add(monsterSounds[ids[i]]);
        }
        monsterSounds = newLS;
    }
}
