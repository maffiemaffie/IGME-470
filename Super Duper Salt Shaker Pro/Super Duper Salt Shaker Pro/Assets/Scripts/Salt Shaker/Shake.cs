using System;
using System.Collections;
using UnityEngine;

public class Shake : MonoBehaviour
{
    public float ShakeMaxAngle = 0.5f;
    public float ShakePeriod = 0.5f;
    private bool isShaking = false;
    private Quaternion startingRotation;
    public bool addShake = false;

    void OnValidate()
    {
        if (addShake)
        {
            addShake = false;
            AddShake();
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startingRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (isShaking)
        {
            float progress = Mathf.PingPong(Time.time, ShakePeriod) / ShakePeriod;

            float currentAngle = Mathf.Lerp(-ShakeMaxAngle, ShakeMaxAngle, progress);

            transform.rotation = startingRotation;
            transform.Rotate(new Vector3(
                currentAngle,
                0,
                0
            ), Space.Self);
        }
    }

    public void AddShake()
    {
        StopAllCoroutines();
        StartCoroutine(AddShakeCoroutine());
    }

    IEnumerator AddShakeCoroutine()
    {
        isShaking = true;
        yield return new WaitForSeconds(1f);
        isShaking = false;

        transform.rotation = startingRotation;
    }
}
