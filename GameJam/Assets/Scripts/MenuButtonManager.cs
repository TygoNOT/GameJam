using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuButtonManager : MonoBehaviour
{
    [Header("Buttons")]
    public Button playButton;
    public Button optionsButton;
    public Button exitButton;

    [Header("Others")]
    public GameObject optionsPanel;
    void Start()
    {
        playButton.onClick.AddListener(OnPlayClicked);
        optionsButton.onClick.AddListener(OnOptionsClicked);
        exitButton.onClick.AddListener(OnExitClicked);
    }

    void Update()
    {
        
    }

    public void OnPlayClicked()
    {
        // SceneManager.LoadScene("GameScene");
    }

    public void OnExitClicked()
    {
        Application.Quit();
    }

    public void OnOptionsClicked()
    {
        optionsPanel.SetActive(true);
    }
}
