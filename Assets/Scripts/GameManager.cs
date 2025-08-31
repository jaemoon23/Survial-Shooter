using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    public UiManager uiManager;

    public MonsterSpawner bunnySpawner;
    public MonsterSpawner bearSpawner;
    public MonsterSpawner ephantSpawner;

    public bool IsGameOver { get; private set; } = false;

    private float restartInterval = 3f;
    private float restartTimer = 0f;

    private bool isPaused = false;
    private int score;

    private void OnEnable()
    {
        Time.timeScale = 1f;
        IsGameOver = false;
        restartTimer = 0f;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
            if (isPaused)
            {
                Time.timeScale = 0f; // 게임 일시정지
                uiManager.MenuPanel(true);
            }
            else
            {
                Time.timeScale = 1f; // 게임 재개
                uiManager.MenuPanel(false);
            }
        }

        if (IsGameOver)
        {
            restartTimer += Time.unscaledDeltaTime;
            if (restartTimer > restartInterval)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }
    
    public void AddScore(int score)
    {
        this.score += score;
        uiManager.UpdateScore(this.score);
    }
    public void EndGame()
    {
        bunnySpawner.enabled = false;
        bearSpawner.enabled = false;
        ephantSpawner.enabled = false;
        IsGameOver = true;
        Time.timeScale = 0f;
        uiManager.GameOver(true);
    }
}
