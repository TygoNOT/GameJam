using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] public Button playButton;
    [SerializeField] public Button optionsButton;
    [SerializeField] public Button exitButton;

    [Header("Others")]
    [SerializeField] public GameObject optionsPanel;

    [Header("UI")]
    [SerializeField] public Slider musicVolumeSlider;
    [SerializeField] public Slider sfxVolumeSlider;
    [SerializeField] public Toggle fullscreenToggle;

    [Header("Audio")]
    [SerializeField] public AudioMixer audioMixer;
    [SerializeField] private AudioSource buttonClickAudioSource;
    [SerializeField] private AudioClip buttonClickSound;

    private const string PrefMusVolume = "pref_music_volume";
    private const string PrefSfxVolume = "pref_sfx_volume";
    private const string PrefFullscreen = "pref_fullscreen";

    void Start()
    {
        float savedMusicVol = PlayerPrefs.HasKey(PrefMusVolume) ? PlayerPrefs.GetFloat(PrefMusVolume) : 1f;
        float savedSfxVol = PlayerPrefs.HasKey(PrefSfxVolume) ? PlayerPrefs.GetFloat(PrefSfxVolume) : 1f;
        bool savedFs = PlayerPrefs.HasKey(PrefFullscreen) ? PlayerPrefs.GetInt(PrefFullscreen) == 1 : Screen.fullScreen;

        ApplyMusicVolume(savedMusicVol, applyToSlider: false);
        ApplySFXVolume(savedSfxVol, applyToSlider: false);
        ApplyFullscreen(savedFs);

        playButton.onClick.AddListener(() => OnButtonClicked(OnPlayClicked));
        optionsButton.onClick.AddListener(() => OnButtonClicked(OnOptionsClicked));
        exitButton.onClick.AddListener(() => OnButtonClicked(OnExitClicked));

        if (musicVolumeSlider != null && sfxVolumeSlider != null)
        {
            musicVolumeSlider.value = savedMusicVol;
            musicVolumeSlider.onValueChanged.AddListener(ApplyMusicVolume);
            sfxVolumeSlider.value = savedSfxVol;
            sfxVolumeSlider.onValueChanged.AddListener(ApplySFXVolume);
        }

        if (fullscreenToggle != null)
        {
            fullscreenToggle.isOn = savedFs;
            fullscreenToggle.onValueChanged.AddListener(ApplyFullscreen);
        }

        if (buttonClickAudioSource == null)
        {
            buttonClickAudioSource = GetComponent<AudioSource>();

            if (buttonClickAudioSource == null)
            {
                buttonClickAudioSource = gameObject.AddComponent<AudioSource>();
                buttonClickAudioSource.playOnAwake = false;
            }
        }
    }

    private void OnButtonClicked(System.Action buttonAction)
    {
        PlayButtonClickSound();
        buttonAction?.Invoke();
    }

    private void PlayButtonClickSound()
    {
        if (buttonClickAudioSource != null && buttonClickSound != null)
        {
            buttonClickAudioSource.PlayOneShot(buttonClickSound);
        }
    }

    void Update()
    {

    }

    public void OnPlayClicked()
    {
        SceneManager.LoadScene("Infinite Level");
    }

    public void OnExitClicked()
    {
        Application.Quit();
    }

    public void OnOptionsClicked()
    {
        optionsPanel.SetActive(true);
    }

    private void OnDestroy()
    {
        if (musicVolumeSlider != null)
            musicVolumeSlider.onValueChanged.RemoveListener(ApplyMusicVolume);

        if (sfxVolumeSlider != null)
            sfxVolumeSlider.onValueChanged.RemoveListener(ApplySFXVolume);

        if (fullscreenToggle != null)
            fullscreenToggle.onValueChanged.RemoveListener(ApplyFullscreen);
    }

    public void ApplyMusicVolume(float value)
    {
        ApplyMusicVolume(value, applyToSlider: true);
    }

    public void ApplySFXVolume(float value)
    {
        ApplySFXVolume(value, applyToSlider: true);
    }

    private void ApplyMusicVolume(float value, bool applyToSlider)
    {
        PlayerPrefs.SetFloat(PrefMusVolume, value);

        if (audioMixer != null)
        {
            float dB = Mathf.Log10(value) * 20;
            audioMixer.SetFloat("MusicVolume", dB);
        }

        if (applyToSlider && musicVolumeSlider != null && musicVolumeSlider.value != value)
            musicVolumeSlider.value = value;

        PlayerPrefs.Save();
    }

    private void ApplySFXVolume(float value, bool applyToSlider)
    {
        PlayerPrefs.SetFloat(PrefSfxVolume, value);

        if (audioMixer != null)
        {
            float dB = Mathf.Log10(value) * 20;
            audioMixer.SetFloat("SFXVolume", dB);
        }

        if (applyToSlider && sfxVolumeSlider != null && sfxVolumeSlider.value != value)
            sfxVolumeSlider.value = value;

        PlayerPrefs.Save();
    }

    private void ApplyFullscreen(bool value)
    {
        Screen.fullScreen = value;

        PlayerPrefs.SetInt(PrefFullscreen, value ? 1 : 0);
        PlayerPrefs.Save();

        if (fullscreenToggle != null && fullscreenToggle.isOn != value)
            fullscreenToggle.isOn = value;
    }

    public void ToggleFullscreen()
    {
        bool newState = !(fullscreenToggle != null ? fullscreenToggle.isOn : Screen.fullScreen);
        ApplyFullscreen(newState);
    }
}