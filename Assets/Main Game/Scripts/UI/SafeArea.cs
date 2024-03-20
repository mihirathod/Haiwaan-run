using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeArea : MonoBehaviour
{
    void Start()
    {
        // Get the safe area
        Rect safeArea = Screen.safeArea;

        Debug.Log("Safe Area: " +
                  "X: " + safeArea.x +
                  ", Y: " + safeArea.y +
                  ", Width: " + safeArea.width +
                  ", Height: " + safeArea.height);

        // Adjust the Canvas based on the safe area
        RectTransform canvasRectTransform = GetComponent<RectTransform>();
        canvasRectTransform.anchoredPosition = new Vector2(safeArea.x, safeArea.y);
        canvasRectTransform.sizeDelta = new Vector2(safeArea.width, safeArea.height);
    }
}
