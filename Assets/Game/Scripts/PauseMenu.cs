using System.Collections;
using TMPro;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private GameObject gameOverMenuUI;
    [SerializeField] private GameObject gameUIPanel;
    [SerializeField] private TextMeshProUGUI playerPointsText;
    [SerializeField] private TextMeshProUGUI gameOverText;
    [SerializeField] private float timeToWait = 0.5f;

    private PlayerPoints playerPoints;
    private LevelLoader levelLoader;
    private GameTimer gameTimer;

    private readonly string pointsMessage = "{0} pts";
    private readonly string message = "Congratulations!\r\nYou have achieved {0} points.";

    public static bool GameIsPaused { get; set; } = false;

    private void Awake()
    {
        levelLoader = GameObject.FindGameObjectWithTag("LevelLoader").GetComponent<LevelLoader>();
        var gameController = GameObject.FindGameObjectWithTag("GameController");
        gameTimer = gameController.GetComponent<GameTimer>();
        playerPoints = gameController.GetComponent<PlayerPoints>();
    }

    private void Start()
    {
        StartCoroutine(LateStart());
    }

    private IEnumerator LateStart()
    {

        yield return new WaitForSeconds(timeToWait);

        SoundManager.PlayMusic(SoundManager.Music.GameMusic);
    }

    private void Update()
    {
        if (gameTimer.TimeHasRunOut)
        {
            HidePauseMenu();
            HideGameUIPanel();
            ShowGameOverPanel();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && !gameOverMenuUI.activeSelf)
        {
            if (GameIsPaused)
            {
                HidePauseMenu();
                HideGameOverPanel();
                ShowGameUIPanel();
                Continue();
            }
            else {
                HideGameOverPanel();
                HideGameUIPanel();
                ShowPauseMenu();
                Pause();
            }
        }

        if(gameUIPanel.activeSelf)
        {
            playerPointsText.text = string.Format(pointsMessage, playerPoints.PotalPlayerPoints);
        }
    }

    public void Restart()
    {
        HidePauseMenu();
        HideGameOverPanel();
        ShowGameUIPanel();
        levelLoader.ReloadLevel();
    }

    public void LoadMenu()
    {
        HidePauseMenu();
        HideGameOverPanel();
        ShowGameUIPanel();
        levelLoader.LoadPreviousLevel();
    }

    public void QuitGame()
    {
        levelLoader.QuitGame();
    }

    private void Pause()
    {
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    private void Continue()
    {
        Time.timeScale = 1.0f;
        GameIsPaused = false;
    }

    private void ShowPauseMenu()
    {
        pauseMenuUI.SetActive(true);
    }

    private void HidePauseMenu()
    {
        pauseMenuUI.SetActive(false);
    }

    private void ShowGameOverPanel()
    {
        gameOverText.text = string.Format(message, playerPoints.PotalPlayerPoints);
        gameOverMenuUI.SetActive(true);
    }

    private void HideGameOverPanel()
    {
        gameOverMenuUI.SetActive(false);
    }

    private void ShowGameUIPanel()
    {
        gameUIPanel.SetActive(true);
    }

    private void HideGameUIPanel()
    {
        gameUIPanel.SetActive(false);
    }
}
