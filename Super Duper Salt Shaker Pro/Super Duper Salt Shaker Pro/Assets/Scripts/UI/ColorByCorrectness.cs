using System;
using UnityEngine;
using UnityEngine.UI;

public class ColorByCorrectness : MonoBehaviour
{
    public Color CorrectColor = Color.green;
    public Color IncorrectColor = Color.red;
    public float MaxValue = 2f;
    public float MinValue = 0f;

    public static int TargetValue = 0;
    public void UpdateValue()
    {
        float currentValue = transform.localScale.x * 3;
        float offTargetBy = Mathf.Abs(currentValue - TargetValue) / (MaxValue - MinValue);
        Color color = Color.Lerp(CorrectColor, IncorrectColor, offTargetBy);
        GetComponent<Image>().color = color;
    }
}
