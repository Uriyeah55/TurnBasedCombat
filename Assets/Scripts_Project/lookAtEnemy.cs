using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookAtEnemy : MonoBehaviour
{
    public GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.transform.LookAt (target.gameObject.transform.position);

    }

}
