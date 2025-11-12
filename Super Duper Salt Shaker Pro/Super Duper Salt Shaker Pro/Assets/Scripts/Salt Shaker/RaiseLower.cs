using System.Collections;
using UnityEngine;

public class RaiseLower : MonoBehaviour
{
    [SerializeField]
    private Vector3 raisedLocation;
    [SerializeField]
    private Vector3 raisedRotation;

    [SerializeField]
    private Vector3 loweredLocation;
    [SerializeField]
    private Vector3 loweredRotation;

    [SerializeField]
    private bool raise;
    [SerializeField]
    private bool lower;

    private void OnValidate()
    {
        if (raise)
        {
            raise = false;
            Raise();
        }
        if (lower)
        {
            lower = false;
            Lower();
        }
    }

    public void Raise()
    {
        StartCoroutine(MoveRotateFromTo(loweredLocation, loweredRotation, raisedLocation, raisedRotation));
    }

    public void Lower()
    {
        StartCoroutine(MoveRotateFromTo(raisedLocation, raisedRotation, loweredLocation, loweredRotation));
    }

    IEnumerator MoveRotateFromTo(Vector3 fromLocation, Vector3 fromRotation, Vector3 toLocation, Vector3 toRotation)
    {
        Vector3 vectorToNewLocation = toLocation - fromLocation;
        Vector3 vectorToNewRotation = toRotation - fromRotation;

        transform.position = fromLocation;
        transform.rotation = Quaternion.Euler(fromRotation);


        for (float progress = 0; progress < 1; progress += 0.005f)
        {
            transform.position = fromLocation + progress * vectorToNewLocation;
            transform.rotation = Quaternion.Euler(fromRotation + progress * vectorToNewRotation);

            yield return null;
        }

        transform.position = toLocation;
        transform.rotation = Quaternion.Euler(toRotation);
    }
}
