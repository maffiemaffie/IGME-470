using UnityEngine;

public class WaitForShakeStrongerThan : CustomYieldInstruction
{
    float currentIntensity;
    float intensityThreshold;

    public override bool keepWaiting
    {
        get
        {
            return currentIntensity < intensityThreshold;
        }
    }

    public WaitForShakeStrongerThan(float intensityThreshold)
    {
        this.intensityThreshold = intensityThreshold;
        DataStreamIn.Instance.NewDataAvailable.AddListener(input => currentIntensity = input);
    }
}
