using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playChimeraSound : MonoBehaviour
{
    AudioSource audioData;
    // Start is called before the first frame update
    void Start()
    {
        audioData = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void playWhipSound(){
        audioData.Play(0);
    }
}
