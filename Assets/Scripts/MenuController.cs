using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.EventSystems;

public class MenuController : MonoBehaviour
{

    [Header("Main Menu Sounds")]
    public AudioSource newGameButtonSound;
    public AudioSource loadGameButtonSound;
    public AudioSource optionsMainButtonSound;
    public AudioSource quitGameButtonSound;
    
    [Header("Character Selection Sounds")]

    public AudioSource playAsDonusButtonSound;
    public AudioSource playAsSivionButtonSound;
    public AudioSource returnToMainMenuButtonSound;
    public AudioSource returnToCharacterSelectionSound;
    public AudioSource noFileFoundSound;

    [Header("Options Menu Sounds")]

    public AudioSource toSoundMenuSound;
    public AudioSource toGraphicsMenuSound;
    public AudioSource toGameplayMenuSound;

    [Header("Settings Sounds")]

    public AudioSource resetToDefaultButtonSound;
    public AudioSource applyAndReturnSound;
    public AudioSource returnToOptionsMenuSound;


    [Header("Volume Settings")]

    [SerializeField] private TMP_Text VolumeTextValue = null;
    [SerializeField] private Slider volumeSlider = null;
    [SerializeField] private float defaultVolume = 1.0f;

    [Header("Game Settings")]

    [SerializeField] private TMP_Text controllerSensitivityTextValue = null;
    [SerializeField] private Slider controllerSensitivitySlider = null;
    [SerializeField] private int defaultSensitivity = 4;
    public int mainControllerSensitivity = 4;

    [Header("Toggle Settings")]
    
    [SerializeField] private Toggle invertYToggle = null;

    [Header("Graphics Settings")]

    [SerializeField] private Slider brightnessSlider = null;
    [SerializeField] private TMP_Text brightnessTextValue = null;
    [SerializeField] private float defaultBrightness = 1.0f;

    private int _qualityLevel;
    private bool _isFullscreen;
    private float _brightnessLevel;

    [Header("Confirmation Box")]
    [SerializeField] private GameObject confirmationPrompt = null;
    
    [Header("Levels to Load")]

    public string _NewGameLevel;
    private string LevelToLoad;
    [SerializeField] private GameObject NoSavedGameDialog = null;

    [Header("Resolution Dropdowns")]

    //public TMP_Dropdown resolutionDropdown;
    //private Resolution[] resolutions;
    //private string option;
    //private int currentResolutionIndex = 0;

    [SerializeField] private Toggle fullScreenToggle;

    [Header("Menu Nav")]
    public GameObject newGameFirstButton;
    public GameObject donusFirstButton;
    public GameObject sivionFirstButton;
    public GameObject optionsFirstButton;

