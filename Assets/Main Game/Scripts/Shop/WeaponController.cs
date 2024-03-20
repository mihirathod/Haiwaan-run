using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private GameObject[] weapon;

    private void Update()
    {

        for (int i = 0; i < weapon.Length; i++)
        {
            weapon[i].SetActive(false);
        }

        ChoosePlayer(PlayerPrefs.GetInt("CurrentWeapon"));
    }

    private void ChoosePlayer(int _index)
    {
        weapon[_index].SetActive(true);
    }
}