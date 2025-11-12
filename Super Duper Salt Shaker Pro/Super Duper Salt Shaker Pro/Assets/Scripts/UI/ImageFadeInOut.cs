using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ImageFadeInOut : MonoBehaviour
{
  public bool fadeIn;
  public bool fadeOut;

  void OnValidate()
  {
    if (fadeOut)
    {
      fadeOut = false;
      FadeOut();
    }

    if (fadeIn)
    {
      fadeIn = false;
      FadeIn();
    }
  }

  public void FadeIn()
  {
    StartCoroutine(FadeInCoroutine());
  }

  public void FadeOut()
  {
    StartCoroutine(FadeOutCoroutine());
  }
  private IEnumerator FadeOutCoroutine()
  {
    Image image = GetComponent<Image>();
    Color opaque = new Color(image.color.r, image.color.g, image.color.b, 1);
    image.color = opaque;

    for (float progress = 1; progress > 0; progress -= 0.007f)
    {
      float easedProgress = Mathf.SmoothStep(0, 1, progress);

      image.color = new Color(image.color.r, image.color.g, image.color.b, easedProgress);

      yield return null;
    }

    image.color = new Color(image.color.r, image.color.g, image.color.b, 0);
  }

  private IEnumerator FadeInCoroutine()
  {
    Image image = GetComponent<Image>();
    Color transparent = new Color(image.color.r, image.color.g, image.color.b, 0);
    image.color = transparent;

    for (float progress = 0; progress < 1; progress += 0.007f)
    {
      float easedProgress = Mathf.SmoothStep(0, 1, progress);

      image.color = new Color(image.color.r, image.color.g, image.color.b, easedProgress);

      yield return null;
    }

    image.color = new Color(image.color.r, image.color.g, image.color.b, 1);
  }
}
