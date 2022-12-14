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

	//public GameObject playerPrefab;
	//public GameObject enemyPrefab;

	//public Transform playerBattleStation;
	//public Transform enemyBattleStation;
 [Header("Cameras")]
	public GameObject mainCam;
	public GameObject enemyCam;
	Animator camAnimator;
	Unit playerUnit;
	Unit enemyUnit;

	public BattleHUD playerHUD;
	public BattleHUD enemyHUD;

	public BattleState state;
	 [Header("GameObjects")]
	public GameObject playerGO;
	public GameObject enemyGO;
		//personas
	public GameObject skeleton;
	public GameObject personaEnemy;

	Animator personaPlayerAC;

	public GameObject player;
	public GameObject enemy;
 [Header("Animators")]
	Animator playerAC;
	Animator enemyAC;
		bool isDead; 

	//cameras
	public GameObject skeletonCameraws;
	public GameObject enemyPersonaCam;

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
		hideSkillButtons();
		camAnimator= mainCam.gameObject.GetComponent<Animator>();


		personaPlayerAC= skeleton.GetComponent<Animator>();
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
	//	Debug.Log("player HP: " + playerUnit.currentHP);
	}

	IEnumerator SetupBattle()
	{
		attackAnnouncer.enabled=false;
		textAttackAnnouncer.enabled=false;
		//yield return new WaitForSeconds(0.5f);

		hideSkillButtons();
		//dialogPanel.gameObject.SetActive(true);
		//dialogText.text = "It's about time... I taught you a lesson!";

		//Adachi line
		//playSound(0);
		playerUnit = playerGO.GetComponent<Unit>();
		enemyUnit = enemyGO.GetComponent<Unit>();


		//playerHUD.SetHUD(playerUnit);
		//enemyHUD.SetHUD(enemyUnit);

		yield return new WaitForSeconds(0.2f);
		//audioBGMobject.gameObject.SetActive(true);

		state = BattleState.PLAYERTURN;
		PlayerTurn();
		showSkillButtons();
		dialogPanel.gameObject.SetActive(false);
		actorText.enabled=false;


	}

	IEnumerator PlayerAttack()
	{
		hideSkillButtons();
		//fade in skeleton
		//fadeInObject(skeleton);
		//check if finishes enemy
		skeleton.SetActive(true);
		bool isDead = enemyUnit.TakeDamage(playerUnit.damage);
		showAttackName("Bite");
		//set hp
		enemyHUD.SetHP(enemyUnit.currentHP);
		//dialogPanel.gameObject.SetActive(true);
		//dialogText.text = "Player: the attack deals " + playerUnit.damage + " points of damage!";

		//persona (skeleton) events
		skeletonCameraws.SetActive(true);
		personaPlayerAC.SetTrigger("attack");

		yield return new WaitForSeconds(2f);
		textAttackAnnouncer.enabled=false;
		attackAnnouncer.enabled=false;
		skeletonCameraws.SetActive(false);
		//skeleton.SetActive(false);
		//dialogPanel.gameObject.SetActive(false);

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

	IEnumerator PlayerStrongAttack()
	{
		hideSkillButtons();
		//fade in skeleton
		//fadeInObject(skeleton);
		//check if finishes enemy
		skeleton.SetActive(true);
		bool isDead = enemyUnit.TakeDamage(playerUnit.damage + 10);
		showAttackName("Snake whip");
		//set hp
		enemyHUD.SetHP(enemyUnit.currentHP);
		//dialogPanel.gameObject.SetActive(true);
		//dialogText.text = "Player: the attack deals " + playerUnit.damage + " points of damage!";

		//persona (skeleton) events
		skeletonCameraws.SetActive(true);
		personaPlayerAC.SetTrigger("strongAttack");

		yield return new WaitForSeconds(2f);
		textAttackAnnouncer.enabled=false;
		attackAnnouncer.enabled=false;
		skeletonCameraws.SetActive(false);
		//skeleton.SetActive(false);
		//dialogPanel.gameObject.SetActive(false);

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

	IEnumerator EscapeAttempt()
	{
		hideSkillButtons();
		
		dialogPanel.gameObject.SetActive(true);
		dialogText.text = "Can't escape this battle. Time to face your demons.";

		//quan acabi la animacio desapareix amb fade out
		yield return new WaitForSeconds(3f);
	
		dialogPanel.gameObject.SetActive(false);

	    //camAnimator.Play("idleCam1", 0, 0.25f);
		showSkillButtons();
	}


	IEnumerator EnemyTurn()
	{
		
		hideSkillButtons();

		//decidir atack 1 focus 2 atack 3 super atack 4 canviar song
		int attackChosen = Random.Range(1, 5);
		attackChosen = 4;

		
//		Debug.Log("Alfonso random attack: " + attackChosen);

		while (attackChosen==1 && isFocused)
		{
		//torna a calcular si toca focus i ja esta focus
 		attackChosen = Random.Range(1, 5);
		Debug.Log("ha coincidit random i focused");
		}

		dialogPanel.gameObject.SetActive(true);
		//damageMultiplier will be used to determine if the enemy has buffs
		int damageMultiplier=1;
		switch(attackChosen)
		{
		case 1:
		dialogText.text = enemyUnit.unitName + " is focusing...";
		isFocused=true;
		Debug.Log("atac: " + attackChosen);
		//particules
			break;
			case 2:
			//atac normal
			if(isFocused)
			{
				showAttackName("Focused attack");
				dialogText.text = enemyUnit.unitName + " attacks with more energy! (atac 2 focused) 6 dmg";

				yield return new WaitForSeconds(2f);

				enemyPersonaCam.SetActive(true);

				enemyAC.GetComponent<Animator>().SetTrigger("attack");
				yield return new WaitForSeconds(2f);
				isFocused=false;
				damageMultiplier=2;

			}
			else
			{
				showAttackName("Tackle");
				//dialogText.text = enemyUnit.unitName + " attacks! (atac 2 NO focused) 3 dmg";
				yield return new WaitForSeconds(2f);

				//personaEnemy.SetActive(true);
				enemyPersonaCam.SetActive(true);

				enemyAC.GetComponent<Animator>().SetTrigger("attack");
				yield return new WaitForSeconds(2f);
			}
			break;
			case 3:
			if(isFocused)
			{
				showAttackName("Charged Curse");
				//dialogText.text = enemyUnit.unitName + " charges with a focused epic mega super hit! (atac 3 focused) 12 damage";
				damageMultiplier=4;
				//enemyUnit.damage *= 4;
				isFocused=false;
				//reiniciem damage
				//enemyUnit.damage=enemyUnit.baseDamage;
			}
			else
			{
				showAttackName("Strong will");
				//dialogText.text = enemyUnit.unitName + " charges with a strong attack! (atac 3 NO focused) 6 damage";
				damageMultiplier=2;
				//enemyUnit.damage *= 2;
			}
			break;
			case 4:
				actorText.enabled=true;
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
		textAttackAnnouncer.enabled=false;
		attackAnnouncer.enabled=false;

		
//		Debug.Log("pre switch 2 atac: " + attackChosen + " i boleana " + isFocused);
		
		//si canvia la canço no volem que ataqui
		if(attackChosen != 4)
		{
			switch(attackChosen)
			{		
				case 1:
			personaEnemy.GetComponent<Animator>().SetTrigger("focus");
				break;
				case 2:
			personaEnemy.GetComponent<Animator>().SetTrigger("attack");
				break;
				case 3:
				enemyAC.GetComponent<Animator>().SetTrigger("attackHard");
				break;
			}
			
			yield return new WaitForSeconds(2f);
			if(isFocused){
				isDead = playerUnit.TakeDamage(0);
			}
			else
			{
				isDead = playerUnit.TakeDamage(enemyUnit.damage * damageMultiplier);
				//playerUnit.SetHP(enemyUnit.damage * damageMultiplier - playerUnit.currentHP);
				//dialogText.text =  enemyUnit.unitName + " deals " + enemyUnit.damage * damageMultiplier + " points of damage!";

				yield return new WaitForSeconds(2f);
				dialogPanel.gameObject.SetActive(false);

				enemyPersonaCam.SetActive(false);
				//personaEnemy.SetActive(false);
				playerHUD.SetHP(playerUnit.currentHP);
			}
			yield return new WaitForSeconds(1f);
		}
		
		 //temporalDamageEnemy= enemyUnit.damage;
		 //enemyUnit.damage=0;

		textAttackAnnouncer.enabled=false;
		attackAnnouncer.enabled=false;
		


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
		//dialogText.text = "Choose an action:";
		dialogPanel.gameObject.SetActive(false);
		showSkillButtons();
		showAudioButtons();
	}

	//### SHOW AND HIDE UI ###
	public void showOffensiveSkills()
	{
		
		playerAC.SetInteger("offenseStance", 1);
		//playerAC.SetTrigger("offenseStance");
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
		//playerAC.SetTrigger("defStance");
		playerAC.SetInteger("offenseStance", 2);
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

		playerAC.SetInteger("offenseStance", 0);
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
		playerAC.SetTrigger("heal");

		int amountHealed=5;
		dialogPanel.gameObject.SetActive(true);
		
		if(playerUnit.currentHP==playerUnit.maxHP)
		{
			dialogText.text = "Already full HP!";
		}
		else
		{
			//playerAC.SetTrigger("heal");
			playerUnit.Heal(amountHealed);
			playerHUD.SetHP(playerUnit.currentHP);
			dialogText.text = "You recover " + amountHealed + "HP!";
			personaPlayerAC.SetTrigger("heal");

		}
		yield return new WaitForSeconds(3f);
		dialogPanel.gameObject.SetActive(false);
		attackAnnouncer.enabled=false;
		textAttackAnnouncer.enabled=false;
		state = BattleState.ENEMYTURN;
		StartCoroutine(EnemyTurn());
	}

		IEnumerator PlayerBigHealAttempt()
	{
		hideSkillButtons();

		playerAC.SetInteger("offenseStance",3);
		
		showAttackName("Super Recovery");

		yield return new WaitForSeconds(2f);
		playerAC.SetInteger("offenseStance",0);
		textAttackAnnouncer.enabled=false;
		attackAnnouncer.enabled=false;

		if(playerUnit.currentHP==playerUnit.maxHP)
		{
			dialogPanel.gameObject.SetActive(true);
			dialogText.text = "Already at full HP!";	
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
				dialogText.text = "Super recovery heals you fully! You recover " + amountHealed + "HP!";
				personaPlayerAC.SetInteger("offenseStance",3);
				yield return new WaitForSeconds(2f);
				personaPlayerAC.SetInteger("offenseStance",1);
			}
			else
			{
				combatPanel.gameObject.SetActive(false);
				dialogPanel.gameObject.SetActive(true);
				dialogText.text = "Healing failed";
			}
		}
		
		
		yield return new WaitForSeconds(2f);
		attackAnnouncer.enabled=false;
		textAttackAnnouncer.enabled=false;
		attackAnnouncer.enabled=false;
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
		attackAnnouncer.enabled=true;
		textAttackAnnouncer.enabled=true;
		textAttackAnnouncer.text=attackName;
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
