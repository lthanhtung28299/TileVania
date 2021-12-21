using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("player Life And Score")]
    [SerializeField] int playerLives = 3;
    [SerializeField] int scorePerlives = 500;
    [SerializeField] int score = 0;

    [Header("Main Menu UI")]
    [SerializeField] GameObject menuCanvas;
    [SerializeField] GameObject startbt;
    [SerializeField] GameObject quitbt;
    [SerializeField] GameObject gametitle;
    [Header("Ingame UI")]
    [SerializeField] GameObject ingameCanvas;
    [SerializeField] GameObject walkthroughControl;
    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] GameObject pausedButton;
    [SerializeField] GameObject resumeGame;
    [SerializeField] GameObject HomeButton;
    [Header("FinalSceneCanvas")]
    [SerializeField] GameObject endGameCanvas;
    [SerializeField] TextMeshProUGUI finalScoreText;
    [SerializeField] GameObject finalHomeButton;


    void Awake()
    {
        int numGameManager = FindObjectsOfType<GameManager>().Length;
        if(numGameManager > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        livesText.text = $"Live: {playerLives.ToString()}";
        scoreText.text = $"Score: {score.ToString()}";
        ingameCanvas.SetActive(false);
        menuCanvas.SetActive(true);
        endGameCanvas.SetActive(false);
    }

    IEnumerator controllerWalkthrough()
    {
        yield return new WaitForSecondsRealtime(5f);
        walkthroughControl.SetActive(false);
    }

    public void ProcessPlayerDeath()
    {
        if(playerLives > 1)
        {
            TakeLive();
        }
        else
        {
            ResetGame();
        }
    }

    public void AddScore(int pointsToAdd)
    {
        score += pointsToAdd;
        scoreText.text = $"Score: {score.ToString()}";
        int finalScore = score + (playerLives * scorePerlives);
        finalScoreText.text = $"Congratulation you win!!! \n Total Score: {finalScore.ToString()}";
    }

    void TakeLive()
    {
        playerLives--;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        livesText.text = $"Live: {playerLives.ToString()}";
    }

    void ResetGame()
    {
        FindObjectOfType<ScenePersist>().ResetScenePersist();
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }

    public void GamePaused()
    {
        resumeGame.SetActive(true);
        HomeButton.SetActive(true);
        pausedButton.SetActive(false);
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        pausedButton.SetActive(true);
        resumeGame.SetActive(false);
        HomeButton.SetActive(false);
    }

    public void BackToHomeMenu()
    {
        Time.timeScale = 1f;
        ResetGame();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
        menuCanvas.SetActive(false);
        ingameCanvas.SetActive(true);
        walkthroughControl.SetActive(true);
        StartCoroutine("controllerWalkthrough");
        pausedButton.SetActive(true);
        resumeGame.SetActive(false);
        HomeButton.SetActive(false);
    }

    public void FinalScore()
    {
        ingameCanvas.SetActive(false);
        endGameCanvas.SetActive(true);
    }
}
