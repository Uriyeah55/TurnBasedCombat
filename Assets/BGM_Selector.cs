using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BGM_Selector : MonoBehaviour
{
    public Text currentSongName;
    public AudioClip[] BGM_tracks;
    AudioSource audioS;
    int currentIndex=0;

    // Start is called before the first frame update
    void Start()
    {
        audioS=GetComponent<AudioSource>();
        audioS.clip = BGM_tracks[currentIndex];
         audioS.Play();

    }

    // Update is called once per frame
    void Update()
    {
        currentSongName.text= audioS.clip.ToString();
    }
    public void playSound(int clipPosition)
	{
         audioS.clip = BGM_tracks[clipPosition];
         audioS.Play();
	}
    public void AddIndex()
    {
        currentIndex++;

        if(currentIndex >= BGM_tracks.Length)
        {
        currentIndex=0;
        Debug.Log("arribat al tope");
        }
         audioS.clip = BGM_tracks[currentIndex];
         audioS.Play();
    }
       public void SubstractIndex()
    {
        currentIndex--;
        if(currentIndex <= 0)
        {
        currentIndex=BGM_tracks.Length - 1;
        }
         audioS.clip = BGM_tracks[currentIndex];
         audioS.Play();
    }
}
