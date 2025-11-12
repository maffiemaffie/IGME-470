using System.Collections;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [SerializeField]
    private Camera[] cameras;
    [SerializeField]
    private MoveLight spotlight;

    [SerializeField]
    private Vector3 view1Location;
    [SerializeField]
    private Vector3 view1Rotation;

    [SerializeField]
    private Vector3 view2Location;
    [SerializeField]
    private Vector3 view2Rotation;

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
        spotlight.GoToLocation1();
        StartCoroutine(MoveRotateFromTo(cameras[0].transform.position, cameras[0].transform.rotation.eulerAngles, view1Location, view1Rotation));
    }

    public void GoToLocation2()
    {
        spotlight.GoToLocation2();
        StartCoroutine(MoveRotateFromTo(cameras[0].transform.position, cameras[0].transform.rotation.eulerAngles, view2Location, view2Rotation));
    }

    IEnumerator MoveRotateFromTo(Vector3 fromLocation, Vector3 fromRotation, Vector3 toLocation, Vector3 toRotation)
    {
        Vector3 vectorToNewLocation = toLocation - fromLocation;
        Vector3 vectorToNewRotation = toRotation - fromRotation;

        foreach (Camera camera in cameras)
        {
            camera.transform.position = fromLocation;
            camera.transform.rotation = Quaternion.Euler(fromRotation);
        }


        for (float progress = 0; progress < 1; progress += 0.005f)
        {
            foreach (Camera camera in cameras)
            {
                camera.transform.position = fromLocation + progress * vectorToNewLocation;
                camera.transform.rotation = Quaternion.Euler(fromRotation + progress * vectorToNewRotation);
            }

            yield return null;
        }

        foreach (Camera camera in cameras)
        {
            camera.transform.position = toLocation;
            camera.transform.rotation = Quaternion.Euler(toRotation);
        }
    }
}
