using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playSoundEf : MonoBehaviour
{
    AudioSource audioData;
    public AudioClip[] audioSources;
    // Start is called before the first frame update
    void Start()
    {
        audioData = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void playSound(){
        audioData.Play(0);
    }
}
