    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;
    using UnityEngine.EventSystems;
    using TMPro;


    public class SkillCardUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        public TMP_Text skillNameText, skillDescription;
    // public Sprite skillIcon;
        public Image image;

        private Vector3 originalPosition;
        private Vector3 hoverOffset = new Vector3(0, 20, 0);
        private RectTransform cardRectTransform;  
        public GameObject manager, shuffleMenu;

        public Skill currentSkill;

        // Lista de los slots de shuffle
        public List<Image> shuffleSlots = new List<Image>();

        private bool isHovered = false;

        void Start()
        {
            manager = GameObject.Find("MANAGER");
            shuffleMenu = GameObject.Find("ShuffleMenu");
            hideInfoTexts();

            // Obtener el RectTransform de la propia carta
            cardRectTransform = GetComponent<RectTransform>();
            originalPosition = cardRectTransform.anchoredPosition;

            // Obtener todos los componentes Image en los hijos de ShuffleMenu
            shuffleSlots.AddRange(shuffleMenu.GetComponentsInChildren<Image>());

            if (shuffleSlots.Count == 0)
            {
                Debug.LogError("No shuffle slots found in ShuffleMenu.");
            }
        }

        public void SetSkillData(Skill skill)
        {
            if (skill == null)
            {
                Debug.LogError("Skill passed to SetSkillData is null.");
                return;
            }

            Debug.Log("Setting skill data for: " + skill.skillName);
            skillNameText.text = skill.skillName;
            skillDescription.text = skill.skillDescription;
            currentSkill = skill;
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
       
            // Verificar el valor de isShuffling
            if (GlobalVars.isShuffling)
            {
                // Lógica actual de shuffle
                ShuffleSlot emptySlot = GetEmptyShuffleSlot();
                if (emptySlot != null)
                {
                    emptySlot.SetSkill(currentSkill);
                    emptySlot.gameObject.GetComponent<Image>().sprite = image.sprite;

                    Debug.Log("Skill placed in shuffle slot: " + currentSkill.skillName);
                }
                else
                {
                    Debug.Log("All shuffle slots are full.");
                }
            }
            else
            {
                // Mostrar información de la skill
                Debug.Log($"Skill: {currentSkill.skillName}, Description: {currentSkill.skillDescription}");
            }
    }


    private ShuffleSlot GetEmptyShuffleSlot()
    {
        // Recorre todos los hijos de ShuffleMenu buscando un slot vacío
        foreach (ShuffleSlot slot in shuffleMenu.GetComponentsInChildren<ShuffleSlot>())
        {
            Debug.Log($"Checking slot: {slot.gameObject.name} - Assigned skill: {slot.assignedSkill}");
            
            if (slot.assignedSkill == null)  // Si el slot no tiene una habilidad asignada
            {
                Debug.Log($"{slot.gameObject.name} is empty.");
                return slot;
            }
        }
        
        Debug.Log("No empty slots found.");
        return null;  // No hay slots vacíos
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
