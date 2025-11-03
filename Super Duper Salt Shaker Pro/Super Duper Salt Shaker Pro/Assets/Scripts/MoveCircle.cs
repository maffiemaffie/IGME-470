using System;
using System.Collections;
using UnityEngine;

public class MoveCircle : MonoBehaviour
{
    public float radius = 5f;
    public bool startCircle = false;
    private Vector3 startingPosition;
    private Vector3 circleCenter;

    void OnValidate()
    {
        if (startCircle)
        {
            startCircle = false;
            StartCircle();
        }
    }

    public void StartCircle()
    {
        StartCoroutine(MoveCircleCoroutine());
    }

    IEnumerator MoveCircleCoroutine()
    {
        startingPosition = transform.position;
        circleCenter = transform.position + Vector3.left * radius;

        for (float angle = 0; angle <= Mathf.PI * 2; angle += 0.005f)
        {
            transform.position = circleCenter + radius * new Vector3(
                Mathf.Cos(angle),
                0,
                Mathf.Sin(angle)
            );

            yield return null;
        }

        transform.position = startingPosition;
    }
}
