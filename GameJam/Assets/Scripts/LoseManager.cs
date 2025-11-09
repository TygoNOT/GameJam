using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoseManager : MonoBehaviour
{
    public GameObject losePanel;
    public Button retryButton;
    public Button exitButton;
    private bool isGameOver = false;
    void Start()
    {
        retryButton.onClick.AddListener(Retry);
        exitButton.onClick.AddListener(ExitToMenu);
    }

    void Update()
    {
        
    }

    public void Lose()
    {
        if (isGameOver) return;
        isGameOver = true;

        losePanel.SetActive(true);
        Time.timeScale = 0f;
    }
    public void Retry()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ExitToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
