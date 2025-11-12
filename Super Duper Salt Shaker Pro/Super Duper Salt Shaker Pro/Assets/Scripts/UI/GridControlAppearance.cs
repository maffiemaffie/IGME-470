using NUnit.Framework;
using UnityEngine;

public class GridControlAppearance : MonoBehaviour
{
    public MoveCircle Shaker;
    public int IAmQuadrantNumber = 0;
    public int NumberOfQuadrants = 4;
    private bool showing = false;

    // Update is called once per frame
    void Update()
    {
        if (Shaker.CircleProgress == null)
        {
            if (showing) GetComponent<ImageFadeInOut>().FadeOut();
            return;
        }

        float myMinimumProgressToShow = IAmQuadrantNumber / (float)NumberOfQuadrants;
        float myMaxmiumProgressToShow = (IAmQuadrantNumber + 1) / (float)NumberOfQuadrants;

        bool readyToShow = !showing && Shaker.CircleProgress > myMinimumProgressToShow && Shaker.CircleProgress < myMaxmiumProgressToShow;
        if (readyToShow)
        {
            showing = true;
            GetComponent<ImageFadeInOut>().FadeIn();
            return;
        }

        bool readyToHide = showing && (Shaker.CircleProgress < myMinimumProgressToShow || Shaker.CircleProgress > myMaxmiumProgressToShow);
        if (readyToHide)
        {
            showing = false;
            GetComponent<ImageFadeInOut>().FadeOut();
            return;
        }
    }
}
