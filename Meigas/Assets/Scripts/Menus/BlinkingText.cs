using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BlinkingText : MonoBehaviour {

    private Text thisText;
    [SerializeField]
    private float timeTakenDuringLerp = 2f;

    private void Awake()
    {
        thisText = this.GetComponent<Text>();
    }

	private void Start () {
        StartCoroutine(BlinkingTextCoroutine());	
	}

    private IEnumerator BlinkingTextCoroutine()
    {
        while (true)
        {
            yield return FadeCoroutine(0f);
            yield return FadeCoroutine(0.8f);
        }
    }

    private IEnumerator FadeCoroutine(float endingImageAlpha)
    {
        bool isLerping = true;
        float _timeStartedLerping = Time.time;

        while (isLerping)
        {
            float timeSinceStarted = Time.time - _timeStartedLerping;
            float percentageComplete = timeSinceStarted / timeTakenDuringLerp;

            Color _color = thisText.color;

            _color.a = Mathf.Lerp(_color.a, endingImageAlpha, percentageComplete);

            thisText.color = _color;

            if (percentageComplete >= 1f)
            {
                isLerping = false;
            }

            yield return null;
        }
    }
}
