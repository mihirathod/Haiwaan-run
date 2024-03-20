using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponSelection : MonoBehaviour
{
    [Header("Navigation Buttons")]
    [SerializeField] private Button previousButton;
    [SerializeField] private Button nextButton;

    [Header("Equip/Buy Buttons")]
    [SerializeField] private Button equipButton;
    [SerializeField] private Button buyButton;
    [SerializeField] private TextMeshProUGUI priceText;
    [SerializeField] private TextMeshProUGUI coinText;

    [Header("Weapon Names")]
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private string[] weaponNames;

    [Header("Weapon Attributes")]
    [SerializeField] private int[] weaponPrices;
    [SerializeField] private GameObject[] weapons;
    private int currentWeapon;


    private void Start()
    {
        currentWeapon = PlayerPrefs.GetInt("CurrentWeapon", 0);

        // Check if the first weapon is unlocked, if not, unlock it
        if (PlayerPrefs.GetInt("WeaponUnlocked_0", 0) == 0)
        {
            PlayerPrefs.SetInt("WeaponUnlocked_0", 1);
            PlayerPrefs.Save();
        }

        SelectWeapon(currentWeapon);
    }

    private void SelectWeapon(int index)
    {
        for (int i = 0; i < weapons.Length; i++)
            weapons[i].gameObject.SetActive(i == index);

        UpdateUI();
    }

    private void UpdateUI()
    {
        //If the current weapon is unlocked, show the equip button
        if (PlayerPrefs.GetInt("WeaponUnlocked_" + currentWeapon) == 1)
        {
            PlayerPrefs.SetInt("CurrentWeapon", currentWeapon);
            equipButton.gameObject.SetActive(true);
            buyButton.gameObject.SetActive(false);
        }
        //If not, show the buy button and set the price
        else
        {
            equipButton.gameObject.SetActive(false);
            buyButton.gameObject.SetActive(true);
            priceText.text = weaponPrices[currentWeapon].ToString();
        }
    }

    private void Update()
    {
        //Check if we have enough Coins
        if (buyButton.gameObject.activeInHierarchy)
            buyButton.interactable = (PlayerPrefs.GetInt("Coins", 0) >= weaponPrices[currentWeapon]);

        coinText.text = PlayerPrefs.GetInt("Coins").ToString();
        nameText.text = weaponNames[currentWeapon].ToString();
    }

    public void ChangeWeapon(int change)
    {
        currentWeapon += change;

        if (currentWeapon > weapons.Length - 1)
            currentWeapon = 0;
        else if (currentWeapon < 0)
            currentWeapon = weapons.Length - 1;
        SelectWeapon(currentWeapon);
    }

    public void BuyWeapon()
    {
        int coins = PlayerPrefs.GetInt("Coins", 0);
        coins -= weaponPrices[currentWeapon];
        PlayerPrefs.SetInt("Coins", coins);
        PlayerPrefs.SetInt("WeaponUnlocked_" + currentWeapon, 1);
        //data save here 

        PlayerPrefs.Save();

        UpdateUI();
    }
}