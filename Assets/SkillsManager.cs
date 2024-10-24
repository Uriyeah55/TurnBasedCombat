using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillsManager : MonoBehaviour
{
    public List<Skill> CharacterSkills;  // List of skills
    public GameObject cardPrefab;         // Prefab for the skill card
    public Transform cardParent;          // Parent transform for the instantiated cards

    void Start()
    {
        // Ensure that CharacterSkills is populated
        if (CharacterSkills == null || CharacterSkills.Count == 0)
        {
            Debug.LogError("CharacterSkills is not populated.");
            return;
        }

        // Spawn the skill cards
        StartCoroutine(SpawnSkillCards());
    }

private IEnumerator SpawnSkillCards()
{
    foreach (Skill skill in CharacterSkills)
    {
        if (skill == null)
        {
            Debug.LogWarning("Found a null skill in CharacterSkills.");
            continue;
        }

        Debug.Log("Spawning skill: " + skill.skillName);

        // Instanciar el prefab
        GameObject newCard = Instantiate(cardPrefab, cardParent);

        // Obtener el SkillCardUI desde los hijos
        var skillCardUI = newCard.GetComponentInChildren<SkillCardUI>();

        if (skillCardUI != null)
        {
            skillCardUI.SetSkillData(skill);
        }
        else
        {
            Debug.LogError($"SkillCardUI component not found on any child of the prefab: {cardPrefab.name}");
        }

        yield return null; // Esperar al siguiente frame
    }
}

}
