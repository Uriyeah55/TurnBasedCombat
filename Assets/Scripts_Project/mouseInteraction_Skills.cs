using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
 using UnityEngine.EventSystems;

public class mouseInteraction_Skills : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
 {
    Image img;
    Text _text;
     private bool mouse_over = false;
     void Start(){

        img=this.gameObject.GetComponent<Image>();
        _text=this.GetComponentInChildren<Text>();
     }
     public void OnPointerEnter(PointerEventData eventData)
     {
         mouse_over = true;
         img.enabled=true;
        _text.color = Color.yellow;
     }
 
     public void OnPointerExit(PointerEventData eventData)
     {
         _text.color = Color.black;
         img.enabled=false;
         mouse_over = false;
     }
      public void escapeAttempt(){
        if(gameObject.name=="EscapeBtn"){
        }
     }
 }