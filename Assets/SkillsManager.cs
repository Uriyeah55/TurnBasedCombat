using System.Collections;
using UnityEngine;
using System.Collections.Generic;  // Required for List<>


public class SkillsManager : MonoBehaviour
{
    public List<Skill> CharacterSkills;  // List of skill ScriptableObjects
    public GameObject cardPrefab;  // Card prefab to instantiate
    public Transform cardParent;   // Where the cards will be placed

    void Start()
    {
        StartCoroutine(SpawnSkillCards());
    }

    IEnumerator SpawnSkillCards()
    {
        foreach (Skill skill in CharacterSkills)
        {
            GameObject newCard = Instantiate(cardPrefab, cardParent);
            // Here, you would set the card's data from the Skill ScriptableObject
            //newCard.GetComponent<SkillCardUI>().SetSkillData(skill);
            
            // Optional: Add some animation, like a delay between spawning cards
            yield return new WaitForSeconds(0.2f);
        }
    }
}
