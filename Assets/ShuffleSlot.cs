using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShuffleSlot : MonoBehaviour
{
    public Image slotImage;             // Imagen del slot
   // public TMP_Text skillNameText;      // Texto del nombre de la habilidad
    //public TMP_Text skillDescriptionText; // Texto de la descripción de la habilidad
    public Skill assignedSkill;         // La habilidad asignada al slot

    // Método para asignar una habilidad al slot
    public void SetSkill(Skill skill)
    {
        if (skill != null)
        {
            assignedSkill = skill;
            //slotImage.sprite = skill.image;      // Asigna el icono de la habilidad
            //skillNameText.text = skill.skillName;    // Asigna el nombre de la habilidad
           // skillDescriptionText.text = skill.skillDescription; // Asigna la descripción
            slotImage.enabled = true;  // Habilita la imagen si no lo estaba
        }
    }

    // Método para limpiar el slot
public void ClearSlot()
{
    assignedSkill = null;
    slotImage.sprite = null;
   // skillNameText.text = "";
    //skillDescriptionText.text = "";
    slotImage.enabled = false;

    Debug.Log($"{gameObject.name} slot cleared.");
}


}
