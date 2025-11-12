using UnityEngine;

public class MoveCircle : MonoBehaviour
{
    public float Radius = 5f;
    public float Duration = 4f;
    public bool startCircle = false;
    public float? CircleProgress { get; private set; }
    private Vector3 startingPosition;
    private Vector3 circleCenter;
    private bool circling;
    private float? startedCirclingAt;


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
        if (circling) return;

        circling = true;
        startedCirclingAt = Time.time;
    }

    private void Awake()
    {
        startingPosition = transform.position;
        circleCenter = transform.position + Vector3.left * Radius;
    }

    private void Update()
    {
        if (!circling) return;
        if (startedCirclingAt == null) return;

        float progress = (Time.time - (float)startedCirclingAt) / Duration;

        if (progress > 1)
        {
            circling = false;
            startedCirclingAt = null;
            transform.position = startingPosition;
            CircleProgress = null;
            return;
        }

        float angle = Mathf.Lerp(0, -Mathf.PI * 2, progress);
        CircleProgress = progress;

        transform.position = circleCenter + Radius * new Vector3(
                Mathf.Cos(angle),
                0,
                Mathf.Sin(angle)
            );
    }
}
