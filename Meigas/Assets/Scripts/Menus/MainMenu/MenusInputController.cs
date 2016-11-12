using UnityEngine;
using System.Collections;

public class MenusInputController : MonoBehaviour
{

    public enum mainPanels
    {
        main,
        ranking,
        how2play
    }

    private mainPanels currentPanel = mainPanels.main;
    private int mainPanelIndex = 0;
    private int mainPanelButtonsLength = 4;

    public delegate void DeselectMainPanelButtons();
    public static event DeselectMainPanelButtons deselectMainPanelButtons;

    public delegate void SelectNewGameButton();
    public static event SelectNewGameButton selectNewGameButton;

    public delegate void SelectRankingButton();
    public static event SelectRankingButton selectRankingButton;

    public delegate void HideRankingPanel();
    public static event HideRankingPanel hideRankingPanel;

    public delegate void SelectHowToPlayButton();
    public static event SelectHowToPlayButton selectHowToPlayButton;

    public delegate void HideHowToPlayPanel();
    public static event HideHowToPlayPanel hideHowToPlayPanel;

    public delegate void SelectExitButton();
    public static event SelectExitButton selectExitButton;

    private void Awake()
    {

    }

    private void Start()
    {
        selectNewGameButton();
    }

    private void Update()
    {
        switch (currentPanel)
        {
            case mainPanels.main:
                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    mainPanelIndex = (mainPanelIndex + 1) % (mainPanelButtonsLength - 1);
                }
                else if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    mainPanelIndex = (mainPanelIndex - 1) % (mainPanelButtonsLength - 1);
                }
                else if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Z))
                {
                    UpdateSelectedMainPanelButton(mainPanelIndex);
                }
                break;
            case mainPanels.ranking:
                break;
            case mainPanels.how2play:
                break;
            default:
                break;
        }
    }

    private void UpdateSelectedMainPanelButton(int value)
    {
        switch (value)
        {
            case 0:
                selectNewGameButton();
                break;
            case 1:
                selectRankingButton();
                break;
            case 2:
                selectHowToPlayButton();
                break;
            case 3:
                selectExitButton();
                break;
            default:
                break;
        }
    }
}
