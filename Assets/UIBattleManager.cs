using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIBattleManager : MonoBehaviour
{

    public Button btnSkills,btnShuffle,btnBack;
    public GameObject skillCardPanel,canvasCombat,camBehindPlayer;
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
        btnSkills.gameObject.SetActive(false);
        playerAC.SetInteger("currentStance", 2);
		skillCardPanel.SetActive(true);
		canvasCombat.GetComponent<HorizontalSpacingLerp>().StartLerpingSpacing(-40f, -100f, .2f);
        btnBack.gameObject.SetActive(true);
		//CameraTransitionManager.GetComponent<CameraTransitionManager>().StartCameraTransition(1,2);
		camBehindPlayer.GetComponent<Animator>().SetInteger("currentState",0);
    }
    public void hideSkillButtons(){

        btnSkills.gameObject.SetActive(false);
        btnShuffle.gameObject.SetActive(false);
    }
		void hideAudioButtons(){

        }
		void hideAttackName(){

        }
        void showAttackName(){
            
        }
}
