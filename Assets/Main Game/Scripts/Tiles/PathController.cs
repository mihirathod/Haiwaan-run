using UnityEngine;

public class PathController : MonoBehaviour
{
    public GameObject[] coinPositions;
    public GameObject[] coins;
    public GameObject[] powerPosition;
    public GameObject[] powerups;
    public int randomPowerPositionIndex;
    public int powerupIndex;

    private void OnEnable()
    {
        if (!GameManager.Instance.isTutorialPlay)
        {
            SpawnPowerUps();
        }
        SpawnCoins();
    }


    void SpawnCoins()
    {
        for (int i = 0; i < coinPositions.Length; i++)
        {
            Instantiate(coins[0], coinPositions[i].transform);
        }
    }

    void SpawnPowerUps()
    {
        randomPowerPositionIndex = Random.Range(0, powerPosition.Length);
        powerupIndex = Random.Range(0, 8);

        switch (powerupIndex)
        {
            case 0:
                Instantiate(powerups[0], powerPosition[randomPowerPositionIndex].transform);
                break;
            case 1:
                Instantiate(powerups[1], powerPosition[randomPowerPositionIndex].transform);
                break;
            case 2:
                Instantiate(powerups[2], powerPosition[randomPowerPositionIndex].transform);
                break;
            case 3:
                Instantiate(powerups[3], powerPosition[randomPowerPositionIndex].transform);
                break;
        }
    }
}
