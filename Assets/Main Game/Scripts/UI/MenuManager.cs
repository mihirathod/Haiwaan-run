using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuManager : MonoBehaviour
{

    [Header("Warning Popup")]
    public GameObject warningPopup;

    [Header("Text")]
    public TextMeshProUGUI GemText;
    public TextMeshProUGUI HighScoreText;
    public TextMeshProUGUI CoinText;

    [Header("Ads")]
    public int PlayCount;


    private void Awake()
    {
        PlayCount = PlayerPrefs.GetInt("PlayCount");
        PlayerPrefs.SetInt("PlayCount", PlayerPrefs.GetInt("PlayCount") + 1);
        if (PlayerPrefs.GetInt("PlayCount") >= 5)
        {
            PlayerPrefs.SetInt("PlayCount", 1);
        }
    }

    private void Update()
    {
        CheckInternetConnection();
        CoinText.text = PlayerPrefs.GetInt("Coins").ToString();
        HighScoreText.text = PlayerPrefs.GetInt("HighScore").ToString();
    }

    void CheckInternetConnection()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            // No internet connection, display warning popup
            if (warningPopup != null)
            {
                warningPopup.SetActive(true); // Show the warning popup
            }
        }
        else
        {
            // Internet connection available, continue the game
            // You might want to hide the warning popup here if it's already shown
            if (warningPopup != null)
            {
                warningPopup.SetActive(false); // Hide the warning popup
            }
        }
    }

    public void PlayGame()
    {
        Invoke("PlayGameOnDelay", 0.2f);
    }

    void PlayGameOnDelay()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("Currentmap") + 2);
    }
}
