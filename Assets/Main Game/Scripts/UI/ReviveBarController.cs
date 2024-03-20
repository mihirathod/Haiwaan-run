using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReviveBarController : MonoBehaviour
{
    public Image cooldown;
    public float waitTime;
    public bool isReviveBarOn;

    private void Update()
    {
        if (isReviveBarOn)
        {
            cooldown.fillAmount -= 1.0f / waitTime * Time.deltaTime;
            CallGameOver();
        }
    }

    public void CallGameOver()
    {
        if (cooldown.fillAmount <= 0 && isReviveBarOn)
        {
            CloseRevivePanel();
        }
    }

    public void CloseRevivePanel()
    {
        isReviveBarOn = false;
        GameManager.Instance.GameOver();
    }

    public void CallRevivePlayerByDimond()
    {
        RevivePlayer();
    }

    public void CallRevivePlayerFree()
    {
        RevivePlayer();
    }

    public void RevivePlayer()
    {
        GameManager.Instance.EnableGameplayUI();
        isReviveBarOn = false;
        GameManager.Instance.ReviveScreen.SetActive(false);
        PlayerController.Instance.RevivePlayer();
    }
}
