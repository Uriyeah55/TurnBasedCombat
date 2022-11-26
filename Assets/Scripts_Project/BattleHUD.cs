using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BattleHUD : MonoBehaviour
{

	//public Text nameText;
	//public Text levelText;
	public Text hpText;

	public void SetHUD(Unit unit)
	{
	//	nameText.text = unit.unitName;
		//levelText.text = "Lvl " + unit.unitLevel;
        hpText.text=unit.maxHP.ToString();
		//hpSlider.maxValue = unit.maxHP;
		//hpSlider.value = unit.currentHP;
	}

	public void SetHP(int hp)
	{
		hpText.text = hp.ToString();;
	}

}