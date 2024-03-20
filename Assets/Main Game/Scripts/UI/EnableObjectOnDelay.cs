using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableObjectOnDelay : MonoBehaviour
{
    public GameObject targetObject;
    public float delayTime;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("EnableObjectsOnDelay", delayTime);
    }

    void EnableObjectsOnDelay()
    {
        targetObject.SetActive(true);
    }
}
