using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIBattleManager : MonoBehaviour
{

    public Button btnSkills,btnShuffle,btnGenre,btnBack;
    public GameObject skillCardPanel,canvasCombat,camBehindPlayer,shuffleMenu;
    public AnimationManager animManager;
    Animator playerAC;

    // Start is called before the first frame update
    void Start()
    {
        //Obtain player Animator from the Animation Manager script
        animManager=GetComponent<AnimationManager>();
        playerAC=animManager.playerAnimCtrler;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
     public void  showSkills()
     {

        if(GlobalVars.isShuffling){
shuffleMenu.SetActive(true);
        }
        else{
shuffleMenu.SetActive(false);

        }
        btnSkills.gameObject.SetActive(false);
        btnShuffle.gameObject.SetActive(false);
        btnGenre.gameObject.SetActive(false);
        playerAC.SetInteger("currentStance", 2);
		skillCardPanel.SetActive(true);
		canvasCombat.GetComponent<HorizontalSpacingLerp>().StartLerpingSpacing(-40f, -100f, .2f);
        btnBack.gameObject.SetActive(true);
		//CameraTransitionManager.GetComponent<CameraTransitionManager>().StartCameraTransition(1,2);
		camBehindPlayer.GetComponent<Animator>().SetInteger("currentState",0);
    }
         public void  showShuffleMenu()
     {
        btnSkills.gameObject.SetActive(false);
        btnGenre.gameObject.SetActive(false);

        playerAC.SetInteger("currentStance", 2);
		skillCardPanel.SetActive(true);
		canvasCombat.GetComponent<HorizontalSpacingLerp>().StartLerpingSpacing(-40f, -100f, .2f);
        btnBack.gameObject.SetActive(true);
		//CameraTransitionManager.GetComponent<CameraTransitionManager>().StartCameraTransition(1,2);
		camBehindPlayer.GetComponent<Animator>().SetInteger("currentState",0);
    }
        public void showSkillButtons(){
		camBehindPlayer.GetComponent<Animator>().SetInteger("currentState",1);

        btnSkills.gameObject.SetActive(true);
        btnShuffle.gameObject.SetActive(true);
        btnGenre.gameObject.SetActive(true);
        
    }
    public void hideSkillButtons(){

        btnSkills.gameObject.SetActive(false);
        btnShuffle.gameObject.SetActive(false);
        btnGenre.gameObject.SetActive(false);

    }
    	static public void activateShuffleMode()
        {
		    GlobalVars.isShuffling=true;
	    }
        static public void disableShuffleMode()
        {
		    GlobalVars.isShuffling=false;
	    }
		void hideAudioButtons(){

        }
		void hideAttackName(){

        }
        void showAttackName(){
            
        }
}
