using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class ScaleByValue : MonoBehaviour
{
    public float MaxScale = 0.125f;
    public float MinScale = 0.075f;

    public void Reset()
    {
        transform.localScale = Vector3.zero;
    }

    public void UpdateValue(float value)
    {
        // transform.localScale += Vector3.one * Mathf.Lerp(MinScale, MaxScale, value);
        StopAllCoroutines();
        StartCoroutine(ScaleUpTo(transform.localScale.x + Mathf.Lerp(MinScale, MaxScale, value)));
        // StartCoroutine(ScaleUpTo(transform.localScale.x + 0.1f));
    }

    private IEnumerator ScaleUpTo(float scale)
    {
        while (transform.localScale.x < scale)
        {
            transform.localScale += Vector3.one * 0.02f;
            yield return null;
        }

        transform.localScale = Vector3.one * scale;
    }

    private void Awake()
    {
        transform.localScale = Vector3.zero;
    }
}
