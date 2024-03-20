using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerSelection : MonoBehaviour
{
    [Header("Navigation Buttons")]
    [SerializeField] private Button previousButton;
    [SerializeField] private Button nextButton;

    [Header("Play/Buy Buttons")]
    [SerializeField] private Button play;
    [SerializeField] private Button buy;
    [SerializeField] private TextMeshProUGUI priceText;
    [SerializeField] private TextMeshProUGUI coinText;

    [Header("Player Names")]
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private string[] playerNames;

    [Header("Player Attributes")]
    [SerializeField] private int[] playerPrices;
    [SerializeField] private GameObject[] players;
    private int currentPlayer;


    private void Start()
    {
        currentPlayer = PlayerPrefs.GetInt("CurrentPlayer", 0);

        // Check if the first player is unlocked, if not, unlock it
        if (PlayerPrefs.GetInt("PlayerUnlocked_0", 0) == 0)
        {
            PlayerPrefs.SetInt("PlayerUnlocked_0", 1);
            PlayerPrefs.Save();
        }

        // Ensure the current player is unlocked, if not, find the first unlocked player
        if (PlayerPrefs.GetInt("PlayerUnlocked_" + currentPlayer, 0) == 0)
        {
            currentPlayer = FindFirstUnlockedPlayer();
            PlayerPrefs.SetInt("CurrentPlayer", currentPlayer);
            PlayerPrefs.Save();
        }

        SelectPlayer(currentPlayer);
    }

    private int FindFirstUnlockedPlayer()
    {
        for (int i = 0; i < players.Length; i++)
        {
            if (PlayerPrefs.GetInt("PlayerUnlocked_" + i, 0) == 1)
            {
                return i;
            }
        }

        // Return 0 if no unlocked player is found (default case)
        return 0;
    }

    private void SelectPlayer(int _index)
    {
        for (int i = 0; i < players.Length; i++)
            players[i].gameObject.SetActive(i == _index);

        UpdateUI();
    }

    private void UpdateUI()
    {
        //If the current player is unlocked, show the play button
        if (PlayerPrefs.GetInt("PlayerUnlocked_" + currentPlayer) == 1)
        {
            PlayerPrefs.SetInt("CurrentPlayer", currentPlayer);
            play.gameObject.SetActive(true);
            buy.gameObject.SetActive(false);
        }
        //If not, show the buy button and set the price
        else
        {
            play.gameObject.SetActive(false);
            buy.gameObject.SetActive(true);
            priceText.text = playerPrices[currentPlayer].ToString();
        }
    }

    private void Update()
    {
        //Check if we have enough Coins
        if (buy.gameObject.activeInHierarchy)
            buy.interactable = (PlayerPrefs.GetInt("Coins", 0) >= playerPrices[currentPlayer]);

        coinText.text = PlayerPrefs.GetInt("Coins").ToString();
        nameText.text = playerNames[currentPlayer].ToString();
    }

    public void ChangePlayer(int _change)
    {
        currentPlayer += _change;

        if (currentPlayer > players.Length - 1)
            currentPlayer = 0;
        else if (currentPlayer < 0)
            currentPlayer = players.Length - 1;

        SelectPlayer(currentPlayer);
    }

    public void BuyPlayer()
    {
        int Coins = PlayerPrefs.GetInt("Coins", 0);
        Coins -= playerPrices[currentPlayer];
        PlayerPrefs.SetInt("Coins", Coins);
        PlayerPrefs.SetInt("PlayerUnlocked_" + currentPlayer, 1);
        //save data here

        PlayerPrefs.Save();

        UpdateUI();
    }

    public void SetLastPlayer()
    {
        currentPlayer = PlayerPrefs.GetInt("CurrentPlayer");
        SelectPlayer(PlayerPrefs.GetInt("CurrentPlayer"));
    }

}
