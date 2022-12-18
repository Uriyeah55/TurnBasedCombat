using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

using UnityEngine.UI;


public enum BattleState {START,PLAYERTURN,ENEMYTURN,WON,LOST}

public class BattleSystem : MonoBehaviour
{

 [Header("Audio")]

	AudioSource mainAudioSource;
	public AudioClip[] soundsArray;
	public GameObject audioBGMobject;
	public GameObject BGMpanel;
	public GameObject audioManager;

	//public GameObject playerPrefab;
	//public GameObject enemyPrefab;

	//public Transform playerBattleStation;
	//public Transform enemyBattleStation;

	 [Header("FX")]
	public GameObject focusedEffect;
	public GameObject megidolaonEffect;
	public GameObject firagaEffect;



 	[Header("Cameras")]
	public GameObject mainCam;
	public GameObject enemyCam;
	public GameObject enemyPersonaCam;
	public GameObject playerCam;
	public GameObject zoomFacePlayerCam;
	public GameObject chimeraCameraws;

	Animator camAnimator;
	Unit playerUnit;
	Unit enemyUnit;

	public BattleHUD playerHUD;
	public BattleHUD enemyHUD;

	public BattleState state;
	 [Header("GameObjects")]
	public GameObject SFXContainer;

	public GameObject playerGO;
	public GameObject enemyGO;
		//personas
	public GameObject chimera;
	public GameObject personaEnemy;
	Animator personaPlayerAC;

	public GameObject player;
	public GameObject enemy;
 [Header("Animators")]
	Animator playerAC;
	Animator enemyAC;
		bool isDead; 

	 [Header("Texts")]
	public Text actorText;
	public Text dialogText;
	//UI 
	 [Header("Buttons")]
	public Button buttonAttack;
	public Button buttonAttack2;

	public Button buttonHeal;
	public Button buttonGreatHeal;

	public Button buttonOffenseSkills;
	public Button buttonDefenseSkills;
	public Button escapeBtn;
	public Button backSkills;

	public GameObject dialogPanel;
	public GameObject combatPanel;
	public Image attackAnnouncer;
	public Text textAttackAnnouncer;

	public GameObject previousSongButton;
	public GameObject nextSongButton;



	bool isFocused=false;
	//bool turnFocusedPassed=false;
	//int temporalDamageEnemy=0;
    // Start is called before the first frame update
    void Start()
    {
		//textAttackAnnouncer.enabled=true;
		//attackAnnouncer.enabled=true;
		
		hideSkillButtons();
		camAnimator= mainCam.gameObject.GetComponent<Animator>();


		personaPlayerAC= chimera.GetComponent<Animator>();
		playerAC=player.GetComponent<Animator>();
		enemyAC=enemy.GetComponent<Animator>();

		mainAudioSource = this.gameObject.GetComponent<AudioSource>();
        //collision2 = audiosVoice[1];
        //collision3 = audiosVoice[2];
        //collision4 = audiosVoice[3];
		
		
		state = BattleState.START;
		StartCoroutine(SetupBattle());
    }
	void Update(){
	//	Debug.Log("alfonso damage: " + enemyUnit.damage);
	Debug.Log("esta focus? " + isFocused);
	
	}

