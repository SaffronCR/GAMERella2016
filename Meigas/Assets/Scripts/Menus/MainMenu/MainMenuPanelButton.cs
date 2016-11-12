using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public abstract class MainMenuPanelButton : MonoBehaviour{

    protected Image image;
    public Color deselectedColor = Color.white;
    public Color selectedColor = Color.white;

    

    private void Awake()
    {
        image = this.GetComponent<Image>();
    }

    private void Start()
    {
        image.color = deselectedColor;
    }

    protected void Select()
    {
        image.color = selectedColor;
    }

    protected void Deselect()
    {
        image.color = deselectedColor;
    }

    //Just in case animations for the buttons will be implemented
    protected virtual void OnPress()
    {
        //...
    }
}
