using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MainMenuManager : MonoBehaviour
{
    public GameObject settingsPanel; //설정 패널
    public GameObject settingButton; //설정 안에 버튼(설정에서 메인메뉴로 넘어가는 버튼)
    public GameObject howtoplayPanel;
    public Slider bgmSlider; // 소리 조절
    public Slider sfxSlider; // 효과음 조절
    public Slider brightnessSlider; //화면 밝기 
    public AudioSource bgmAudioSource; //음악 재생
    public Image brightnessOverlay;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void StartButton()
    {
        SceneManager.LoadScene("PlayScene");
    }
    void Start()
    {
       settingsPanel.SetActive(false); //처음은 창 닫기
       howtoplayPanel.SetActive(false);
       bgmSlider.value = 1f;
       sfxSlider.value = 1f;
       brightnessSlider.value = 1f;

       bgmSlider.onValueChanged.AddListener(ChangeBGMVolume);
       sfxSlider.onValueChanged.AddListener(ChangeSFXVolume);
       brightnessSlider.onValueChanged.AddListener(ChangeBrightness);
       ChangeBrightness(brightnessSlider.value);
    }
    public void OpenSettings()
    {
        settingsPanel.SetActive(true); //설정창 열기
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false); //설정창 닫기
    }
    public void OpenHowToPlay()
    {
        howtoplayPanel.SetActive(true);
    }
    public void CloseHowToPlay()
    {
        howtoplayPanel.SetActive(false);
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
        // 나중에 효과음 AudioSource와 연결
        Debug.Log("SFX Volume : " + value);
    }
    public void ChangeBrightness(float value)
    {
        Color color = brightnessOverlay.color;
        color.a = (1f - value) * 0.7f;
        brightnessOverlay.color = color;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    // Update is called once per frame
}
