using System.Collections;
using System.Drawing.Text;
using TMPro;
using UnityEngine;

public class Countdown : MonoBehaviour
{
    [SerializeField]
    private TMP_Text[] countdownNumbers;
    private const float countdownEachNumberDuration = 1f;

    private void Awake()
    {
        foreach (TMP_Text number in countdownNumbers)
        {
            number.enabled = false;
        }
    }

    public void StartCountdown()
    {
        StartCoroutine(CountdownCoroutine());
    }

    private IEnumerator CountdownCoroutine()
    {
        foreach (TMP_Text number in countdownNumbers)
        {
            number.enabled = true;
            yield return new WaitForSeconds(1);
            number.enabled = false;
        }

        RoundEvents.Instance.OnRoundStart();
    }
}
