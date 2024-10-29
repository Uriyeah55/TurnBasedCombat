using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class SkillCardUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public TMP_Text skillNameText, skillDescription;
    public Sprite skillIcon;
    public Image image;

    private Vector3 originalPosition;
    private Vector3 hoverOffset = new Vector3(0, 20, 0);
    private RectTransform cardRectTransform;  // El RectTransform de la carta, no del contenedor
    public GameObject manager;

    public Skill currentSkill;

    private bool isHovered = false;

    void Start()
    {
        manager=GameObject.Find("MANAGER");
        hideInfoTexts();
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

        if (skillNameText == null || skillDescription == null)
        {
            Debug.LogError("Text components not assigned or found in the prefab hierarchy!");
            return;
        }

        skillNameText.text = skill.skillName;
        skillDescription.text = skill.skillDescription;
        currentSkill=skill;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isHovered)
        {
            isHovered = true;
            image.rectTransform.localPosition = new Vector3(0, 20, 0);
            showInfoTexts();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (isHovered)
        {
            isHovered = false;
            image.rectTransform.localPosition = Vector3.zero;
            hideInfoTexts();
        }
    }

public void OnPointerClick(PointerEventData eventData)
{
    Debug.Log("Skill card clicked!");

    // Establecer la habilidad seleccionada
    manager.GetComponent<BattleSystem>().playerSelectedSkill = currentSkill;

    // Llamar a PlayerAttack utilizando StartCoroutine
    StartCoroutine(manager.GetComponent<BattleSystem>().PlayerAttack());

    Debug.Log("current skill: " + currentSkill);
}


    public void showInfoTexts()
    {
        skillNameText.enabled = true;
        skillDescription.enabled = true;
    }

    public void hideInfoTexts()
    {
        skillNameText.enabled = false;
        skillDescription.enabled = false;
    }
}
