// using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField]
    private GameObject[] dishes;
    [SerializeField]
    private MoveCircle moveShaker;
    [SerializeField]
    private Shake shakeShaker;
    [SerializeField]
    private DisplaySaltiness saltinessDisplay;
    [SerializeField]
    private ScaleByValue[] quadrants;
    [SerializeField]
    private TMP_Text nextOrderNameDisplay;
    [SerializeField]
    private FinalScore finalScore;

    [Header("Round Settings")]
    [SerializeField]
    private Vector3 EnterFrom = new Vector3(-10, 0, 0);
    [SerializeField]
    private Vector3 ExitTo = new Vector3(10, 0, 0);
    [SerializeField]
    private Vector3 DishMainLocation = Vector3.zero;
    public bool initializeRound = false;
    public bool startCountdown = false;

    private GameObject currentDish;


    private void OnValidate()
    {
        if (initializeRound)
        {
            initializeRound = false;
            InitializeRound();
        }

        if (startCountdown)
        {
            startCountdown = false;
            RoundEvents.Instance.OnCountdownStart();
        }
    }

    public void InitializeRound()
    {
        GameObject dishToSpawn = PickRandomDish();

        currentDish = Instantiate(dishToSpawn, EnterFrom, Quaternion.identity);

        currentDish.GetComponent<DishEnterExit>().MainLocation = DishMainLocation;
        currentDish.GetComponent<DishEnterExit>().EnterFrom = EnterFrom;
        currentDish.GetComponent<DishEnterExit>().ExitTo = ExitTo;

        currentDish.GetComponent<DishEnterExit>().Enter();

        nextOrderNameDisplay.text = currentDish.GetComponent<DishName>().Name;
        finalScore.SetNextDishName(currentDish.GetComponent<DishName>().Name);

        int saltiness = (int)Random.Range(1f, 3.75f);
        ColorByCorrectness.TargetValue = saltiness;
        saltinessDisplay.SetSaltiness(saltiness);

        // StartCoroutine(WaitForDish());

        StartCoroutine(StartAfterPause());
    }

    public void ResetRound()
    {
        foreach (ScaleByValue quadrant in quadrants)
        {
            quadrant.Reset();
        }

        if (currentDish)
        {
            currentDish.GetComponent<DishEnterExit>().Exit();
        }
    }

    private IEnumerator StartAfterPause()
    {
        yield return new WaitForSeconds(1f);
        // yield return new WaitForShakeStrongerThan(2f);
        RoundEvents.Instance.OnCountdownStart();
    }

    private IEnumerator NextRoundAfterPause()
    {
        yield return new WaitForSeconds(4f);
        // yield return new WaitForShakeStrongerThan(3f);
        RoundEvents.Instance.OnRoundEnd();
    }

    // private IEnumerator WaitForDish()
    // {
    //     while (currentDish.transform.position != DishMainLocation)
    //     {
    //         yield return null;
    //     }

    //     RoundEvents.Instance.OnCountdownStart();
    // }

    public void StartRound()
    {
        StartCoroutine(WaitForShakerToFinish());

        moveShaker.StartCircle();
    }

    public void RevealScore()
    {
        StartCoroutine(NextRoundAfterPause());
    }

    private IEnumerator WaitForShakerToFinish()
    {
        while (moveShaker.CircleProgress == null)
        {
            yield return null;
        }

        while (moveShaker.CircleProgress != null)
        {
            yield return null;
        }

        RoundEvents.Instance.OnRevealScore();
    }

    private GameObject PickRandomDish()
    {
        if (dishes.Length == 0)
        {
            throw new System.Exception("No dishes");
        }

        int randomIndex = Random.Range(0, dishes.Length - 1);

        return dishes[randomIndex];
    }
}
