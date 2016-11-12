using UnityEngine;
using UnityEngine.SceneManagement;
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
    private string moveAndSelect;
    private string goBack;

    public GameObject mainMenuPanel;
    public GameObject creditsPanel;
    public GameObject buttonsPanel;
    public GameObject rankingPanel;
    public GameObject howToPlayPanel;


    public delegate void DeselectMainPanelButtons();
    public static event DeselectMainPanelButtons deselectMainPanelButtons;

    public delegate void SelectNewGameButton();
    public static event SelectNewGameButton selectNewGameButton;

    public delegate void SelectRankingButton();
    public static event SelectRankingButton selectRankingButton;

    public delegate void SelectHowToPlayButton();
    public static event SelectHowToPlayButton selectHowToPlayButton;

    public delegate void SelectExitButton();
    public static event SelectExitButton selectExitButton;

    public delegate void UpdateButtonPanelText(string newText);
    public static event UpdateButtonPanelText updateButtonPanelText;

    private void Awake()
    {

    }

    private void Start()
    {
        moveAndSelect = "Up/Down: Move selection    Z: Accept";
        goBack = "X: Back";
        updateButtonPanelText(moveAndSelect);
        selectNewGameButton();
    }

    private void Update()
    {
        switch (currentPanel)
        {
            case mainPanels.main:
                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    mainPanelIndex = (mainPanelIndex + 1) % mainPanelButtonsLength;
                    UpdateSelectedMainPanelButton(mainPanelIndex);
                }
                else if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    mainPanelIndex--;

                    if (mainPanelIndex < 0)
                    {
                        mainPanelIndex = (mainPanelButtonsLength - 1);
                    }

                    UpdateSelectedMainPanelButton(mainPanelIndex);
                }
                else if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Z))
                {
                    PressSelectedMainPanelButton(mainPanelIndex);
                }
                break;

            case mainPanels.ranking:
                if (Input.GetKeyDown(KeyCode.X))
                {
                    rankingPanel.SetActive(false);
                    mainMenuPanel.SetActive(true);
                    creditsPanel.SetActive(true);
                    updateButtonPanelText(moveAndSelect);
                    currentPanel = mainPanels.main;
                }
                break;

            case mainPanels.how2play:
                if (Input.GetKeyDown(KeyCode.X))
                {
                    howToPlayPanel.SetActive(false);
                    mainMenuPanel.SetActive(true);
                    creditsPanel.SetActive(true);
                    updateButtonPanelText(moveAndSelect);
                    currentPanel = mainPanels.main;
                }
                break;

            default:
                break;
        }
    }

    private void UpdateSelectedMainPanelButton(int value)
    {
        deselectMainPanelButtons();
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
   
    private void PressSelectedMainPanelButton(int value)
    {
        switch (value)
        {
            case 0:
                //Not yet implemented
                //SceneManager.LoadScene("Intro");
                break;
            case 1:
                mainMenuPanel.SetActive(false);
                creditsPanel.SetActive(false);
                rankingPanel.SetActive(true);
                updateButtonPanelText(goBack);
                currentPanel = mainPanels.ranking;
                break;
            case 2:
                mainMenuPanel.SetActive(false);
                creditsPanel.SetActive(false);
                howToPlayPanel.SetActive(true);
                updateButtonPanelText(goBack);
                currentPanel = mainPanels.how2play;
                break;
            case 3:
                //Application.Quit();
                break;
            default:
                break;
        }
    }
}
