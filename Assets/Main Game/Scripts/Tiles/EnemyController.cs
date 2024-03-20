using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public bool IsHit;

    private void Start()
    {
        IsHit = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Destroyer")
        {
            Destroy(gameObject);
        }
    }
}
