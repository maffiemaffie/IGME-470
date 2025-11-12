using TMPro;
using UnityEngine;

public class ShrinkOverTime : MonoBehaviour
{
    private float startingFontSize;
    void Awake()
    {
        startingFontSize = GetComponent<TMP_Text>().fontSize;
    }
    void Update()
    {
        if (GetComponent<TMP_Text>().enabled)
            GetComponent<TMP_Text>().fontSize -= 50 * Time.deltaTime;
        else GetComponent<TMP_Text>().fontSize = startingFontSize;
    }
}
