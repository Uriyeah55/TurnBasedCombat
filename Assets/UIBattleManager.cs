using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBattleManager : MonoBehaviour
{
    public Button btnSkills, btnShuffle, btnBack, btnGenre, btnClear;
    public GameObject skillCardPanel, canvasCombat, camBehindPlayer, shuffleMenu;
    public GameObject[] shuffleBack;
    public GameObject shuffleBackGlobal;

    public AnimationManager animManager;

    public List<ShuffleSlot> shuffleSlots = new List<ShuffleSlot>(); // Lista de todos los slots de shuffle
    public List<Skill> usedSkills = new List<Skill>(); // Nueva lista para habilidades usadas
    Animator playerAC;

    // Start is called before the first frame update
    void Start()
    {
        // Obtener el Animator del jugador desde el script AnimationManager
        animManager = GetComponent<AnimationManager>();
        playerAC = animManager.playerAnimCtrler;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void showSkills()
    {
        if (GlobalVars.isShuffling)
        {
            shuffleBackGlobal.SetActive(true);
            shuffleMenu.SetActive(true);
            btnClear.gameObject.SetActive(true);

            foreach (GameObject backSlot in shuffleBack)
            {
                backSlot.SetActive(true);
            }
        }
        else
        {
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
        camBehindPlayer.GetComponent<Animator>().SetInteger("currentState", 0);
    }

    public void showShuffleMenu()
    {
        btnSkills.gameObject.SetActive(false);
        playerAC.SetInteger("currentStance", 2);
        skillCardPanel.SetActive(true);
        canvasCombat.GetComponent<HorizontalSpacingLerp>().StartLerpingSpacing(-40f, -100f, .2f);
        btnBack.gameObject.SetActive(true);
        camBehindPlayer.GetComponent<Animator>().SetInteger("currentState", 0);
    }

    public void showSkillButtons()
    {
        camBehindPlayer.GetComponent<Animator>().SetInteger("currentState", 1);

        btnSkills.gameObject.SetActive(true);
        btnShuffle.gameObject.SetActive(true);
        btnGenre.gameObject.SetActive(true);
        skillCardPanel.SetActive(false);
        btnClear.gameObject.SetActive(false);
    }

    public void ClearSelectedSkills()
    {
        // Limpia todos los slots de shuffle
        foreach (ShuffleSlot slot in shuffleMenu.GetComponentsInChildren<ShuffleSlot>())
        {
            slot.ClearSlot();
        }

        // Limpia la lista de habilidades usadas
        usedSkills.Clear();

        Debug.Log("All shuffle slots and used skills list have been cleared.");
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

    public void hideSkillButtons()
    {
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

    public void showShuffleBacks()
    {
        foreach (GameObject backSlot in shuffleBack)
        {
            backSlot.SetActive(true);
        }
    }

    static public void activateShuffleMode()
    {
        GlobalVars.isShuffling = true;
    }

    static public void disableShuffleMode()
    {
        GlobalVars.isShuffling = false;
    }

    public void AddSkillToUsed(Skill skill)
    {
        if (!usedSkills.Contains(skill))
        {
            usedSkills.Add(skill);
            Debug.Log($"Skill {skill.skillName} added to used skills.");
        }
        else
        {
            Debug.Log($"Skill {skill.skillName} is already used.");
        }
    }

    public void ClearUsedSkills()
    {
        usedSkills.Clear();
        Debug.Log("Used skills list has been cleared.");
    }
}
