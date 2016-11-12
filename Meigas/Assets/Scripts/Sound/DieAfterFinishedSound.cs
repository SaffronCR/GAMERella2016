using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class DieAfterFinishedSound : MonoBehaviour {

    private float soundLength;

    private void Awake()
    {
        soundLength = this.GetComponent<AudioSource>().clip.length;
    }

	private void Start () {
        StartCoroutine(DieAfterSoundCoroutine());
	}
	
    private IEnumerator DieAfterSoundCoroutine()
    {
        yield return new WaitForSeconds(soundLength);
        Destroy(this.gameObject);
    }
	
}
