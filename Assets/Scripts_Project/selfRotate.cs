using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class selfRotate : MonoBehaviour
{
    public float beatFrequency =2;
    public Text playerHPText;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("HeartBeatPlayer",0,2f);
    }

    // Update is called once per frame
    void Update()
    {
        //this.gameObject.transform.Rotate (0.0f, speed, 0.0f);
        if(player.GetComponent<Unit>().currentHP>=player.GetComponent<Unit>().maxHP){
            //text verd
            beatFrequency=2;
        }
    }
    public void HeartBeatPlayer()
    {
		//StartCoroutine(HB());
    }
 

}
 