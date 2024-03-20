using UnityEngine;

public class playerSelect : MonoBehaviour
{
    [SerializeField] private GameObject[] player;

    private void Start()
    {
        for (int i = 0; i < player.Length; i++)
        {
            player[i].SetActive(false);
        }

        int currentPlayer = PlayerPrefs.GetInt("CurrentPlayer", 0);

        // Check if the selected player is unlocked, if not, choose the first unlocked player
        if (PlayerPrefs.GetInt("PlayerUnlocked_" + currentPlayer, 0) == 0)
        {
            PlayerPrefs.SetInt("CurrentPlayer", currentPlayer);
            PlayerPrefs.Save();
        }

        ChoosePlayer(currentPlayer);
    }


    private void ChoosePlayer(int _index)
    {
        player[_index].SetActive(true);
    }
}
