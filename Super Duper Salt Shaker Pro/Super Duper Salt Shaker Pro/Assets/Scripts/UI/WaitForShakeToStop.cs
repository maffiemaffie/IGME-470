using UnityEngine;

public class WaitForShakeToStop : CustomYieldInstruction
{
    float currentIntensity;

    float deadZone;

    public override bool keepWaiting
    {
        get
        {
            return currentIntensity > deadZone;
        }
    }

    public WaitForShakeToStop(float deadZone)
    {
        this.deadZone = deadZone;
        DataStreamIn.Instance.NewDataAvailable.AddListener(input => currentIntensity = input);
    }
}