	IEnumerator SetupBattle()
	{
		//audioBGMobject.gameObject.SetActive(false);
	/*
		attackAnnouncer.enabled=false;
		textAttackAnnouncer.enabled=false;
		yield return new WaitForSeconds(0.5f);

		hideSkillButtons();
		dialogPanel.gameObject.SetActive(true);
		dialogText.text = "It's about time... I taught you a lesson!";

		//Adachi line
		playSound(0);
		*/
		playerUnit = playerGO.GetComponent<Unit>();
		enemyUnit = enemyGO.GetComponent<Unit>();


		//playerHUD.SetHUD(playerUnit);
		//enemyHUD.SetHUD(enemyUnit);

		yield return new WaitForSeconds(0);
		audioManager.GetComponent<BGM_Selector>().startGame();
		//audioBGMobject.gameObject.SetActive(true);

		state = BattleState.PLAYERTURN;
		PlayerTurn();
		showSkillButtons();
		dialogPanel.gameObject.SetActive(false);
		actorText.enabled=false;


	}
#region 
	IEnumerator PlayerAttack()
	{
		hideSkillButtons();
		hideAudioButtons();
		hideAttackName();
		playSound(4);
		playerCam.SetActive(true);
		playerAC.SetTrigger("miniAttack");
		showAttackName("Firaga");
		yield return new WaitForSeconds(1f);
		playerCam.SetActive(false);
		chimeraCameraws.SetActive(true);
		personaPlayerAC.SetTrigger("attack");
		yield return new WaitForSeconds(1.5f);
		hideAttackName();

		chimeraCameraws.SetActive(false);
		enemyPersonaCam.SetActive(true);
		firagaEffect.SetActive(true);

		StartCoroutine(personaEnemyDelayHit());
		yield return new WaitForSeconds(3f);
		bool isDead = enemyUnit.TakeDamage(playerUnit.damage);
		playerAC.SetInteger("currentStance",0);

		enemyHUD.SetHP(enemyUnit.currentHP);
		enemyPersonaCam.SetActive(false);

	    camAnimator.Play("idleCam1", 0, 0.25f);

		if(isDead)
		{
			state = BattleState.WON;
			EndBattle();
		} else
		{
			state = BattleState.ENEMYTURN;
			StartCoroutine(EnemyTurn());
		}
	}
	#endregion

	IEnumerator PlayerStrongAttack()
	{
		hideAttackName();
		hideSkillButtons();
		hideAudioButtons();
		playSound(7);
		playerCam.SetActive(true);
		playerAC.SetTrigger("superAttack");
		showAttackName("Megidolaon");
		yield return new WaitForSeconds(1f);
		playerCam.SetActive(false);
		chimeraCameraws.SetActive(true);
		personaPlayerAC.SetTrigger("strongAttack");
		yield return new WaitForSeconds(1.2f);
		hideAttackName();
		playerAC.SetInteger("currentStance",0);
		megidolaonEffect.SetActive(true);

		enemyPersonaCam.SetActive(true);
		StartCoroutine(personaEnemyDelayHit());
		yield return new WaitForSeconds(3f);
		bool isDead = enemyUnit.TakeDamage(playerUnit.damage + 10);
		enemyHUD.SetHP(enemyUnit.currentHP);
		chimeraCameraws.SetActive(false);
		enemyPersonaCam.SetActive(false);

		//chimera.SetActive(false);
		//dialogPanel.gameObject.SetActive(false);

	    camAnimator.Play("idleCam1", 0, 0.25f);
		yield return new WaitForSeconds(2f);

		if(isDead)
		{
			state = BattleState.WON;
			EndBattle();
		} else
		{
			state = BattleState.ENEMYTURN;
			StartCoroutine(EnemyTurn());
		}
	}

	IEnumerator EscapeAttempt()
	{
		hideSkillButtons();
		hideAudioButtons();
		dialogPanel.gameObject.SetActive(true);
		dialogText.enabled=true;
		dialogText.text = "Can't escape this battle. Time to face your demons.";

		//quan acabi la animacio desapareix amb fade out
		yield return new WaitForSeconds(3f);

		showAudioButtons();
		dialogPanel.gameObject.SetActive(false);
		showSkillButtons();
	}


