using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class TransitionBetweenScenes : FadeImage {

    [SerializeField]
    private float timeUntilLoadScene = 0.5f;
    [SerializeField]
    private string sceneNameToLoad;

    public void TransitionToNewScene()
    {
        StartCoroutine(TransitionToNewSceneCoroutine(sceneNameToLoad));
    }

    public void CustomTransitionToNewScene(string sceneName)
    {
        StartCoroutine(TransitionToNewSceneCoroutine(sceneName));
    }

    private IEnumerator TransitionToNewSceneCoroutine(string sceneName)
    {
        yield return FadeCoroutine(1f);
        yield return new WaitForSeconds(timeUntilLoadScene);
        SceneManager.LoadScene(sceneName);
    }
}
