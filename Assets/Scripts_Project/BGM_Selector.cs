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
    AudioSource audioS;
    int currentIndex=0;
    int currentEnemyIndex=0;

    public GameObject BattleSystemGO;

   // int enemyBGMcurrentIndex=0;

    // Start is called before the first frame update
    void Start()
    {/*
        currentSongName.color= Color.black;
        BGM_tracks[1].name.Replace('s', 'G');
        audioS=GetComponent<AudioSource>();
        audioS.clip = BGM_tracks[currentIndex];
        audioS.Play();
   
        updateSongText(audioS.clip.name.ToString());
        */
    }

    // Update is called once per frame
    void Update()
    {

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
         BattleSystemGO.GetComponent<BattleSystem>().state= BattleState.ENEMYTURN;
         BattleSystemGO.GetComponent<BattleSystem>().startEnemyTurn();
         BattleSystemGO.GetComponent<BattleSystem>().hideAudioButtons();
         updateSongText(BGM_tracks[currentIndex].name);
        currentSongName.color= Color.black;

    }
       public void SubstractIndex()
    {

        currentIndex--;
        if(currentIndex < 0)
        {
            currentIndex=BGM_tracks.Length-1;
        }
         audioS.clip = BGM_tracks[currentIndex];
         audioS.Play();
         BattleSystemGO.GetComponent<BattleSystem>().state= BattleState.ENEMYTURN;
         BattleSystemGO.GetComponent<BattleSystem>().startEnemyTurn();
         BattleSystemGO.GetComponent<BattleSystem>().hideAudioButtons();

        updateSongText(BGM_tracks[currentIndex].name);
        currentSongName.color= Color.black;


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
        updateSongText(enemy_BGM_tracks[currentEnemyIndex].name.ToString());
        currentSongName.color= Color.red;

    }
    public void updateSongText(string name)
    {
        name.Replace("(UnityEngine.AudioClip)","");
        currentSongName.text=name;
    }
    public void startGame(){
        currentSongName.color= Color.black;
        BGM_tracks[1].name.Replace('s', 'G');
        audioS=GetComponent<AudioSource>();
        audioS.clip = BGM_tracks[currentIndex];
        audioS.Play();
   
        updateSongText(audioS.clip.name.ToString());
    }
}
     

