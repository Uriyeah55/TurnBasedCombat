using System.Collections.Generic;
using UnityEngine;

public class ShuffleManager : MonoBehaviour
{
    public List<ShuffleSlot> shuffleSlots = new List<ShuffleSlot>(); // Lista de todos los slots de shuffle

    public void ClearAllShuffleSlots()
    {
        foreach (ShuffleSlot slot in shuffleSlots)
        {
            if (slot != null)
            {
                slot.ClearSlot(); // Llama a ClearSlot en cada slot
            }
        }
        Debug.Log("All shuffle slots have been cleared.");
    }
}
