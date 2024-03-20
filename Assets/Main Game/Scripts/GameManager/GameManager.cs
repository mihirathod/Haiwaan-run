using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region Var
    public static GameManager Instance;

    public Button TransformButton;
    //Game Objects
    public GameObject gameOverScreen;
    public GameObject[] gamePlayScreen;
    public GameObject transformBarFullEffect;
    public GameObject newHSText;
    public GameObject PauseButton;
    public GameObject PauseScreen;
    public GameObject[] tutorialPages;
    public GameObject warningPopup;

    //float
    public float score;
    public float transformPoint;
    public float targetTransformPoint;
    public float TransformDuration;

    //Int
    public int coins;
    public int playCount;
    public int transformCount;

    //Bool
    public bool gameOver;
    public bool isGameStarted;
    public bool isGamePaused;
    public bool isTutorialPlay;
    public bool isLevelShow;


    //Texts
    [Header ("Gameplay Text")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI levelHeaderText;

    [Header ("Game Pause Text")]
    public TextMeshProUGUI gamePauseScoreText;
    public TextMeshProUGUI gamePauseHighScoreText;
    public TextMeshProUGUI gamePauseCoinText;

    [Header ("Game Over Text")]
    public TextMeshProUGUI gameOverScoreText;
    public TextMeshProUGUI gameOverHighScoreText;
    public TextMeshProUGUI gameOverCoinText;

    //Image
    public Image TransformFilBar;

    public GameObject ReviveScreen;

    //Ref
    public ReviveBarController ReviveBarController;

    //Power ups
    public PowerIndicatorBarController armorBar;
    public PowerIndicatorBarController magnetBar;
    public PowerIndicatorBarController coinBoosterBar;
    public PowerIndicatorBarController scoreBoosterBar;


    [Header("normal Ad")]
    public static int gameOverCount;
    #endregion

    #region Unity Functions
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        levelHeaderText.gameObject.SetActive(false);
        PlayGame();
    }

    private void Update()
    {
        CheckInternetConnection();
        if (PlayerPrefs.GetInt("PlayCount") == 0)
        {

        }
        else
        {
            if (score >= 20 && score <= 22 && !isLevelShow)
            {
                isLevelShow = true;
                levelHeaderText.gameObject.SetActive(true);
                levelHeaderText.text = "Level 1";
                Invoke("DisableLevelText", 2.5f);
            }
        }

        if (score >= 500 && score <= 502 && !isLevelShow)
        {
            isLevelShow = true;
            levelHeaderText.gameObject.SetActive(true);
            levelHeaderText.text = "Level 2";
            Invoke("DisableLevelText", 2.5f);
        }
        if (score >= 1000 && score <= 1002 && !isLevelShow)
        {
            isLevelShow = true;
            levelHeaderText.gameObject.SetActive(true);
            levelHeaderText.text = "Level 3";
            Invoke("DisableLevelText", 2.5f);
        }
    }

    void DisableLevelText()
    {
        isLevelShow = false;
        levelHeaderText.gameObject.SetActive(false);
    }

    void Start()
    {
        transformBarFullEffect.SetActive(false);
        playCount = PlayerPrefs.GetInt("PlayCount");
        transformCount = PlayerPrefs.GetInt("transformCount");
        isGameStarted = true;
        TransformButton.interactable = false;
        score = 0;
        Time.timeScale = 1;
        PlayerPrefs.GetInt("Coins");
    }
    #endregion

    #region Custom Function

    public void PlayGame()
    {
        if (PlayerPrefs.GetInt("PlayCount") == 0 && PlayerPrefs.GetInt("HighScore") <= 100 && SceneManager.GetActiveScene().buildIndex == 2)
        {
            PauseButton.SetActive(false);
            isTutorialPlay = true;
        }
    }

    public void GameOver()
    {
        gameOverCount++;
        if (gameOverCount >= 5)
        {
            gameOverCount = 1;
        }

        ReviveScreen.SetActive(false);
        gameOverCoinText.text = coins.ToString();
        gameOverScoreText.text = Mathf.RoundToInt(score).ToString();
        if (score > PlayerPrefs.GetInt("HighScore", 0))
        {
            newHSText.SetActive(true);
            PlayerPrefs.SetInt("HighScore",(int)score);
    
        }

       
        gameOverHighScoreText.text = PlayerPrefs.GetInt("HighScore").ToString();
        gameOverScreen.SetActive(true);
    }

    public void DisableGameplayUI()
    {
        for (int i = 0; i < gamePlayScreen.Length; i++)
        {
            gamePlayScreen[i].SetActive(false);
        }
    }

    public void EnableGameplayUI()
    {
        for (int i = 0; i < gamePlayScreen.Length; i++)
        {
            gamePlayScreen[i].SetActive(true);
        }
    }

    public void OpenRevive()
    {
        PlayerManager.Instance.DisableAllPowerup();
        ReviveBarController.cooldown.fillAmount = 1;
        ReviveScreen.SetActive(true);
    }

    public void TransformPlayer()
    {
        if (PlayerPrefs.GetInt("PlayCount") == 0)
        {
            PauseButton.SetActive(true);
        }
        PlayerController.Instance.GetTransform();
    }

    public void DisplayCoins()
    {
        if (PlayerManager.Instance.isCoinBoosterActivate)
        {
            coins += 2;
        }
        else
        {
            coins++;
        }
        PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + 1);

        coinText.text = coins.ToString();
    }

    public void UpdateTransformBar()
    {

        TransformFilBar.fillAmount = transformPoint / targetTransformPoint;
        if (TransformFilBar.fillAmount >= 1)
        {
            transformBarFullEffect.SetActive(true);
            Invoke("PlayTransformTutorialOnDelay", 1);
            TransformButton.interactable = true;
        }
    }

    void PlayTransformTutorialOnDelay()
    {
        if (PlayerPrefs.GetInt("transformCount") == 0)
        {
            Time.timeScale = 0;
            tutorialPages[6].SetActive(true);
        }
    }


    #endregion

    #region UI Function
    public void GoToMenu()
    {
        Time.timeScale = 1;
        Invoke("GoToMenuOnDelay", 0.1f);
    }

    public void PauseGame()
    {
        isGamePaused = true;
        PauseScreen.SetActive(true);
        gamePauseCoinText.text = coins.ToString();
        gamePauseHighScoreText.text = PlayerPrefs.GetInt("HighScore").ToString();
        gamePauseScoreText.text = Mathf.RoundToInt(score).ToString();
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        isGamePaused = false;
        Time.timeScale = 1;
        PauseScreen.SetActive(false);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void GoToMenuOnDelay()
    {
        isGamePaused = false;
        SceneManager.LoadScene(1);
    }
    #endregion

    #region Vibration

    public void PlayDefaultVibration()
    {
        if (HapticFeedbackControll.isHapticOn == false)
        {
            Vibration.Init();
            Vibration.Vibrate();
        }
    }

    public void PlaySmallVibration()
    {
        if (HapticFeedbackControll.isHapticOn == false)
        {
            Vibration.Init();
            Vibration.VibratePop();
        }
    }
    #endregion

    #region NoInternet
    void CheckInternetConnection()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            PauseGame();
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
    #endregion

}