	IEnumerator EnemyTurn()
	{
		megidolaonEffect.SetActive(false);
		//focusedEffect.SetActive(false);
		firagaEffect.SetActive(false);
		yield return new WaitForSeconds(1f);
		hideSkillButtons();
		hideAudioButtons();
		hideAttackName();
		//dialogPanel.SetActive(false);
		//decidir atack 1 focus 2 atack 3 super atack 4 canviar song
		int attackChosen = Random.Range(1, 5);

		while (attackChosen==1 && isFocused)
		{
		//torna a calcular si toca focus i ja esta focus
 		attackChosen = Random.Range(1, 5);
		Debug.Log("ha coincidit random i focused");
		}

		Debug.Log("atack post random: " + attackChosen);

		//damageMultiplier will be used to determine if the enemy has buffs
		int damageMultiplier=1;
		switch(attackChosen)
		{
		case 1:
		enemyPersonaCam.SetActive(true);
		showAttackName("Focus");
		isFocused=true;
		focusedEffect.SetActive(true);
		Debug.Log("FOCUS");
	
		break;
		case 2:
		//atac normal
		if(isFocused)
		{
			showAttackName("Focused attack");
			dialogPanel.SetActive(false);
			yield return new WaitForSeconds(1f);
			enemyPersonaCam.SetActive(true);
			enemyAC.GetComponent<Animator>().SetTrigger("attack");
			yield return new WaitForSeconds(2f);
			isFocused=false;
			focusedEffect.SetActive(false);
			damageMultiplier=2;
		}
		else
		{
			showAttackName("Tackle");
			//dialogPanel.SetActive(false);
			//dialogText.text = enemyUnit.unitName + " attacks! (atac 2 NO focused) 3 dmg";

			//personaEnemy.SetActive(true);
			enemyPersonaCam.SetActive(true);
			yield return new WaitForSeconds(2f);

			enemyAC.GetComponent<Animator>().SetTrigger("attack");
			//yield return new WaitForSeconds(2f);
		}
			break;
			case 3:
			if(isFocused)
			{
				showAttackName("Charged Curse");
				//dialogPanel.SetActive(false);

				//dialogText.text = enemyUnit.unitName + " charges with a focused epic mega super hit! (atac 3 focused) 12 damage";
				damageMultiplier=4;
				//enemyUnit.damage *= 4;
				isFocused=false;
				focusedEffect.SetActive(false);
				//reiniciem damage
				//enemyUnit.damage=enemyUnit.baseDamage;
			}
			else
			{
				showAttackName("Strong will");
				//dialogPanel.SetActive(false);

				//dialogText.text = enemyUnit.unitName + " charges with a strong attack! (atac 3 NO focused) 6 damage";
				damageMultiplier=2;
				//enemyUnit.damage *= 2;
			}
			break;
			case 4:
				dialogPanel.SetActive(true);
				actorText.enabled=true;
				dialogText.enabled=true;
				actorText.text="Alfonso";
				dialogText.text = "I'm already tired of this song!";
				yield return new WaitForSeconds(2f);
				actorText.enabled=false;
				combatPanel.SetActive(false);
				enemyCam.SetActive(true);
				playSound(2);
				enemyAC.GetComponent<Animator>().SetTrigger("changeSong");
				yield return new WaitForSeconds(0.4f);
				BGMpanel.GetComponent<BGM_Selector>().playEnemySong();
				yield return new WaitForSeconds(1f);
			
				enemyCam.SetActive(false);
				//alguna animacio a la UI de la tele?

			break;
		}
//		hideAttackName();

		

		
//		Debug.Log("pre switch 2 atac: " + attackChosen + " i boleana " + isFocused);
		
		//si canvia la canço no volem que ataqui
		if(attackChosen != 4)
		{
			switch(attackChosen)
			{		
				case 1:
				personaEnemy.GetComponent<Animator>().SetTrigger("focus");
				yield return new WaitForSeconds(1f);
				break;
				case 2:
			personaEnemy.GetComponent<Animator>().SetTrigger("attack");
				break;
				case 3:
				enemyCam.SetActive(true);
				enemyAC.GetComponent<Animator>().SetTrigger("attackHard");
				yield return new WaitForSeconds(2f);
				enemyCam.SetActive(false);
				enemyPersonaCam.SetActive(true);
				personaEnemy.GetComponent<Animator>().SetTrigger("strong");
				break;
			}
			
			yield return new WaitForSeconds(1f);
			enemyPersonaCam.SetActive(false);
			if(isFocused)
			{
				isDead = playerUnit.TakeDamage(0);
			}
			else
			{
				isDead = playerUnit.TakeDamage(enemyUnit.damage * damageMultiplier);
				//playerUnit.SetHP(enemyUnit.damage * damageMultiplier - playerUnit.currentHP);
				//dialogText.text =  enemyUnit.unitName + " deals " + enemyUnit.damage * damageMultiplier + " points of damage!";
				dialogPanel.gameObject.SetActive(false);

				//personaEnemy.SetActive(false);
				playerHUD.SetHP(playerUnit.currentHP);
			}
			//yield return new WaitForSeconds(1f);
		}
		
		 //temporalDamageEnemy= enemyUnit.damage;
		 //enemyUnit.damage=0;

	
		


		if(isDead)
		{
			state = BattleState.LOST;
			EndBattle();
		} else
		{
			state = BattleState.PLAYERTURN;
			PlayerTurn();
		}
	}

