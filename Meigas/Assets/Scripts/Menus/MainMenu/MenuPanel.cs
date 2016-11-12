using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuPanel : MonoBehaviour {

    private Transform thisTransform;

    private void Awake()
    {
        thisTransform = this.transform;
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    protected virtual void Show()
    {

    }

    protected virtual void Hide()
    {

    }
}
