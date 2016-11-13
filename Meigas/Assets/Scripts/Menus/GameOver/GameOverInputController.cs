using UnityEngine;
using System.Collections;

public class GameOverInputController : MonoBehaviour {

    public TransitionBetweenScenes transitioner;

    private void Start()
    {
        SoundManager.PlaySound("night-forest");
        transitioner.fadingImage.color = Color.black;
        StartCoroutine(WaitAndFadeInCoroutine());
    }

	private void Update () {
        if (Input.anyKeyDown)
        {
            SoundManager.PlaySound("menu-pressed-play-button");
            transitioner.TransitionToNewScene();
        }
	}

    private IEnumerator WaitAndFadeInCoroutine()
    {
        yield return new WaitForSeconds(0.2f);
        transitioner.FadeIn();
    }
}
