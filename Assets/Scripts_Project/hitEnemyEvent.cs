using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hitEnemyEvent : MonoBehaviour
{
    //momia, no enemy gameobject
    public GameObject enemy;
    Animator enemyAC;
    // Start is called before the first frame update
    void Start()
    {
        enemyAC=enemy.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
       public void triggerEnemyHitAnimation(){
		enemyAC.SetTrigger("hit");
    }
}
