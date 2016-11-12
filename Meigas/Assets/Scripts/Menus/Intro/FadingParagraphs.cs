using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class FadingParagraphs : MonoBehaviour {

    private Queue<Text> paragraphs;
    private float movementParagraph = 50;
    [SerializeField]
    private float timeTakenDuringLerp = 2f;
    [SerializeField]
    private float timeShowingTextUntilGameStarts = 1f;

    public TransitionBetweenScenes transitioner;

    public bool isPlayingIntro = false;

    private void Awake()
    {
        paragraphs = new Queue<Text>();
        for(int i = 0; i < this.transform.childCount; i++)
        {
            Text paragraph = this.transform.GetChild(i).GetComponent<Text>();
            Color _color = paragraph.color;
            _color.a = 0;
            paragraph.color = _color;

            paragraphs.Enqueue(paragraph);
        }
    }

	private void Start () {
        StartCoroutine(FadingCoroutine());
	}

    private IEnumerator FadingCoroutine()
    {
        isPlayingIntro = true;

        bool isLerping;

        while (paragraphs.Count > 0)
        {
            Text currentParagraph = paragraphs.Dequeue();
            Vector2 currentParagraphPosition = currentParagraph.rectTransform.anchoredPosition;
            float endPositionX = currentParagraph.rectTransform.anchoredPosition.x + movementParagraph;
            Color _color = currentParagraph.color;

            float _timeStartedLerping = Time.time;
            isLerping = true;

            while (isLerping)
            {
                float timeSinceStarted = Time.time - _timeStartedLerping;
                float percentageComplete = timeSinceStarted / timeTakenDuringLerp;

                currentParagraphPosition.x = Mathf.Lerp(currentParagraph.rectTransform.anchoredPosition.x, endPositionX, percentageComplete);
                _color.a = Mathf.Lerp(currentParagraph.color.a, 1, percentageComplete);

                currentParagraph.rectTransform.anchoredPosition = currentParagraphPosition;
                currentParagraph.color = _color;

                if(percentageComplete >= 1f)
                {
                    isLerping = false;
                }

                yield return null;
            }
        }

        transitioner.TransitionToNewScene();
    }

}
