using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class alpha50 : MonoBehaviour
{
    Image image;
    void Start () 
    {
          image = this.GetComponent<Image>();
          var tempColor = image.color;
          tempColor.a = 0.5f;
          image.color = tempColor;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
