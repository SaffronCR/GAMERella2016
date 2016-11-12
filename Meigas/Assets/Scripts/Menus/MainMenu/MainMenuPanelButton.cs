using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public abstract class MainMenuPanelButton : MonoBehaviour
{

    protected Text text;
    protected string originalText;
    public Color deselectedColor;
    public Color selectedColor;

    protected void OnEnable()
    {

    }

    protected void OnDisable()
    {

    }

    private void Awake()
    {
        text = this.GetComponent<Text>();
    }

    private void Start()
    {
        originalText = text.text;
        text.color = deselectedColor;
    }

    private void Select()
    {
        text.text = "> " + text.text;
        text.color = selectedColor;
    }

    private void Deselect()
    {
        text.text = originalText;
        text.color = deselectedColor;
    }

    protected abstract void OnPress();

}