    private void Start()
    {
        newGameSelect();
    }
    public void newGameSelect()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(newGameFirstButton);
    }
    public void donusSelect()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(donusFirstButton);
    }
    public void sivionSelect()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(sivionFirstButton);
    }
    public void optionsSelect()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(optionsFirstButton);
    }

    public void NewGameSound()
    {
        newGameButtonSound.Play();
    }

    public void LoadGameSound()
    {
        loadGameButtonSound.Play();
    //    mysounds.PlayOneShot(loadGameButtonSound);
    }

    public void OptionMainButtonSound()
    {
        optionsMainButtonSound.Play();
    //    mysounds.PlayOneShot(optionsMainButtonSound);
    }

    public void QuitGameButtonSound()
    {
        quitGameButtonSound.Play();
    //    mysounds.PlayOneShot(quitGameButtonSound);
    }
    
    public void PlayAsDonusButtonSound()
    {
        playAsDonusButtonSound.Play();
    //    mysounds.PlayOneShot(playAsDonusButtonSound);
    }

    public void PlayAsSivionButtonSound()
    {
        playAsSivionButtonSound.Play();
    //    mysounds.PlayOneShot(playAsSivionButtonSound);
    }


    public void ReturnToMainMenuButtonSound()
    {
        returnToMainMenuButtonSound.Play();
    //    mysounds.PlayOneShot(returnToMainMenuButtonSound);
    }

    public void ReturnToCharacterSelectionMenuButtonSound()
    {
        returnToCharacterSelectionSound.Play();
    //    mysounds.PlayOneShot(returnToCharacterSelectionSound);
    }

    public void NoFileFoundSound()
    {
        noFileFoundSound.Play();
    //    mysounds.PlayOneShot(noFileFoundSound);
    }


    public void ToSoundMenuSound()
    {
        toSoundMenuSound.Play();
    //    mysounds.PlayOneShot(toSoundMenuSound);
    }

    public void ToGraphicsMenuSound()
    {
        toGraphicsMenuSound.Play();
   //     mysounds.PlayOneShot(toGraphicsMenuSound);
    }
    
    public void ToGameplayMenuSound()
    {
        toGameplayMenuSound.Play();
  //      mysounds.PlayOneShot(toGameplayMenuSound);
    }

    public void ResetToDefaultButtonSound()
    {
        resetToDefaultButtonSound.Play();
  //      mysounds.PlayOneShot(resetToDefaultButtonSound);
    }

    public void ApplyAndReturnSound()
    {
        applyAndReturnSound.Play();
  //      mysounds.PlayOneShot(applyAndReturnSound);
    }

    public void ReturnToOptionsMenuSound()
    {
        returnToOptionsMenuSound.Play();
//        mysounds.PlayOneShot(returnToOptionsMenuSound);
    }

    //public void SetResolution(int resolutionIndex)
    //{
    //    Resolution resolution = resolutions[resolutionIndex];
    //    Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    //}

    //private void Start()
    //{
    //    resolutions = Screen.resolutions;
    //    resolutionDropdown.ClearOptions();

    //    List<string> options = new List<string>();

    //    for (int i = 0; i < resolutions.Length; i++)
    //    {
    //        option = resolutions[i].width + " x " + resolutions[i].height;
    //        options.Add(option);

    //        if(resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
    //        {
    //            currentResolutionIndex = i;
    //        }
    //    }
    //    resolutionDropdown.AddOptions(options);
    //    resolutionDropdown.value = currentResolutionIndex;
    //    resolutionDropdown.RefreshShownValue();
    //}

    public void NewGameDialogYes()
    {
        SceneManager.LoadScene(_NewGameLevel);
    }


    public void LoadGameDialogYes()
    {

        if(PlayerPrefs.HasKey("Saved_Level"))
        {
            LevelToLoad = PlayerPrefs.GetString("Saved_Level");
            SceneManager.LoadScene(LevelToLoad);

        }
        else
        {
            NoSavedGameDialog.SetActive(true);
        }
    }

    public void QuitButton()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    public void SetBrightness(float brighness)
    {
        _brightnessLevel = brighness;
        brightnessTextValue.text = brighness.ToString("0.0");
    }

    public void SetFullscreen(bool isFullscreen)
    {
        _isFullscreen = isFullscreen;
    }

    public void SetQuality(int qualityIndex)
    {
        _qualityLevel = qualityIndex;
    }

    public void GraphicsApply()
    {
        PlayerPrefs.SetFloat("masterBrightness", _brightnessLevel);
        PlayerPrefs.SetInt("masterQuality", _qualityLevel);
        PlayerPrefs.SetInt("masterFullscreen", (_isFullscreen ? 1 : 0));
        QualitySettings.SetQualityLevel(_qualityLevel);
        Screen.fullScreen = _isFullscreen;

        StartCoroutine(ConformationBox());

    }

    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
        VolumeTextValue.text = volume.ToString("0.0");
    }

    public void VolumeApply()
    {
        PlayerPrefs.SetFloat("masterVolume", AudioListener.volume);
        confirmationPrompt.SetActive(false);
        StartCoroutine(ConformationBox());
    }

    public void SetControllerSensitivity(float sensitivity)
    {
        mainControllerSensitivity = Mathf.RoundToInt(sensitivity);
        controllerSensitivityTextValue.text = sensitivity.ToString("0");
    }

    public void GameplayApply()
    {
        if(invertYToggle.isOn)
        {
            PlayerPrefs.SetInt("masterInvertY", 1);
        }
        else
        {
            PlayerPrefs.SetInt("masterInvertY", 0);
        }

        PlayerPrefs.SetFloat("masterSensitivity", mainControllerSensitivity);
        StartCoroutine(ConformationBox());
    }

    public void ResetButton(string menuType)
    {
        if (menuType == "Audio")
        {
            AudioListener.volume = defaultVolume;
            volumeSlider.value = defaultVolume;
            VolumeTextValue.text = defaultVolume.ToString("0.0");
            VolumeApply();
        }

        if (menuType == "Gameplay")
        {
            controllerSensitivityTextValue.text = defaultSensitivity.ToString("0");
            controllerSensitivitySlider.value = defaultSensitivity;
            mainControllerSensitivity = defaultSensitivity;
            GameplayApply();
        }

        if(menuType == "Graphics")
        {
            brightnessSlider.value = defaultBrightness;
            brightnessTextValue.text = defaultBrightness.ToString("0.0");

            fullScreenToggle.isOn = false;
            Screen.fullScreen = false;
            //Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, Screen.fullScreen);
            //resolutionDropdown.value = resolutions.Length;
            GraphicsApply();
        }
    }

    public IEnumerator ConformationBox()
    {
        confirmationPrompt.SetActive(true);
        yield return new WaitForSeconds(2);
        confirmationPrompt.SetActive(false);
    }
}
