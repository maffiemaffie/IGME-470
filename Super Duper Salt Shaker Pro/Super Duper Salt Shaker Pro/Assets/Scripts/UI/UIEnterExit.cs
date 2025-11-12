using System.Collections;
using UnityEngine;

public class UIEnterExit : MonoBehaviour
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
        GetComponent<RectTransform>().anchoredPosition = from;

        Vector3 movementVector = to - from;

        for (float progress = 0; progress < 1; progress += 0.005f)
        {
            GetComponent<RectTransform>().anchoredPosition = from + Mathf.SmoothStep(0, 1, progress) * movementVector;
            yield return null;
        }

        GetComponent<RectTransform>().anchoredPosition = to;
    }
}
