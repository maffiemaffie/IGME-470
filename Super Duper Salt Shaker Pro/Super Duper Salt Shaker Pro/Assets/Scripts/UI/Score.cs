using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField]
    private TMP_Text saltedWellDisplay;
    [SerializeField]
    private TMP_Text evennessDisplay;
    [SerializeField]
    private TMP_Text totalTipsDisplay;
    [SerializeField]
    private GameObject[] quadrants;
    [SerializeField]
    private GameManager gameManager;
    [SerializeField]
    private FinalScore finalScore;

    public void CalculateScore()
    {
        float underSaltedBy = 0;
        float overSaltedBy = 0;
        float averageSaltiness = 0;

        foreach (GameObject quadrant in quadrants)
        {
            float actualSaltiness = quadrant.transform.localScale.x * 3;
            float targetSaltiness = ColorByCorrectness.TargetValue;
            underSaltedBy += Mathf.Max(0, targetSaltiness - actualSaltiness);
            overSaltedBy += Mathf.Max(0, actualSaltiness - targetSaltiness);
            averageSaltiness += actualSaltiness;
        }

        underSaltedBy /= quadrants.Length * 3;
        overSaltedBy /= quadrants.Length * 3;
        averageSaltiness /= quadrants.Length * 3;

        float unEvenness = 0;

        foreach (GameObject quadrant in quadrants)
        {
            float actualSaltiness = quadrant.transform.localScale.x;
            unEvenness += Mathf.Abs(actualSaltiness - averageSaltiness);
        }

        unEvenness /= quadrants.Length;

        float score = 0;

        float saltedWell = 1.5f;
        saltedWell -= underSaltedBy * 3f;
        saltedWell -= overSaltedBy * 6f;
        saltedWell = Mathf.Max(0, saltedWell);

        float evenness = 1f;
        evenness -= unEvenness * 4.5f;
        evenness = Mathf.Max(0, evenness);

        score += saltedWell;
        score += evenness;

        saltedWellDisplay.text ="$" + saltedWell.ToString("#0.00");
        evennessDisplay.text ="$" + evenness.ToString("#0.00");
        totalTipsDisplay.text = "$" + score.ToString("#0.00");
        gameManager.TotalScore += score;

        finalScore.SetNextScore("$" + score.ToString("#0.00"));
    }
}
