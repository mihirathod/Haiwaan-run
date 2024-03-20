using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TutorialStageController : MonoBehaviour
{
    public static TutorialStageController Instance;

    public bool is2ndStageCompleted;
    public static int tutorialStageIndex;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && GameManager.Instance.isTutorialPlay)
        {
            UpdatAutoTutorial();
        }
    }

    void UpdatAutoTutorial()
    {
        tutorialStageIndex++;
        switch (tutorialStageIndex)
        {
            case 1:
                Time.timeScale = 0;
                GameManager.Instance.tutorialPages[0].SetActive(true);
                break;
            case 2:
                Time.timeScale = 0;
                GameManager.Instance.tutorialPages[1].SetActive(true);
                break;
            case 3:
                Time.timeScale = 0;
                GameManager.Instance.tutorialPages[2].SetActive(true);
                break;
            case 4:
                Time.timeScale = 0;
                GameManager.Instance.tutorialPages[3].SetActive(true);
                break;
            case 5:
                Time.timeScale = 0;
                GameManager.Instance.tutorialPages[4].SetActive(true);
                break;
            case 6:
                GameManager.Instance.tutorialPages[5].SetActive(true);
                //Invoke("ClearTutorialPage", 1);
                GameManager.Instance.isTutorialPlay = false;
                PlayerPrefs.SetInt("PlayCount", 1);
                break;
        }
    }


    public void ClearTutorialPage()
    {         
        Time.timeScale = 1;
        for (int i = 0; i < GameManager.Instance.tutorialPages.Length; i++)
        {
            GameManager.Instance.tutorialPages[i].SetActive(false);
        }
    }
}
