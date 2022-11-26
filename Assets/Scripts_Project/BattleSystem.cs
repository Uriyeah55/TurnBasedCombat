using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum BattleState {START,PLAYERTURN,ENEMYTURN,WON,LOST}

public class BattleSystem : MonoBehaviour
{
	//public GameObject playerPrefab;
	//public GameObject enemyPrefab;

	//public Transform playerBattleStation;
	//public Transform enemyBattleStation;

	public GameObject mainCam;
	Animator camAnimator;
	Unit playerUnit;
	Unit enemyUnit;

	public BattleHUD playerHUD;
	public BattleHUD enemyHUD;

	public BattleState state;

	public GameObject playerGO;
	public GameObject enemyGO;

	Animator personaPlayerAC;

	public GameObject player;
	public GameObject enemy;

	Animator playerAC;
	Animator enemyAC;
	//personas
	public GameObject skeleton;
	public GameObject personaEnemy;


	//cameras
	public GameObject skeletonCameraws;
	public GameObject enemyPersonaCam;


	public Text dialogText;
	public Button buttonAttack;
	public Button buttonHeal;

	bool isFocused=false;
	bool turnFocusedPassed=false;
	int temporalDamageEnemy=0;
    // Start is called before the first frame update
    void Start()
    {
		camAnimator= mainCam.gameObject.GetComponent<Animator>();
		skeleton.SetActive(false);
	 	personaEnemy.SetActive(false);
		personaPlayerAC= skeleton.GetComponent<Animator>();
		playerAC=player.GetComponent<Animator>();
		enemyAC=enemy.GetComponent<Animator>();
		hideSkillButtons();
		
		state = BattleState.START;
		StartCoroutine(SetupBattle());
    }
	void Update(){
		Debug.Log("alfonso damage: " + enemyUnit.damage);
	}

	IEnumerator SetupBattle()
	{
		//GameObject playerGO = Instantiate(playerPrefab, playerBattleStation);
		playerUnit = playerGO.GetComponent<Unit>();

		//GameObject enemyGO = Instantiate(enemyPrefab, enemyBattleStation);
		enemyUnit = enemyGO.GetComponent<Unit>();

		dialogText.text = enemyUnit.unitName + " busca problemas";

		//playerHUD.SetHUD(playerUnit);
		//enemyHUD.SetHUD(enemyUnit);

		yield return new WaitForSeconds(2f);

		state = BattleState.PLAYERTURN;
		PlayerTurn();
		buttonAttack.gameObject.SetActive(true);
		buttonHeal.gameObject.SetActive(true);
		dialogText.gameObject.SetActive(false);

	}

	IEnumerator PlayerAttack()
	{
		hideSkillButtons();
		//fade in skeleton
		//fadeInObject(skeleton);
		//check if finishes enemy
		skeleton.SetActive(true);
		bool isDead = enemyUnit.TakeDamage(playerUnit.damage);

		//set hp
		enemyHUD.SetHP(enemyUnit.currentHP);
		dialogText.gameObject.SetActive(true);
		dialogText.text = "Player: the attack deals " + playerUnit.damage + " points of damage!";

		//persona (skeleton) events
		skeletonCameraws.SetActive(true);
		personaPlayerAC.SetTrigger("attack");

		//quan acabi la animacio desapareix amb fade out
		yield return new WaitForSeconds(2f);
		skeletonCameraws.SetActive(false);
		skeleton.SetActive(false);
		dialogText.gameObject.SetActive(false);

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

	IEnumerator EnemyTurn()
	{
		
		hideSkillButtons();

		//decidir atack 1 focus 2 atack 3 super atack
		int attackChosen = Random.Range(0, 4);
		Debug.Log("random: " + attackChosen);

		while (attackChosen==1 && isFocused)
		{
		//torna a calcular si toca focus i ja esta focus
 		attackChosen = Random.Range(0, 4);
		Debug.Log("ha coincidit random i focused");

		}

		
		dialogText.gameObject.SetActive(true);
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
				enemyUnit.damage *= 2;
				dialogText.text = enemyUnit.unitName + " attacks with more energy!";

				yield return new WaitForSeconds(2f);

				personaEnemy.SetActive(true);
				enemyPersonaCam.SetActive(true);

				enemyAC.GetComponent<Animator>().SetTrigger("attack");
				yield return new WaitForSeconds(2f);
				isFocused=false;
				//reset damage
				enemyUnit.damage=enemyUnit.baseDamage;

			}
			else
			{
				dialogText.text = enemyUnit.unitName + " attacks!";
				yield return new WaitForSeconds(2f);

				personaEnemy.SetActive(true);
				enemyPersonaCam.SetActive(true);

				enemyAC.GetComponent<Animator>().SetTrigger("attack");
				yield return new WaitForSeconds(2f);
			}
			break;
			case 3:
			if(isFocused)
			{
				dialogText.text = enemyUnit.unitName + " charges with a focused epic mega super hit!";
				enemyUnit.damage *= 4;
				isFocused=false;
				//reiniciem damage
				enemyUnit.damage=enemyUnit.baseDamage;
			}
			else{
				dialogText.text = enemyUnit.unitName + " charges with a strong attack!";
				enemyUnit.damage *= 2;
			}
			break;
		}
		
		Debug.Log("pre switch 2 atac: " + attackChosen + " i boleana " + isFocused);
		
		switch(attackChosen){
			case 2:
		personaEnemy.GetComponent<Animator>().SetTrigger("attack");
			break;
			case 3:
			//super attack animacio	personaEnemy.GetComponent<Animator>().SetTrigger("attack");
			break;
		}
		
		yield return new WaitForSeconds(1f);
		bool isDead; 
		if(isFocused){
			isDead = playerUnit.TakeDamage(0);
		}
		else
		{
			isDead = playerUnit.TakeDamage(enemyUnit.damage);
			dialogText.text = "deals " + enemyUnit.damage + " points of damage!";

			yield return new WaitForSeconds(2f);
			dialogText.gameObject.SetActive(false);

			enemyPersonaCam.SetActive(false);
			personaEnemy.SetActive(false);
			playerHUD.SetHP(playerUnit.currentHP);

		}

		 //temporalDamageEnemy= enemyUnit.damage;
		 //enemyUnit.damage=0;

		

		yield return new WaitForSeconds(1f);

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
		dialogText.gameObject.SetActive(false);
		showSkillButtons();
	}

	void hideSkillButtons()
	{
		buttonAttack.gameObject.SetActive(false);
		buttonHeal.gameObject.SetActive(false);
	}

	void showSkillButtons()
	{
		buttonAttack.gameObject.SetActive(true);
		buttonHeal.gameObject.SetActive(true);
	}

	

	IEnumerator PlayerHeal()
	{
		hideSkillButtons();
		int amountHealed=5;
		playerUnit.Heal(amountHealed);
		playerHUD.SetHP(playerUnit.currentHP);
		dialogText.gameObject.SetActive(true);
		dialogText.text = "You recover " + amountHealed + "HP!";
		personaPlayerAC.SetTrigger("heal");

		yield return new WaitForSeconds(2f);
		dialogText.gameObject.SetActive(false);

		state = BattleState.ENEMYTURN;
		StartCoroutine(EnemyTurn());
	}

	public void OnAttackButton()
	{
		if (state != BattleState.PLAYERTURN)
			return;

		StartCoroutine(PlayerAttack());
	}

	public void OnHealButton()
	{
		if (state != BattleState.PLAYERTURN)
			return;

		StartCoroutine(PlayerHeal());
	}


	}