	void EndBattle()
	{
		if(state == BattleState.WON)
		{
			dialogText.text = "You won the battle!";
		} else if (state == BattleState.LOST)
		{
			dialogText.text = "You were defeated.";
		}
	}

	void PlayerTurn()
	{
		hideAttackName();
		//dialogText.text = "Choose an action:";
		//si li queda mitja vida o menys activa cutscene
		if(enemyUnit.GetComponent<Unit>().currentHP<=enemyUnit.GetComponent<Unit>().maxHP/2){
			Debug.Log("activar cutscene end");
			zoomFacePlayerCam.SetActive(true);
		BGMpanel.GetComponent<BGM_Selector>().stopSongs();
			//so showtime
			playerAC.SetTrigger("reacting");
			
			playSound(3);
		}
		else{
			dialogPanel.gameObject.SetActive(false);
			showSkillButtons();
			showAudioButtons();
		}
		
	}

	//### SHOW AND HIDE UI ###
	public void showOffensiveSkills()
	{
		playSound(6);
		playerAC.SetInteger("currentStance", 1);
		//playerAC.SetTrigger("currentStance");
		buttonAttack.gameObject.SetActive(true);
		buttonAttack2.gameObject.SetActive(true);

		buttonHeal.gameObject.SetActive(false);
		buttonGreatHeal.gameObject.SetActive(false);

		buttonOffenseSkills.gameObject.SetActive(false);
		buttonDefenseSkills.gameObject.SetActive(false);
		escapeBtn.gameObject.SetActive(false);		
		backSkills.gameObject.SetActive(true);
	}
	public void showDefenseSkills()
	{
		playSound(6);
		//playerAC.SetTrigger("defStance");
		playerAC.SetInteger("currentStance", 2);
		buttonHeal.gameObject.SetActive(true);
		buttonGreatHeal.gameObject.SetActive(true);

		buttonAttack.gameObject.SetActive(false);
		buttonAttack2.gameObject.SetActive(false);

		buttonOffenseSkills.gameObject.SetActive(false);
		buttonDefenseSkills.gameObject.SetActive(false);
		escapeBtn.gameObject.SetActive(false);		
		backSkills.gameObject.SetActive(true);

	}
	public void hideSkillButtons()
	{
			combatPanel.gameObject.SetActive(false);

		buttonAttack.gameObject.SetActive(false);
		buttonAttack2.gameObject.SetActive(false);
		buttonHeal.gameObject.SetActive(false);
		buttonGreatHeal.gameObject.SetActive(false);
		buttonOffenseSkills.gameObject.SetActive(false);
		buttonDefenseSkills.gameObject.SetActive(false);
		escapeBtn.gameObject.SetActive(false);		
		backSkills.gameObject.SetActive(false);

	}
	public void hideAudioButtons()
	{
		previousSongButton.gameObject.SetActive(false);
		nextSongButton.gameObject.SetActive(false);
	}
	public void showAudioButtons()
	{
		previousSongButton.gameObject.SetActive(true);
		nextSongButton.gameObject.SetActive(true);
	}

	public void showSkillButtons()
	{

		playerAC.SetInteger("currentStance", 0);
		combatPanel.gameObject.SetActive(true);

//		playerAC.SetTrigger("iddleStance");
		
		buttonHeal.gameObject.SetActive(false);
		buttonAttack.gameObject.SetActive(false);

		buttonAttack2.gameObject.SetActive(false);
		buttonGreatHeal.gameObject.SetActive(false);

		buttonOffenseSkills.gameObject.SetActive(true);
		buttonDefenseSkills.gameObject.SetActive(true);
		escapeBtn.gameObject.SetActive(true);		
		backSkills.gameObject.SetActive(false);

	}



