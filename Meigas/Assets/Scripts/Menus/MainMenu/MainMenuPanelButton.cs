using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public abstract class MainMenuPanelButton : MonoBehaviour{

    protected Text text;
    protected string originalText;
    public Color deselectedColor = Color.black;
    public Color selectedColor = Color.white;

    

    private void Awake()
    {
        text = this.GetComponent<Text>();
    }

    private void Start()
    {
        originalText = text.text;
        text.color = deselectedColor;
    }

    protected void Select()
    {
        text.text = "> " + text.text;
        text.color = selectedColor;
    }

    protected void Deselect()
    {
        text.text = originalText;
        text.color = deselectedColor;
    }

    //Just in case animations for the buttons will be implemented
    protected void OnPress()
    {
        //...
    }
}
