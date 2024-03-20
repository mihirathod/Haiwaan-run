using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject[] playerPrefabe;

    // Start is called before the first frame update
    void Awake()
    {
        Instantiate(playerPrefabe[PlayerPrefs.GetInt("CurrentPlayer")], transform);
    }
}
