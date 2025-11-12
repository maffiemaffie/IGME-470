using UnityEngine;

public class HandleUserInput : MonoBehaviour
{
    public float lowAngle;
    public float mediumAngle;
    public float highAngle;
    public void OnUserInput(float input)
    {
        if (input < 0.5) return;
        if (input > 0.5)
        {
            GetComponent<Shake>().ShakeMaxAngle = lowAngle;
        }
        if (input > 1.5)
        {
            GetComponent<Shake>().ShakeMaxAngle = mediumAngle;
        }
        if (input > 2.5)
        {
            GetComponent<Shake>().ShakeMaxAngle = highAngle;
        }
        GetComponent<Shake>().AddShake();
    }
}
