using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class SkillCardUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TMP_Text skillNameText, skillDescription;
    public Sprite skillIcon;

    private Vector3 originalPosition;
    private Vector3 hoverOffset = new Vector3(0, 20, 0);
    private RectTransform cardRectTransform;  // El RectTransform de la carta, no del contenedor

    private bool isHovered = false;

    void Start()
    {
        // Obtener el RectTransform de la propia carta (no el contenedor)
        cardRectTransform = GetComponent<RectTransform>();

        // Almacenar la posición original de la carta en relación a su contenedor
        originalPosition = cardRectTransform.anchoredPosition;
    }

    public void SetSkillData(Skill skill)
    {
        if (skill == null)
        {
            Debug.LogError("Skill passed to SetSkillData is null.");
            return;
        }

        Debug.Log("Setting skill data for: " + skill.skillName);

        skillNameText = transform.GetChild(0).GetComponent<TMP_Text>();
        skillDescription = transform.GetChild(1).GetComponent<TMP_Text>();

        if (skillNameText == null || skillDescription == null)
        {
            Debug.LogError("Text components not assigned or found in the prefab hierarchy!");
            return;
        }

        skillNameText.text = skill.skillName;
        skillDescription.text = skill.skillDescription;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isHovered)
        {
            isHovered = true;
            // Mover solo la carta dentro del contenedor usando anchoredPosition
            cardRectTransform.anchoredPosition = originalPosition + hoverOffset;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (isHovered)
        {
            isHovered = false;
            // Restaurar la posición de la carta dentro del contenedor
            cardRectTransform.anchoredPosition = originalPosition;
        }
    }
}
