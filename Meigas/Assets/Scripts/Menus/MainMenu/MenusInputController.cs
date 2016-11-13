using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenusInputController : MonoBehaviour
{

    public enum mainPanels
    {
        main,
        //ranking,
        how2play
    }

    private mainPanels currentPanel = mainPanels.main;
    private int mainPanelIndex = 0;
    [SerializeField]
    private int mainPanelButtonsLength = 3;

    public GameObject titleImage;
    public GameObject mainMenuPanel;
    public GameObject creditsPanel;
    //public GameObject rankingPanel;
    public GameObject howToPlayPanel;
    public GameObject transitionImage;

    public delegate void DeselectMainPanelButtons();
    public static event DeselectMainPanelButtons deselectMainPanelButtons;

    public delegate void SelectPlayButton();
    public static event SelectPlayButton selectPlayButton;

    //public delegate void SelectRankingButton();
    //public static event SelectRankingButton selectRankingButton;

    public delegate void SelectHowToPlayButton();
    public static event SelectHowToPlayButton selectHowToPlayButton;

    public delegate void SelectExitButton();
    public static event SelectExitButton selectExitButton;

    private void Start()
    {
        SoundManager.PlaySound("CelticMusicLegend", true, 0.4f);
    }

    private void Update()
    {
        switch (currentPanel)
        {
            case mainPanels.main:
                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    SoundManager.PlaySound("menu-move-button");
                    mainPanelIndex = (mainPanelIndex + 1) % mainPanelButtonsLength;
                    UpdateSelectedMainPanelButton(mainPanelIndex);
                }
                else if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    SoundManager.PlaySound("menu-move-button");
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

            /*
            case mainPanels.ranking:
                if (Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.Delete) || Input.GetKeyDown(KeyCode.Escape))
                {
                    rankingPanel.SetActive(false);
                    mainMenuPanel.SetActive(true);
                    creditsPanel.SetActive(true);
                    updateButtonPanelText(moveAndSelect);
                    currentPanel = mainPanels.main;
                }
                break;
            */

            case mainPanels.how2play:
                if (Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.Delete) || Input.GetKeyDown(KeyCode.Escape))
                {
                    SoundManager.PlaySound("menu-move-button");
                    howToPlayPanel.SetActive(false);
                    titleImage.SetActive(true);
                    mainMenuPanel.SetActive(true);
                    creditsPanel.SetActive(true);
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
                selectPlayButton();
                break;
            case 1:
                selectHowToPlayButton();
                break;
            case 2:
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
                SoundManager.PlaySound("menu-pressed-play-button");
                transitionImage.GetComponent<TransitionBetweenScenes>().TransitionToNewScene();
                break;
            case 1:
                SoundManager.PlaySound("menu-move-button");
                titleImage.SetActive(false);
                mainMenuPanel.SetActive(false);
                creditsPanel.SetActive(false);
                howToPlayPanel.SetActive(true);
                currentPanel = mainPanels.how2play;
                break;
            case 2:
                //SoundManager.PlaySound("menu-move-button");
                Application.Quit();
                break;
            default:
                break;
        }
    }
}
