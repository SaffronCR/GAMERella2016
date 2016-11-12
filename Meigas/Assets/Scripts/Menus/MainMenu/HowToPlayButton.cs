using UnityEngine;
using System.Collections;

public class HowToPlayButton : MainMenuPanelButton
{

    protected void OnEnable()
    {
        MenusInputController.deselectMainPanelButtons += Deselect;
        MenusInputController.selectHowToPlayButton += Select;
    }

    protected void OnDisable()
    {
        MenusInputController.deselectMainPanelButtons -= Deselect;
        MenusInputController.selectHowToPlayButton -= Select;
    }
}
