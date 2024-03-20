using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    #region Var
    private List<GameObject> activeTiles;
    public GameObject[] tilePrefabs;

    public float tileLength;
    public int numberOfTiles;
    public int totalNumOfTiles;

    public float zSpawn;

    private Transform playerTransform;

    private int previousIndex;
    #endregion

    #region Unity Functions
    void Start()
    {
        activeTiles = new List<GameObject>();
        for (int i = 0; i < numberOfTiles; i++)
        {
            if(i==0)
                SpawnTile();
            else
                SpawnTile(Random.Range(0, totalNumOfTiles));
        }

        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

    }
    void Update()
    {
        if(playerTransform.position.z - 160 >= zSpawn - (numberOfTiles * tileLength))
        {
            int index = Random.Range(0, totalNumOfTiles);
            while(index == previousIndex)
                index = Random.Range(0, totalNumOfTiles);

            DeleteTile();
            SpawnTile(index);
        }
            
    }
    #endregion

    #region Custom Function
    public void SpawnTile(int index = 0)
    {
        GameObject tile = tilePrefabs[index];
        if (tile.activeInHierarchy)
            tile = tilePrefabs[index + 7];

        if(tile.activeInHierarchy)
            tile = tilePrefabs[index + 14];

        tile.transform.position = Vector3.forward * zSpawn;
        tile.transform.rotation = Quaternion.identity;
        tile.SetActive(true);

        activeTiles.Add(tile);
        zSpawn += tileLength;
        previousIndex = index;
    }

    private void DeleteTile()
    {
        activeTiles[0].SetActive(false);
        activeTiles.RemoveAt(0);
    }
    #endregion
}
