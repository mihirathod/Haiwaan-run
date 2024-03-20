using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathManager : MonoBehaviour
{
    [SerializeField] GameObject[] paths;
    [SerializeField] GameObject tutorialPaths;
    public static int dummyTileIndex;
    
    private void OnEnable()
    {
        if (GameManager.Instance.isTutorialPlay)
        {
            if (dummyTileIndex == 0)
            {
                dummyTileIndex++;
                Instantiate(tutorialPaths, transform);
            }
            else
            {
                Instantiate(paths[1], transform);
            }
        }
        else
        {
            if (GameManager.Instance.score >= 500 && GameManager.Instance.score <= 999)
            {
                Debug.Log("2nd");
                Instantiate(paths[Random.Range(2, 5)], transform);
            }

            if (GameManager.Instance.score >= 1000)
            {
                Debug.Log("3rd");
                Instantiate(paths[Random.Range(5, paths.Length)], transform);
            }

            if(GameManager.Instance.score <= 500)
            {
                Debug.Log("1st");
                Instantiate(paths[Random.Range(0, 3)], transform);
            }
        }
    }

    private void OnDisable()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }
}
