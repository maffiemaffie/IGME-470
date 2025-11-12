using System.Collections;
using UnityEngine;

public class CameraToTitle : MonoBehaviour
{
    [SerializeField]
    private Camera[] cameras;
    [SerializeField]
    private Vector3 titleLocation;
    [SerializeField]
    private Vector3 titleRotation;

    [SerializeField]
    private bool toTitle;

    private void OnValidate()
    {
        if (toTitle)
        {
            toTitle = false;
            ToTitle();
        }
    }

    public void ToTitle()
    {
        StartCoroutine(MoveRotateFromTo(cameras[0].transform.position, cameras[0].transform.rotation.eulerAngles, titleLocation, titleRotation));
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
