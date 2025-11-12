using System.Collections;
using UnityEngine;

public class DishEnterExit : MonoBehaviour
{
    public Vector3 MainLocation = Vector3.zero;
    public Vector3 EnterFrom;
    public Vector3 ExitTo;
    [SerializeField]
    private bool DontDestroy = false;
    public bool enter;
    public bool exit;
    private Quaternion startingRotation;

    void OnValidate()
    {
        if (enter)
        {
            enter = false;
            Enter();
        }

        if (exit)
        {
            exit = false;
            Exit();
        }
    }

    private void Awake()
    {
        startingRotation = transform.rotation;
    }

    public void Enter()
    {
        StartCoroutine(EnterCoroutine());
    }

    public void Exit()
    {
        StartCoroutine(ExitCoroutine());
    }

    private IEnumerator EnterCoroutine()
    {
        transform.position = EnterFrom;
        for (float progress = 0; progress < 1; progress += 0.0025f)
        {
            float easedProgress = Mathf.SmoothStep(0, 1, progress);

            transform.rotation = startingRotation;
            transform.Rotate(0, 180 - easedProgress * 180, 0);

            transform.position = Vector3.Lerp(EnterFrom, MainLocation, easedProgress);
            yield return null;
        }
        transform.position = MainLocation;
    }

    private IEnumerator ExitCoroutine()
    {
        transform.position = ExitTo;
        for (float progress = 0; progress < 1; progress += 0.002f)
        {
            float easedProgress = Mathf.SmoothStep(0, 1, progress);

            transform.rotation = startingRotation;
            transform.Rotate(0, easedProgress * -180, 0);

            transform.position = Vector3.Lerp(MainLocation, ExitTo, easedProgress);
            yield return null;
        }
        transform.position = ExitTo;

        if (!DontDestroy)
        {
            Destroy(gameObject);
        }
    }
}
