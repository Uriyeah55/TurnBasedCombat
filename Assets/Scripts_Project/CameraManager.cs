using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Camera[] camerasList;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
public void enableSpecificCamera(int index)
{
    for(int i = 0; i < camerasList.Length; i++)
    {
        if (i == index)
        {
            camerasList[i].enabled = true;
            camerasList[i].gameObject.SetActive(true);  // Activa el GameObject si no lo estaba
            Debug.Log("Activando cámara " + i);
        }
        else
        {
            camerasList[i].enabled = false;
            camerasList[i].gameObject.SetActive(false); // Desactiva el GameObject si no lo estaba
            Debug.Log("Desactivando cámara " + i);
        }
    }
}

}