	IEnumerator PlayerHeal()
	{
		hideSkillButtons();
		showAttackName("Healing");
		hideAudioButtons();

		int amountHealed=5;
		dialogPanel.gameObject.SetActive(true);
		
		if(playerUnit.currentHP==playerUnit.maxHP)
		{
			dialogText.text = "Already full HP!";
			playerAC.SetInteger("currentStance",0);

		}
		else
		{
		playerCam.SetActive(true);
			playerAC.SetTrigger("heal");
			playerUnit.Heal(amountHealed);
			playerHUD.SetHP(playerUnit.currentHP);
			dialogPanel.SetActive(true);
			dialogText.enabled=true;
			dialogText.text = "You recover " + amountHealed + "HP!";
			playSFXPosition(1);
			personaPlayerAC.SetTrigger("heal");
		}
		yield return new WaitForSeconds(2f);
		hideAttackName();
		playerCam.SetActive(false);

		dialogPanel.gameObject.SetActive(false);
		
		playerAC.SetInteger("currentStance",0);
		state = BattleState.ENEMYTURN;
		StartCoroutine(EnemyTurn());
	}

	IEnumerator personaEnemyDelayHit()
	{
		yield return new WaitForSeconds(1.5f);
		personaEnemy.GetComponent<Animator>().SetTrigger("receiveHit");

	}

		IEnumerator PlayerBigHealAttempt()
	{
		hideSkillButtons();
		hideAudioButtons();

		playerCam.SetActive(true);
		playerAC.SetInteger("currentStance",3);
		
		showAttackName("Super Recovery");

		yield return new WaitForSeconds(2f);
		hideAttackName();	

		if(playerUnit.currentHP==playerUnit.maxHP)
		{
			dialogPanel.gameObject.SetActive(true);
			dialogText.text = "Already at full HP!";	
			playerAC.SetInteger("currentStance",0);
		}
		else
		{
			//1 exit, la resta fail
			int healingSuccess = Random.Range(1, 6);
	
			int amountHealed;
			if(healingSuccess==1)
			{
				//full vida
				amountHealed=playerUnit.maxHP-playerUnit.currentHP;
				playerUnit.Heal(amountHealed);
				playerHUD.SetHP(playerUnit.currentHP);
				combatPanel.gameObject.SetActive(false);

				dialogPanel.gameObject.SetActive(true);
				dialogText.enabled=true;
				dialogText.text = "Super recovery heals you fully! You recover " + amountHealed + "HP!";
				yield return new WaitForSeconds(2f);
				personaPlayerAC.SetInteger("currentStance",0);
			}
			else
			{
				combatPanel.gameObject.SetActive(false);
				dialogPanel.gameObject.SetActive(true);
				dialogText.enabled=true;
				dialogText.text = "Healing failed";
				playerAC.SetInteger("currentStance",0);

			}
		}
		playerAC.SetInteger("currentStance",0);
		playerCam.SetActive(false);
		yield return new WaitForSeconds(2f);
		hideAttackName();
		
		dialogPanel.gameObject.SetActive(false);
		

		state = BattleState.ENEMYTURN;
		StartCoroutine(EnemyTurn());
	}

	public void OnAttackButton()
	{
		if (state != BattleState.PLAYERTURN)
			return;

		StartCoroutine(PlayerAttack());
	}

		public void OnStrongAttackButton()
	{
		if (state != BattleState.PLAYERTURN)
			return;

		StartCoroutine(PlayerStrongAttack());
	}

	public void OnHealButton()
	{
		if (state != BattleState.PLAYERTURN)
			return;

		StartCoroutine(PlayerHeal());
	}
	public void playSFXPosition(int position){
		SFXContainer.GetComponent<playSoundEf>().playSFX(position);

	}
	public void OnBigHealButton()
	{
		if (state != BattleState.PLAYERTURN)
			return;

		StartCoroutine(PlayerBigHealAttempt());
	}
	public void OnEscapeButton()
	{
		if (state != BattleState.PLAYERTURN)
			return;

		StartCoroutine(EscapeAttempt());
	}

	public void showAttackName(string attackName)
	{
		textAttackAnnouncer.enabled=true;
		attackAnnouncer.enabled=true;
		textAttackAnnouncer.text=attackName;
	}
	public void hideAttackName(){
		textAttackAnnouncer.enabled=false;
		attackAnnouncer.enabled=false;
	}

	public void playSound(int clipPosition)
	{
         mainAudioSource.clip = soundsArray[clipPosition];
         mainAudioSource.Play();
	}
	//method called from other scripts to start the coroutine
	public void startEnemyTurn(){
		StartCoroutine(EnemyTurn());
	}
}
