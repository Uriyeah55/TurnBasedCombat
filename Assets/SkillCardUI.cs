using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class SkillCardUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TMP_Text skillNameText, skillDescription;  // References for name and description text
    public Sprite skillIcon;  // Reference to the Image UI for the skill icon (optional)

    private Vector3 originalPosition;  // Store the original position of the card
    private Vector3 hoverOffset = new Vector3(0, 20, 0);  // The amount to move the card up

    // Flag to ensure we don't repeatedly modify the position
    private bool isHovered = false;

    // Initialize the original position of the card
    void Start()
    {
        // Store the card's original position
        originalPosition = transform.localPosition;
    }

    public void SetSkillData(Skill skill)
    {
        if (skill == null)
        {
            Debug.LogError("Skill passed to SetSkillData is null.");
            return;
        }

        Debug.Log("Setting skill data for: " + skill.skillName);

        // Dynamically assign text components based on hierarchy
        skillNameText = transform.GetChild(0).GetComponent<TMP_Text>();
        skillDescription = transform.GetChild(1).GetComponent<TMP_Text>();

        // Check for null references
        if (skillNameText == null || skillDescription == null)
        {
            Debug.LogError("Text components not assigned or found in the prefab hierarchy!");
            return;
        }

        // Set the skill name and description in the UI
        skillNameText.text = skill.skillName;
        skillDescription.text = skill.skillDescription;  // Ensure your Skill class has this field
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isHovered)
        {
            isHovered = true;
            // Move the card up by the hover offset
            transform.localPosition = originalPosition + hoverOffset;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (isHovered)
        {
            isHovered = false;
            // Reset the card's position back to its original position
            transform.localPosition = originalPosition;
        }
    }
}
