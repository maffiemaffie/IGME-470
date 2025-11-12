using System.Collections;
using UnityEngine;

public class MoveLight : MonoBehaviour
{
    [SerializeField]
    private Vector3 location1;

    [SerializeField]
    private Vector3 location2;

    [SerializeField]
    private bool goToLocation1;
    [SerializeField]
    private bool goToLocation2;

    private void OnValidate()
    {
        if (goToLocation1)
        {
            goToLocation1 = false;
            GoToLocation1();
        }
        if (goToLocation2)
        {
            goToLocation2 = false;
            GoToLocation2();
        }
    }

    public void GoToLocation1()
    {
        StartCoroutine(MoveFromTo(transform.position, location1));
    }

    public void GoToLocation2()
    {
        StartCoroutine(MoveFromTo(transform.position, location2));
    }

    IEnumerator MoveFromTo(Vector3 fromLocation, Vector3 toLocation)
    {
        Vector3 vectorToNewLocation = toLocation - fromLocation;

        transform.position = fromLocation;

        for (float progress = 0; progress < 1; progress += 0.005f)
        {
            transform.position = fromLocation + progress * vectorToNewLocation;
            yield return null;
        }

        transform.position = toLocation;
    }
}
