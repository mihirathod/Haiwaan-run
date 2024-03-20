using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;
    public PlayerController player;
    public GameObject magneticFeild;
    public GameObject armorSphere;
    public float powerupDuration;
    public bool isArmorActivate;
    public bool isMagnetActivate;
    public bool isScoreBoosterActivate;
    public bool isCoinBoosterActivate;
    public bool isColideWithWater;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        magneticFeild.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Coin")
        {
            AudioPlayer.Instance.PlayCoinSound();
            player.PlayCoinEffect();
            GameManager.Instance.DisplayCoins();
            Destroy(other.gameObject);
        }
        else if (other.transform.tag == "Armor")
        {
            Destroy(other.gameObject);
            AudioPlayer.Instance.PlayPowerCollectSound();
            StartCoroutine(ActivateArmorCo());
        }
        else if (other.transform.tag == "Magnet")
        {
            Destroy(other.gameObject);
            AudioPlayer.Instance.PlayPowerCollectSound();
            GameManager.Instance.magnetBar.ResetIndicator(10);
            GameManager.Instance.magnetBar.gameObject.SetActive(true);
            CancelInvoke("DisableMagnet");
            ActivateMagnet();
            Invoke("DisableMagnet", powerupDuration);
        }
        else if (other.transform.tag == "ScoreBooster")
        {
            Destroy(other.gameObject);
            AudioPlayer.Instance.PlayPowerCollectSound();
            GameManager.Instance.scoreBoosterBar.ResetIndicator(10);
            GameManager.Instance.scoreBoosterBar.gameObject.SetActive(true);
            CancelInvoke("DisableScoreBooster");
            ActivateScoreBooster();
            Invoke("DisableScoreBooster", powerupDuration);
        }
        else if (other.transform.tag == "CoinBooster")
        {
            Destroy(other.gameObject);
            AudioPlayer.Instance.PlayPowerCollectSound();
            GameManager.Instance.coinBoosterBar.ResetIndicator(10);
            GameManager.Instance.coinBoosterBar.gameObject.SetActive(true);
            CancelInvoke("DisableCoinBooster");
            ActivateCoinBooster();
            Invoke("DisableCoinBooster", powerupDuration);
        }
    }

    #region Powerups
    //Armor

    IEnumerator ActivateArmorCo()
    {
        yield return new WaitUntil(() => PlayerController.isPlayerTransform == false);

        GameManager.Instance.armorBar.ResetIndicator(10);
        GameManager.Instance.armorBar.gameObject.SetActive(true);
        CancelInvoke("DisableArmor");
        ActivateArmor();
        Invoke("DisableArmor", powerupDuration);
    }

    public void ActivateArmor()
    {
        isArmorActivate = true;
        armorSphere.SetActive(true);
    }

    public void DisableArmor()
    {
        CancelInvoke("DisableArmor");
        AudioPlayer.Instance.PlayTransformSound();
        player.PlayTransformEffect();
        GameManager.Instance.armorBar.gameObject.SetActive(false);
        player.StartShortArmor();
        isArmorActivate = false;
        armorSphere.SetActive(false);
    }

    //Magnet
    public void ActivateMagnet()
    {
        magneticFeild.SetActive(true);
    }

    public void DisableMagnet()
    {
        GameManager.Instance.magnetBar.gameObject.SetActive(false);
        magneticFeild.SetActive(false);
    }

    //Coin Booster
    public void ActivateCoinBooster()
    {
        isCoinBoosterActivate = true;
    }

    public void DisableCoinBooster()
    {
        GameManager.Instance.coinBoosterBar.gameObject.SetActive(false);
        isCoinBoosterActivate = false;
    }

    //Score Booster
    public void ActivateScoreBooster()
    {
        isScoreBoosterActivate = true;
    }

    public void DisableScoreBooster()
    {
        GameManager.Instance.scoreBoosterBar.gameObject.SetActive(false);
        isScoreBoosterActivate = false;
    }

    public void DisableAllPowerup()
    {
        DisableCoinBooster();
        DisableMagnet();
        DisableScoreBooster();
    }
    #endregion
}
