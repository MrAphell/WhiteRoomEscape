using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Linq;

// A fõmenü logikáját, a profilkezelést és a beállításokat vezérlõ osztály
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

    // Belsõ változók a profilok és mentések kezeléséhez
    private List<string> profileNames = new List<string>();
    private string currentProfile = "Default";
    private const string PROFILES_KEY = "AllProfiles";
    private const string LAST_PROFILE_KEY = "LastSelectedProfile";

    private void Start()
    {
        // Egér kurzor felszabadítása a menühöz
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        LoadProfiles();

        // Csúszkák inicializálása a mentett értékek alapján
        if (volumeSlider != null) volumeSlider.value = PlayerPrefs.GetFloat("MasterVolume", 1.0f);
        if (sensSlider != null) sensSlider.value = PlayerPrefs.GetFloat("MouseSens", 2.0f);

        UpdateProfileUI();
    }

    // Panel váltó függvények

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

    // Játék indítás és kilépés

    public void StartGame()
    {
        // Elmentjük az aktuális profilt, mielõtt belépünk a Hub-ba
        PlayerPrefs.SetString("CurrentPlayerName", currentProfile);
        SceneManager.LoadScene("MainHub");
    }

    public void QuitGame()
    {
        Debug.Log("A játék bezárul...");
        Application.Quit();

        // Editor módban is leállítjuk a futtatást
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    // Profilkezelõ logika

    // Betölti a neveket a PlayerPrefs-bõl
    void LoadProfiles()
    {
        string savedString = PlayerPrefs.GetString(PROFILES_KEY, "Default");
        profileNames = savedString.Split(',').ToList();
        currentProfile = PlayerPrefs.GetString(LAST_PROFILE_KEY, "Default");

        // Biztonsági ellenõrzés: ha a mentett profil törölve lett, visszaállunk Default-ra
        if (!profileNames.Contains(currentProfile)) currentProfile = "Default";
    }

    // Profilok listájának mentése egyetlen vesszõvel elválasztott stringként
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
        // Üres vagy már létezõ nevet nem engedünk
        if (string.IsNullOrEmpty(newName) || profileNames.Contains(newName)) return;

        profileNames.Add(newName);
        currentProfile = newName;
        newProfileInput.text = "";
        SaveProfiles();
        UpdateProfileUI();
    }

    public void DeleteCurrentProfile()
    {
        // A Default profil nem törölhetõ
        if (currentProfile == "Default") return;

        profileNames.Remove(currentProfile);
        currentProfile = "Default";
        SaveProfiles();
        UpdateProfileUI();
    }

    // Dropdown eseménykezelõje profilváltáshoz
    public void OnProfileSelected(int index)
    {
        currentProfile = profileNames[index];
        SaveProfiles();
        UpdateWelcomeMessage();
    }

    // UI elemek (Dropdown, üdvözlõ szöveg) frissítése
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

    // eállítások mentése

    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
        PlayerPrefs.SetFloat("MasterVolume", volume);
    }

    public void SetSensitivity(float sens)
    {
        PlayerPrefs.SetFloat("MouseSens", sens);
    }

    public void OpenScoreboard()
    {
        mainMenuPanel.SetActive(false);
        settingsPanel.SetActive(false);
        profilesPanel.SetActive(false);
        scoreboardPanel.SetActive(true);
    }

    // Aktuális profil játékmenet-adatainak (szint, idõ) törlése a GameManager segítségével
    public void ResetProgressButton()
    {
        GameManager.ResetCurrentPlayerData();
        Debug.Log("Haladás törölve!");
    }
}