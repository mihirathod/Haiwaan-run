using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvManager : MonoBehaviour
{
    [SerializeField] GameObject[] envirments;

    private void OnEnable()
    {
        Instantiate(envirments[Random.Range(0, envirments.Length)], transform);
    }
}
