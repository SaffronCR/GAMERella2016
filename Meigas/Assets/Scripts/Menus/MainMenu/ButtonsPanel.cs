using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ButtonsPanel : MonoBehaviour {

    private Text text;

    private void OnEnable()
    {
        MenusInputController.updateButtonPanelText += ChangeText;
    }

    private void OnDisable()
    {
        MenusInputController.updateButtonPanelText -= ChangeText;
    }

    private void Awake()
    {
        text = this.GetComponentInChildren<Text>();
    }

    private void ChangeText(string value)
    {
        text.text = value;
    }
}
