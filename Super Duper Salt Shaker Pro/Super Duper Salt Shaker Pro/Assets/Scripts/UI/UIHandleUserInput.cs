using UnityEngine;

public class UIHandleUserInput : MonoBehaviour
{
    public MoveCircle Shaker;
    public int IAmQuadrantNumber = 0;
    public int NumberOfQuadrants = 4;

    public void OnNewDataInput(float input)
    {
        if (Shaker.CircleProgress == null)
        {
            return;
        }

        float myMinimumProgress = IAmQuadrantNumber / (float)NumberOfQuadrants;
        float myMaxmiumProgress = (IAmQuadrantNumber + 1) / (float)NumberOfQuadrants;

        if (Shaker.CircleProgress < myMinimumProgress || Shaker.CircleProgress > myMaxmiumProgress)
        {
            return;
        }

        if (input / 3f < transform.localScale.x) return;

        GetComponent<ScaleByValue>().UpdateValue(input / 3f);
        GetComponent<ColorByCorrectness>().UpdateValue();
    }
}
