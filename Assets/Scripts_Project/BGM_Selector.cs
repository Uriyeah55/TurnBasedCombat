using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BGM_Selector : MonoBehaviour
{
    public Text currentSongName;
    public AudioClip[] BGM_tracks;
    public AudioClip[] enemy_BGM_tracks;
    AudioClip currentEnemySong;
    bool firstCall=true;
    AudioSource audioS;
    int currentIndex=0;
    int currentEnemyIndex=0;

   // int enemyBGMcurrentIndex=0;

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
        Debug.Log("size songs enem" + enemy_BGM_tracks.Length);
        Debug.Log("enemy index" + currentEnemyIndex);

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
        }
         audioS.clip = BGM_tracks[currentIndex];
         audioS.Play();
    }
       public void SubstractIndex()
    {

        currentIndex--;
        if(currentIndex <= 0)
        {
            currentIndex=BGM_tracks.Length;
        }
         audioS.clip = BGM_tracks[currentIndex];
         audioS.Play();
    }
    public void playEnemySong()
    {
currentEnemyIndex++;

        if(currentEnemyIndex >= enemy_BGM_tracks.Length)
        {
        currentEnemyIndex=0;
        }
         audioS.clip = enemy_BGM_tracks[currentEnemyIndex];
         audioS.Play();
    }
}
     

