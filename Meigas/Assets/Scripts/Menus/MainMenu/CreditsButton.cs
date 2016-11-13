using UnityEngine;
using System.Collections;

public class CreditsButton : MainMenuPanelButton
{

    protected void OnEnable()
    {
        MenusInputController.deselectMainPanelButtons += Deselect;
        MenusInputController.selectCreditsButton += Select;
    }

    protected void OnDisable()
    {
        MenusInputController.deselectMainPanelButtons -= Deselect;
        MenusInputController.selectCreditsButton -= Select;
    }
}
