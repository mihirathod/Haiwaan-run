using UnityEngine;
using UnityEngine.UI;

public class HapticFeedbackControll : MonoBehaviour
{
    #region Variable
    public Sprite soundOnImage;
    public Sprite soundOffImage;
    public Button button;
    public bool isOn = true;
    public static bool isHapticOn;
    #endregion

    #region Function
    private void Update()
    {
        if (PlayerPrefs.GetInt("HapticInt") == 0)
        {
            isOn = true;
            button.GetComponent<Image>().sprite = soundOnImage;
            isHapticOn = false;
        }
        else
        {
            isOn = false;
            button.GetComponent<Image>().sprite = soundOffImage;
            isHapticOn = true;
        }
    }

    public void ButtonClicked()
    {
        if (isOn)
        {
            PlayerPrefs.SetInt("HapticInt", 1);
        }
        else
        {
            Handheld.Vibrate();
            PlayerPrefs.SetInt("HapticInt", 0);
        }
    }
    #endregion
}
