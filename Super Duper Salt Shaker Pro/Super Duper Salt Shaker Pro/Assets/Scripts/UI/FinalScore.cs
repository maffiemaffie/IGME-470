using System;
using TMPro;
using UnityEngine;

public class FinalScore : MonoBehaviour
{
    [SerializeField]
    private TMP_Text[] dishNameDisplays;
    [SerializeField]
    private TMP_Text[] dishScoreDisplays;
    [SerializeField]
    private TMP_Text finalScoreDisplay;
    private int roundNumber = -1;

    public void Clear()
    {
        foreach (TMP_Text display in dishNameDisplays)
        {
            display.text = "";
        }

        foreach (TMP_Text display in dishScoreDisplays)
        {
            display.text = "";
        }

        finalScoreDisplay.text = "";

        roundNumber = -1;
    }

    public void NextRound()
    {
        roundNumber++;
    }

    public void SetNextDishName(string name)
    {
        dishNameDisplays[roundNumber].text = name;
    }

    public void SetNextScore(string score)
    {
        dishScoreDisplays[roundNumber].text = score;
    }

    public void SetFinalScore(string score)
    {
        finalScoreDisplay.text = score;
    }
}
