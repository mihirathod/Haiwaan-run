using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class SceneLoader : MonoBehaviour
{
    public Image loadingBar; // Reference to your loading bar UI element
    public TextMeshProUGUI progressText;

    void Start()
    {
        // Start loading the main scene asynchronously in the background
        StartCoroutine(LoadMainScene());
    }

    IEnumerator LoadMainScene()
    {
        // Simulate a 0.5-second delay for the loading screen
        yield return new WaitForSeconds(0.5f);

        // Load the main scene asynchronously
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(1);

        // Don't allow scene to activate until it's fully loaded
        asyncLoad.allowSceneActivation = false;

        while (!asyncLoad.isDone)
        {
            // Update the loading bar based on the loading progress
            float progress = Mathf.Clamp01(asyncLoad.progress / 0.9f); // 0.9f because isDone becomes true at 0.9
            loadingBar.fillAmount = progress;
            progressText.text = Mathf.RoundToInt(progress * 100).ToString();

            // Check if the load operation is almost done
            if (asyncLoad.progress >= 0.9f)
            {
                // Automatically activate the loaded scene when progress reaches 90%
                asyncLoad.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
