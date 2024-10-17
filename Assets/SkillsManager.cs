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
            if (skill == null) // Check for null skill
            {
                Debug.LogWarning("Found a null skill in CharacterSkills.");
                continue; // Skip this iteration if skill is null
            }

            Debug.Log("Spawning skill: " + skill.skillName); // Log the skill name

            // Instantiate the skill card prefab
            GameObject newCard = Instantiate(cardPrefab, cardParent);
            var skillCardUI = newCard.GetComponent<SkillCardUI>();

            if (skillCardUI != null)
            {
                skillCardUI.SetSkillData(skill);  // Set the skill data on the card
            }
            else
            {
                Debug.LogError($"SkillCardUI component not found on the prefab: {cardPrefab.name}");
            }

            // Optionally yield here if you're spawning many cards and want to control timing
            yield return null; // Wait for the next frame (optional)
        }
    }
}
