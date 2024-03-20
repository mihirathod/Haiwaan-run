using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class MapController : MonoBehaviour
{
    [Header("Navigation Buttons")]
    [SerializeField] private Button previousButton;
    [SerializeField] private Button nextButton;

    [Header("Equip/Buy Buttons")]
    [SerializeField] private Button equipButton;
    [SerializeField] private Button buyButton;
    [SerializeField] private TextMeshProUGUI priceText;
    [SerializeField] private TextMeshProUGUI coinText;

    [Header("map Names")]
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private string[] mapNames;

    [Header("map Attributes")]
    [SerializeField] private int[] mapPrices;
    [SerializeField] private GameObject[] maps;
    private int currentmap;

    private void Start()
    {
        currentmap = PlayerPrefs.GetInt("Currentmap", 0);

        // Check if the first map is unlocked, if not, unlock it
        if (PlayerPrefs.GetInt("MapUnlocked_0", 0) == 0)
        {
            PlayerPrefs.SetInt("MapUnlocked_0", 1);
            PlayerPrefs.Save();
        }

        Selectmap(currentmap);
    }

    private void Selectmap(int index)
    {
        for (int i = 0; i < maps.Length; i++)
            maps[i].gameObject.SetActive(i == index);

        UpdateUI();
    }

    private void UpdateUI()
    {
        //If the current map is unlocked, show the equip button
        if (PlayerPrefs.GetInt("MapUnlocked_" + currentmap) == 1)
        {
            PlayerPrefs.SetInt("Currentmap", currentmap);
            equipButton.gameObject.SetActive(true);
            buyButton.gameObject.SetActive(false);
        }
        //If not, show the buy button and set the price
        else
        {
            equipButton.gameObject.SetActive(false);
            buyButton.gameObject.SetActive(true);
            priceText.text = mapPrices[currentmap].ToString();
        }
    }

    private void Update()
    {
        //Check if we have enough Coins
        if (buyButton.gameObject.activeInHierarchy)
            buyButton.interactable = (PlayerPrefs.GetInt("Coins", 0) >= mapPrices[currentmap]);

        coinText.text = PlayerPrefs.GetInt("Coins").ToString();
        nameText.text = mapNames[currentmap].ToString();
    }

    public void Changemap(int change)
    {
        currentmap += change;

        if (currentmap > maps.Length - 1)
            currentmap = 0;
        else if (currentmap < 0)
            currentmap = maps.Length - 1;
        Selectmap(currentmap);
    }

    public void Buymap()
    {
        int coins = PlayerPrefs.GetInt("Coins", 0);
        coins -= mapPrices[currentmap];
        PlayerPrefs.SetInt("Coins", coins);
        PlayerPrefs.SetInt("MapUnlocked_" + currentmap, 1);
        //data save here 

        PlayerPrefs.Save();

        UpdateUI();
    }
}
