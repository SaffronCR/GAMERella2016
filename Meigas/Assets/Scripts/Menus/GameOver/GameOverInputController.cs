using UnityEngine;
using System.Collections;

public class GameOverInputController : MonoBehaviour {

    public TransitionBetweenScenes transitioner;

    private void Start()
    {
        transitioner.fadingImage.color = Color.black;
        transitioner.FadeIn();
    }

	private void Update () {
        if (Input.anyKeyDown)
        {
            transitioner.TransitionToNewScene();
        }
	}
}
