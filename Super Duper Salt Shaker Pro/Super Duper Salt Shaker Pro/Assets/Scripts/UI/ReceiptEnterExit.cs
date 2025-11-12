using System.Collections;
using UnityEngine;

public class ReceiptInOut : MonoBehaviour
{
    [SerializeField]
    private Vector3 onScreenLocation;
    [SerializeField]
    private Vector3 offScreenLocation;
    [SerializeField]
    private bool enter;
    [SerializeField]
    private bool exit;

    private void OnValidate()
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

    public void Enter()
    {
        StartCoroutine(MoveFromTo(offScreenLocation, onScreenLocation));
    }

    public void Exit()
    {
        StartCoroutine(MoveFromTo(onScreenLocation, offScreenLocation));
    }

    IEnumerator MoveFromTo(Vector3 from, Vector3 to)
    {
        transform.position = from;

        Vector3 movementVector = to - from;

        for (float progress = 0; progress < 1; progress += 0.005f)
        {
            transform.position = from + Mathf.SmoothStep(0, 1, progress) * movementVector;
            yield return null;
        }

        transform.position = to;
    }
}
