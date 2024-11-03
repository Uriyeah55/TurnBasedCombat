using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIBattleManager : MonoBehaviour
{

    public Button btnSkills,btnShuffle,btnBack,btnGenre,btnClear;
    public GameObject skillCardPanel,canvasCombat,camBehindPlayer,shuffleMenu;
    public GameObject[] shuffleBack;
    public GameObject shuffleBackGlobal;

    public AnimationManager animManager;

     public List<ShuffleSlot> shuffleSlots = new List<ShuffleSlot>(); // Lista de todos los slots de shuffle
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
            shuffleBackGlobal.SetActive(true);
            shuffleMenu.SetActive(true);
            btnClear.gameObject.SetActive(true);

            foreach (GameObject backSlot in shuffleBack)
            {
                backSlot.SetActive(true);
            }
        }
        else{
            shuffleMenu.SetActive(false);
            btnClear.gameObject.SetActive(false);

            foreach (GameObject backSlot in shuffleBack)
            {
                backSlot.SetActive(false);
                backSlot.gameObject.SetActive(false);
            }

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
        playerAC.SetInteger("currentStance", 2);
		skillCardPanel.SetActive(true);
		canvasCombat.GetComponent<HorizontalSpacingLerp>().StartLerpingSpacing(-40f, -100f, .2f);
        btnBack.gameObject.SetActive(true);
		//CameraTransitionManager.GetComponent<CameraTransitionManager>().StartCameraTransition(1,2);
		camBehindPlayer.GetComponent<Animator>().SetInteger("currentState",0);
    }
        public void showSkillButtons()
        {
		camBehindPlayer.GetComponent<Animator>().SetInteger("currentState",1);

        btnSkills.gameObject.SetActive(true);
        btnShuffle.gameObject.SetActive(true);
        btnGenre.gameObject.SetActive(true);
		skillCardPanel.SetActive(false);
        btnClear.gameObject.SetActive(false);


    }
    public void hideShuffleMenu()
    {
        shuffleMenu.SetActive(false);
        btnClear.gameObject.SetActive(false);
        foreach (GameObject backSlot in shuffleBack)
            {
                backSlot.SetActive(false);
                backSlot.gameObject.SetActive(false);
            }
    }
    public void hideSkillButtons(){

        btnSkills.gameObject.SetActive(false);
        btnShuffle.gameObject.SetActive(false);
        btnGenre.gameObject.SetActive(false);
		skillCardPanel.SetActive(false);
        btnBack.gameObject.SetActive(false);

    }

        public void ClearAllShuffleSlots()
    {
        showShuffleBacks();
        foreach (ShuffleSlot slot in shuffleSlots)
        {
            if (slot != null)
            {
                slot.ClearSlot(); // Llama a ClearSlot en cada slot
            }
        }
        Debug.Log("All shuffle slots have been cleared.");
    }

    public void showShuffleBacks(){
            foreach (GameObject backSlot in shuffleBack)
            {
                backSlot.SetActive(true);
                //backSlot.gameObject.SetActive(true);
            }
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
