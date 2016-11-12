using UnityEngine;
using System.Collections;

public class NewGameButton : MainMenuPanelButton
{

    protected void OnEnable()
    {
        MenusInputController.deselectMainPanelButtons += Deselect;
        MenusInputController.selectNewGameButton += Select;
    }

    protected void OnDisable()
    {
        MenusInputController.deselectMainPanelButtons -= Deselect;
        MenusInputController.selectNewGameButton -= Select;
    }
}
