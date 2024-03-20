using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeRotator : MonoBehaviour
{
    public bool isReverse;
    public float speed = 100;

    // Update is called once per frame
    void Update()
    {
        if (isReverse)
        {
            transform.Rotate(Vector3.forward * Time.deltaTime * speed);
        }
        else
        {
            transform.Rotate(Vector3.back * Time.deltaTime * speed);
        }
    }
}
