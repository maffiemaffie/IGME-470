using UnityEngine;
using UnityEngine.Events;

// singleton reference: https://gamedev.stackexchange.com/questions/116009/in-unity-how-do-i-correctly-implement-the-singleton-pattern

public class RoundEvents : MonoBehaviour
{
    private static RoundEvents instance;

    public static RoundEvents Instance { get { return instance; } }
    public UnityEvent CountdownStart = new UnityEvent();
    public UnityEvent RoundStart = new UnityEvent();
    public UnityEvent RoundEnd = new UnityEvent();
    public UnityEvent RevealScore = new UnityEvent();


    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public void OnCountdownStart()
    {
        CountdownStart.Invoke();
    }

    public void OnRoundStart()
    {
        RoundStart.Invoke();
    }

    public void OnRoundEnd()
    {
        RoundEnd.Invoke();
    }

    public void OnRevealScore()
    {
        RevealScore.Invoke();
    }
}
