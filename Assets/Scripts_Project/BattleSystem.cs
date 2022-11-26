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
		dialogText.text = "The attack deals " + playerUnit.damage + " points of damage!";

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
		dialogText.gameObject.SetActive(true);
		dialogText.text = enemyUnit.unitName + " attacks!";

		yield return new WaitForSeconds(2f);

	 	personaEnemy.SetActive(true);
		enemyPersonaCam.SetActive(true);
		enemyAC.GetComponent<Animator>().SetTrigger("attack");
		yield return new WaitForSeconds(2f);
		personaEnemy.GetComponent<Animator>().SetTrigger("attack");

		//Debug.Log("trigger enemic");
		yield return new WaitForSeconds(1f);

		dialogText.text = "deals " + enemyUnit.damage + " points of damage!";

		yield return new WaitForSeconds(2f);
		dialogText.gameObject.SetActive(false);

		enemyPersonaCam.SetActive(false);
	 	personaEnemy.SetActive(false);

		bool isDead = playerUnit.TakeDamage(enemyUnit.damage);


		playerHUD.SetHP(playerUnit.currentHP);

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

