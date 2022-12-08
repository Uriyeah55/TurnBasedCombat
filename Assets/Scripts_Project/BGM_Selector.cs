using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BGM_Selector : MonoBehaviour
{
    public Text currentSongName;
    public AudioClip[] BGM_tracks;
    public AudioClip[] enemy_BGM_tracks;
    bool playerSongPlaying;
    AudioSource audioS;
    int currentIndex=0;
   // int enemyBGMcurrentIndex=0;

    // Start is called before the first frame update
    void Start()
    {
        audioS=GetComponent<AudioSource>();
        audioS.clip = BGM_tracks[currentIndex];
        audioS.Play();
        playerSongPlaying=false;

    }

    // Update is called once per frame
    void Update()
    {
        currentSongName.text= audioS.clip.ToString();
    }
    public void playSound(int clipPosition)
	{
        playerSongPlaying=true;
         audioS.clip = BGM_tracks[clipPosition];
         audioS.Play();
	}
    public void AddIndex()
    {
        playerSongPlaying=true;

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
        playerSongPlaying=true;

        currentIndex--;
        if(currentIndex <= 0)
        {
            currentIndex=BGM_tracks.Length;
        }
         audioS.clip = BGM_tracks[currentIndex];
         audioS.Play();
    }
    public void playRandomEnemySong()
    {
        //es tria una canço de enemy
        int indexEnemySong = Random.Range(0, enemy_BGM_tracks.Length);
    Debug.Log("ha toca la enemy song " + enemy_BGM_tracks[indexEnemySong].name);
        bool found=false;
        if(playerSongPlaying)
        {
        //es busca si el nom de la que ha tocat es la mateixa que sona al Audiosource actualment
        foreach(AudioClip a in enemy_BGM_tracks)
        {
            Debug.Log("foreach songs, " + a.name);

            if (a.name==enemy_BGM_tracks[indexEnemySong].name)
            {
                Debug.Log("ha coincidit " + a.name + " amb " + enemy_BGM_tracks[indexEnemySong].name);
                found=true;
            }
        }
        //si ha coincidit, carreguem un altre random fins que no coincideixi el nom de la que toqui amb l'actual
        if(found)
        {
                Debug.Log("1-entra al if coincidit");

 		        int newSongToPlay = Random.Range(0, enemy_BGM_tracks.Length);
                Debug.Log("2-la nova que toca es " + enemy_BGM_tracks[newSongToPlay].name);
                Debug.Log("2.5-compara " + enemy_BGM_tracks[newSongToPlay].name + " amb " + enemy_BGM_tracks[indexEnemySong].name);


            while (enemy_BGM_tracks[indexEnemySong].name== enemy_BGM_tracks[newSongToPlay].name)
            {

                newSongToPlay = Random.Range(0, enemy_BGM_tracks.Length);
                Debug.Log("3-WHILE: es genera la " + enemy_BGM_tracks[newSongToPlay].name + "al bucle");

            }
            //es canvia la canço per una que no coincideixi
            audioS.clip = enemy_BGM_tracks[newSongToPlay];


        }
        else
        {
             //s'utilitza el random del principi
            audioS.clip = enemy_BGM_tracks[indexEnemySong];
        }

        }
        
       
       
       audioS.Play();
    }
}
     

