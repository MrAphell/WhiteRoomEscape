using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Linq;

public class MainMenuController : MonoBehaviour
{
    [Header("Panelek")]
    public GameObject mainMenuPanel;
    public GameObject settingsPanel;
    public GameObject profilesPanel;
    public GameObject scoreboardPanel;

    [Header("Fõmenü UI")]
    public TextMeshProUGUI welcomeText;

    [Header("Profil UI Elemek (A Panel_Profiles-ról)")]
    public TMP_Dropdown profileDropdown;
    public TMP_InputField newProfileInput;

    [Header("Settings UI Elemek")]
    public Slider volumeSlider;
    public Slider sensSlider;

    private List<string> profileNames = new List<string>();
    private string currentProfile = "Default";
    private const string PROFILES_KEY = "AllProfiles";
    private const string LAST_PROFILE_KEY = "LastSelectedProfile";

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        LoadProfiles();

        if (volumeSlider != null) volumeSlider.value = PlayerPrefs.GetFloat("MasterVolume", 1.0f);
        if (sensSlider != null) sensSlider.value = PlayerPrefs.GetFloat("MouseSens", 2.0f);

        UpdateProfileUI();
    }


    public void ShowMainMenu()
    {
        mainMenuPanel.SetActive(true);
        settingsPanel.SetActive(false);
        profilesPanel.SetActive(false);
        if (scoreboardPanel != null) scoreboardPanel.SetActive(false);
    }

    public void OpenSettings()
    {
        mainMenuPanel.SetActive(false);
        settingsPanel.SetActive(true);
        profilesPanel.SetActive(false);
    }

    public void OpenProfiles()
    {
        mainMenuPanel.SetActive(false);
        settingsPanel.SetActive(false);
        profilesPanel.SetActive(true);
    }

    public void StartGame()
    {
        PlayerPrefs.SetString("CurrentPlayerName", currentProfile);
        SceneManager.LoadScene("MainHub");
    }

    public void QuitGame()
    {
        Debug.Log("A játék bezárul...");

        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    void LoadProfiles()
    {
        string savedString = PlayerPrefs.GetString(PROFILES_KEY, "Default");
        profileNames = savedString.Split(',').ToList();
        currentProfile = PlayerPrefs.GetString(LAST_PROFILE_KEY, "Default");

        if (!profileNames.Contains(currentProfile)) currentProfile = "Default";
    }

    void SaveProfiles()
    {
        string dataToSave = string.Join(",", profileNames);
        PlayerPrefs.SetString(PROFILES_KEY, dataToSave);
        PlayerPrefs.SetString(LAST_PROFILE_KEY, currentProfile);
        PlayerPrefs.Save();
    }

    public void CreateNewProfile()
    {
        string newName = newProfileInput.text.Trim();
        if (string.IsNullOrEmpty(newName) || profileNames.Contains(newName)) return;

        profileNames.Add(newName);
        currentProfile = newName;
        newProfileInput.text = "";
        SaveProfiles();
        UpdateProfileUI();
    }

    public void DeleteCurrentProfile()
    {
        if (currentProfile == "Default") return;

        profileNames.Remove(currentProfile);
        currentProfile = "Default";
        SaveProfiles();
        UpdateProfileUI();
    }

    public void OnProfileSelected(int index)
    {
        currentProfile = profileNames[index];
        SaveProfiles();
        UpdateWelcomeMessage();
    }

    void UpdateProfileUI()
    {
        if (profileDropdown != null)
        {
            profileDropdown.ClearOptions();
            profileDropdown.AddOptions(profileNames);
            profileDropdown.value = profileNames.IndexOf(currentProfile);
            profileDropdown.RefreshShownValue();
        }
        UpdateWelcomeMessage();
    }

    void UpdateWelcomeMessage()
    {
        if (welcomeText != null) welcomeText.text = "Welcome: " + currentProfile;
    }

    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
        PlayerPrefs.SetFloat("MasterVolume", volume);
    }
    public void SetSensitivity(float sens) { PlayerPrefs.SetFloat("MouseSens", sens); }

    public void OpenScoreboard()
    {
        mainMenuPanel.SetActive(false);
        settingsPanel.SetActive(false);
        profilesPanel.SetActive(false);

        scoreboardPanel.SetActive(true);
    }

    public void ResetProgressButton()
    {
        // Meghívjuk a GameManager törlõjét
        GameManager.ResetCurrentPlayerData();
        Debug.Log("Haladás törölve!");
    }
}