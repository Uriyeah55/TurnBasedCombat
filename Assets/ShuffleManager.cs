using System.Collections.Generic;
using UnityEngine;

public class ShuffleManager : MonoBehaviour
{
    public List<ShuffleSlot> shuffleSlots = new List<ShuffleSlot>(); // Lista de todos los slots de shuffle
    public GameObject[] shuffleBack;
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

    public void showShuffleBacks(){
            foreach (GameObject backSlot in shuffleBack)
            {
                backSlot.SetActive(true);
                //backSlot.gameObject.SetActive(true);
            }
    }
}
