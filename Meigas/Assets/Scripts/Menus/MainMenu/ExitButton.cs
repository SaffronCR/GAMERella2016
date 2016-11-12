using UnityEngine;
using System.Collections;

public class ExitButton : MainMenuPanelButton {

    protected void OnEnable()
    {
        MenusInputController.deselectMainPanelButtons += Deselect;
        MenusInputController.selectExitButton += Select;
    }

    protected void OnDisable()
    {
        MenusInputController.deselectMainPanelButtons -= Deselect;
        MenusInputController.selectExitButton -= Select;
    }
}
