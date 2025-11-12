using System.Collections;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    FinalScore finalScore;

    [Header("Game Config")]
    public int Rounds = 4;

    public float TotalScore = 0;
    private float onRoundNumber = 0;

    [SerializeField]
    private bool startGame = false;

    private void Start()
    {
        GameEvents.Instance.OnGameReload();
    }

    private IEnumerator StartAfterShake()
    {
        // yield return new WaitForShakeToStop(0f);
        yield return new WaitForSeconds(0.5f);
        yield return new WaitForShakeStrongerThan(2);
        GameEvents.Instance.OnGameStart();
    }

    private IEnumerator ReloadAfterShake()
    {
        yield return new WaitForShakeStrongerThan(2);
        GameEvents.Instance.OnGameReload();
    }

    private void OnValidate()
    {
        if (startGame)
        {
            startGame = false;
            GameEvents.Instance.OnGameStart();
        }
    }

    public void InitializeGame()
    {
        TotalScore = 0;
        onRoundNumber = 0;
        StartCoroutine(StartAfterShake());
    }

    public void OnRoundEnd()
    {
        onRoundNumber++;
        if (onRoundNumber < Rounds)
        {
            GameEvents.Instance.OnNextRound();
        }
        else
        {
            GameEvents.Instance.OnGameEnd();
            StartCoroutine(ReloadAfterShake());
        }
    }

    public void SetFinalScore()
    {
        finalScore.SetFinalScore("$" + TotalScore.ToString("#.00"));
    }
}