using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ColorOptions
{
    public Color[] initializedColors = new Color[]
    {
        Color.red,
        Color.green,
        Color.yellow,
        Color.cyan,
        Color.magenta
    };
}

public class RandomColor : MonoBehaviour
{
    [SerializeField] private ColorOptions randomColors;

    void Start()
    {
        ApplyRandomColor();
    }

    private void ApplyRandomColor()
    {
        if (randomColors.initializedColors.Length == 0)
        {
            Debug.LogWarning("No random colors set for " + name);
            return;
        }

        Color randomColor = randomColors.initializedColors[Random.Range(0, randomColors.initializedColors.Length)];

        foreach (Transform child in transform)
        {
            Image image = child.GetComponent<Image>();
            image.color = randomColor;
        }
    }
}
