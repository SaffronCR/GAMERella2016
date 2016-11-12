using UnityEngine;
using System.Collections;

public class RankingsButton : MainMenuPanelButton
{

    protected void OnEnable()
    {
        MenusInputController.deselectMainPanelButtons += Deselect;
        MenusInputController.selectRankingButton += Select;
    }

    protected void OnDisable()
    {
        MenusInputController.deselectMainPanelButtons -= Deselect;
        MenusInputController.selectRankingButton -= Select;
    }
}
