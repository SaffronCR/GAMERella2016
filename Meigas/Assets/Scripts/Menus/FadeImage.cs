using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeImage : MonoBehaviour {

    [SerializeField]
    private float timeTakenDuringLerp = 2f;
    [SerializeField]
    private float timeShowingTextUntilGameStarts = 1f;

    public Image fadingImage;

    private void Awake()
    {
        fadingImage = this.GetComponent<Image>();
    }

	public void FadeOut()
    {
        StartCoroutine(FadeCoroutine(1f));
    }
	
    public void FadeIn()
    {
        StartCoroutine(FadeCoroutine(0f));
    }

    public void CustomFade(float alpha)
    {
        StartCoroutine(FadeCoroutine(alpha));
    }

    protected virtual IEnumerator FadeCoroutine(float endingImageAlpha)
    {
        bool isLerping = true;
        float _timeStartedLerping = Time.time;

        while (isLerping)
        {
            float timeSinceStarted = Time.time - _timeStartedLerping;
            float percentageComplete = timeSinceStarted / timeTakenDuringLerp;

            Color _color = fadingImage.color;

            _color.a = Mathf.Lerp(_color.a, endingImageAlpha, percentageComplete);

            fadingImage.color = _color;

            if (percentageComplete >= 1f)
            {
                isLerping = false;
            }

            yield return null;
        }
    }

}
