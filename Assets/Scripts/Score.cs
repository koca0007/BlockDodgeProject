using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Score : MonoBehaviour
{
    Block block;

    public Transform player;

    public Text mainMenuHighscoreText;

    public Button startButton;

    [Header("StartMenu")]
    public GameObject startGamePanel;
    public AudioClip StartSounds;
    private AudioSource audioSource;
    public AudioClip clickSound;
    public AudioClip gameOverSound;
    public AudioClip explosionSound;


    public GameObject pausePanel;

    public GameObject InGameUI;


    [Header("Score Elements")]
    public Text scoreText;
    public Text highscoreText;
    public int score = 0;
    public int highscore = 0;

    [Header("Game Over")]
    public GameObject gameOverPanel;
    public Text gameOverPanelScoreText;
    public Text gameOverHighScoreText;


    private float timeInGame;
    private float slowness = 10f;


    private void Awake()
    {
        block = FindObjectOfType<Block>();
        audioSource = GetComponent<AudioSource>();
        Time.timeScale = 0f;

        startGamePanel.SetActive(true);
        InGameUI.SetActive(false);
        gameOverPanel.SetActive(false);
        pausePanel.SetActive(false);
    }

    private void Start()
    {

        audioSource.PlayOneShot(StartSounds, 0.03f) ;
        

        GetHighScore();
        highscore = PlayerPrefs.GetInt("Highscore");
        mainMenuHighscoreText.text = "Best: " + highscore;

    }

    private void Update()
    {
        timeInGame += Time.deltaTime;
        if (timeInGame >= 1)
        {
            score += (int)timeInGame + 2;
            timeInGame -= (int)timeInGame;
            scoreText.text = score.ToString();
        }

        if (score > highscore)
        {
            PlayerPrefs.SetInt("Highscore", score);
            highscoreText.text = "Best: " + score.ToString();
            highscore = score;
        }

        if (Input.GetButtonDown("Cancel"))
        {
            if (InGameUI.activeInHierarchy == true && Time.timeScale == 1)
            {
                
                Time.timeScale = 0f;
                InGameUI.SetActive(false);
                pausePanel.SetActive(true);
                gameOverPanel.SetActive(false);
                audioSource.PlayOneShot(clickSound, 0.15f);
                Cursor.visible = true;
            }
        }

        if (startGamePanel.activeInHierarchy == true && Input.GetButtonDown("Submit"))
        {
            startGamePanel.SetActive(false);
            InGameUI.SetActive(true);
            gameOverPanel.SetActive(false);
            pausePanel.SetActive(false);
            Time.timeScale = 1f;
            audioSource.Stop();
            audioSource.PlayOneShot(clickSound, 0.15f);
        }

        if (pausePanel.activeInHierarchy == true && Input.GetButtonDown("Submit"))
        {
            Time.timeScale = 1f;
            InGameUI.SetActive(true);
            pausePanel.SetActive(false);
            startGamePanel.SetActive(false);
            audioSource.PlayOneShot(clickSound, 0.15f);
        }

        if (gameOverPanel.activeInHierarchy == true && (Input.GetButtonDown("Submit") || Input.GetButtonDown("Cancel")))
        {
            
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

            score = 0;
            gameOverPanel.SetActive(false);
        }

       if (InGameUI.activeInHierarchy == true)
        {
            Cursor.visible = false;
        }
    }

    public void StartGame()
    {
            startGamePanel.SetActive(false);
            InGameUI.SetActive(true);
            gameOverPanel.SetActive(false);
            pausePanel.SetActive(false);
            Time.timeScale = 1f;
            audioSource.Stop();
            audioSource.PlayOneShot(clickSound, 0.15f);
    }

    private void GetHighScore()
    {
        highscore = PlayerPrefs.GetInt("Highscore");
        highscoreText.text = "Best: " + highscore;
    }

    public IEnumerator EndGame()
    {
        Time.timeScale = 1f / slowness;
        Time.fixedDeltaTime /= slowness;
        yield return new WaitForSeconds(0.12f); 
        Time.timeScale = 0f;
        gameOverPanel.SetActive(true);

        gameOverPanelScoreText.text = "Score: " + score.ToString();
        gameOverHighScoreText.text = "Best: " + highscore.ToString();
                
        scoreText.text = score.ToString();

        audioSource.PlayOneShot(gameOverSound, 0.2f);
    }

    public void RestartGame()
    { 
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        
        score = 0;
        gameOverPanel.SetActive(false);
        Destroy(block);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        InGameUI.SetActive(true);
        pausePanel.SetActive(false);
        audioSource.PlayOneShot(clickSound);

        if (gameOverPanel.activeInHierarchy == true)
        {
            Time.timeScale = 0f;
            pausePanel.SetActive(false);
            gameOverPanel.SetActive(true);
            audioSource.PlayOneShot(clickSound, 0.15f);
        }
    }

    public void MainMenu()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 0f;
        InGameUI.SetActive(false);
        startGamePanel.SetActive(true);
        pausePanel.SetActive(false);
        score = 0;
        audioSource.PlayOneShot(clickSound, 0.15f);
        Cursor.visible = true;
    }

    public void ExitGame()
    {
        audioSource.PlayOneShot(clickSound, 0.15f);
        Application.Quit();
    }
}