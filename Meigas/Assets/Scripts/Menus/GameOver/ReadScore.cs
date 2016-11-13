using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ReadScore : MonoBehaviour {

    private Text scoreText;

	private void Awake() {
        scoreText = this.GetComponent<Text>();
	}

    private void Start()
    {
        scoreText.text = "Saved Souls\n" + PlayerPrefs.GetFloat("score"); ;
    }

}
