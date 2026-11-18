using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject pausePanel;
    public GameObject settingsPanel;
    public GameObject howtoplayPanel;
    public Slider bgmSlider; // 소리 조절
    public Slider sfxSlider; // 효과음 조절
    public Slider brightnessSlider; //화면 밝기 
    public AudioSource bgmAudioSource; //음악 재생
    public Image brightnessOverlay;
    public Transform player; //플레이어 위치 저장
    void Start()
    {
        pausePanel.SetActive(false);
        settingsPanel.SetActive(false);
        howtoplayPanel.SetActive(false);
        bgmSlider.value = 1f;
        sfxSlider.value = 1f;
        brightnessSlider.value = 1f;

        bgmSlider.onValueChanged.AddListener(ChangeBGMVolume);
        sfxSlider.onValueChanged.AddListener(ChangeSFXVolume);
        brightnessSlider.onValueChanged.AddListener(ChangeBrightness);
        ChangeBrightness(brightnessSlider.value);
    }
    public void ContinueGame()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }
    public void GoMainMenu()
    {
        PlayerGameData.playerPosition = player.position;
        PlayerGameData.hasSavedPosition = true;

        SceneManager.LoadScene("MainMenuScene");
    }
    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
        pausePanel.SetActive(false);
    }
    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
        pausePanel.SetActive(true);
    }
    public void OpenHowToPlay()
    {
        howtoplayPanel.SetActive(true);
        pausePanel.SetActive(false);
    }
    public void CloseHowToPlay()
    {
        howtoplayPanel.SetActive(false);
        pausePanel.SetActive(true);
    }
    private void ChangeBGMVolume(float value) //음량 조절
    {
        if (bgmAudioSource != null)
        {
            bgmAudioSource.volume = value;
        }
    }

    private void ChangeSFXVolume(float value) //sfx조절
    {
        Debug.Log("SFX Volume : " + value);
    }
    public void ChangeBrightness(float value)
    {
        Debug.Log("밝기 값 : " + value);
        Color color = brightnessOverlay.color;
        color.a = (1f - value) * 0.7f;
        brightnessOverlay.color = color;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!pausePanel.activeSelf)
            {
                pausePanel.SetActive(true);
                Time.timeScale = 0f;
            }
            else
            {
                pausePanel.SetActive(false);
                Time.timeScale = 1f;
            }
        }
    }
}
