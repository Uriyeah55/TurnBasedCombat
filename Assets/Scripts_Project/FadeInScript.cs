using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInScript : MonoBehaviour
{
    SpriteRenderer rend;
    // Start is called before the first frame update
    void Start()
    {
        rend =GetComponent<SpriteRenderer>();
        Color c=rend.material.color;
        c.a=0f;
        rend.material.color=c;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator FadeIn(){
        for(float f = 0.0f;f<=1;f+=0.05f){
            Color c=rend.material.color;
            c.a=f;
            rend.material.color=c;
            yield return new WaitForSeconds(0.05f);
        }
    }
    public void startFading(){
        StartCoroutine("FadeIn");
    }
}
 