using UnityEngine;
using UnityEngine.Events;

// singleton reference: https://gamedev.stackexchange.com/questions/116009/in-unity-how-do-i-correctly-implement-the-singleton-pattern

public class GameEvents : MonoBehaviour
{
    private static GameEvents instance;

    public static GameEvents Instance { get { return instance; } }
    public UnityEvent GameReload = new UnityEvent();
    public UnityEvent GameStart = new UnityEvent();
    public UnityEvent NextRound = new UnityEvent();
    public UnityEvent GameEnd = new UnityEvent();


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

    public void OnGameReload()
    {
        GameReload.Invoke();
    }

    public void OnGameStart()
    {
        GameStart.Invoke();
    }

    public void OnNextRound()
    {
        NextRound.Invoke();
    }

    public void OnGameEnd()
    {
        GameEnd.Invoke();
    }
}
