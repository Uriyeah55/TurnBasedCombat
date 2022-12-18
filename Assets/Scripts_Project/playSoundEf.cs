using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playSoundEf : MonoBehaviour
{
    AudioSource audioS;
    public AudioClip[] sfxClips;
    // Start is called before the first frame update
    void Start()
    {
        audioS = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void playSFX(int clipPosition)
	{
         audioS.clip = sfxClips[clipPosition];
         audioS.Play();
	}
}
