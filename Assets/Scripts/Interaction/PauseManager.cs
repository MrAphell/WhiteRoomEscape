using UnityEngine;
using UnityEngine.SceneManagement;

// A játék megállításáért, a menü kezeléséért és az idõ (TimeScale) megállításáért felelõs osztály
public class PauseManager : MonoBehaviour
{
    // Singleton minta, hogy a PauseManager minden jeleneten átíveljen
    public static PauseManager Instance;

    [Header("UI Elemek")]
    public GameObject pausePanel; // A szüneteltetéskor megjelenõ menü panelje

    // Globálisan elérhetõ állapotjelzõ, hogy tudjuk, meg van-e állítva a játék
    public static bool IsPaused = false;

    private void Awake()
    {
        // Gondoskodunk róla, hogy csak egyetlen példány létezzen a menedzserbõl
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Jelenetváltáskor nem semmisül meg
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Biztosítjuk, hogy induláskor fusson az idõ
        ResumeGame();
    }

    private void Update()
    {
        // A fõmenüben nem engedélyezzük a szüneteltetést
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            if (pausePanel.activeSelf) pausePanel.SetActive(false);
            return;
        }

        // Escape gombra váltunk a megállítás és a folytatás között
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    // Játék folytatása: UI elrejtése, idõ elindítása és kurzor bezárása
    public void ResumeGame()
    {
        if (pausePanel != null) pausePanel.SetActive(false);

        Time.timeScale = 1f; // Idõ visszaállítása normál sebességre
        IsPaused = false;

        // Csak a játék közben zárjuk le a kurzort, a fõmenüben nem
        if (SceneManager.GetActiveScene().name != "MainMenu")
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    // Játék megállítása: UI megjelenítése, idõ megfagyasztása és kurzor felszabadítása
    public void PauseGame()
    {
        if (pausePanel != null) pausePanel.SetActive(true);

        Time.timeScale = 0f; // Idõ megállítása (fizika, animációk leállnak)
        IsPaused = true;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // Visszatérés a fõmenübe: idõ visszaállítása és a jelenet betöltése
    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        IsPaused = false;

        SceneManager.LoadScene("MainMenu");
    }
}