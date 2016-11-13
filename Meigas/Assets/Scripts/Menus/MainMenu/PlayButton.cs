using UnityEngine;
using System.Collections;

public class PlayButton : MainMenuPanelButton {

    protected void OnEnable()
    {
        MenusInputController.deselectMainPanelButtons += Deselect;
        MenusInputController.selectPlayButton += Select;
    }

    protected void OnDisable()
    {
        MenusInputController.deselectMainPanelButtons -= Deselect;
        MenusInputController.selectPlayButton -= Select;
    }

    private void Start()
    {
        Select();
    }
}
