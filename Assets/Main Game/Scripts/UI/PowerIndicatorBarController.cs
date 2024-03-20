using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerIndicatorBarController : MonoBehaviour
{
    public Image cooldown;
    public float waitTime;

    public void ResetIndicator(int BarTime)
    {
        waitTime = BarTime;
        cooldown.fillAmount = 1;
    }

    private void Update()
    {
        cooldown.fillAmount -= 1.0f / waitTime * Time.deltaTime;
    }
}
