using UnityEngine;
using UnityEngine.SceneManagement;

// A játékmenet mentéseit, az életeket és a szintek feloldását kezelõ központi osztály
public class GameManager : MonoBehaviour
{
    public static int MaxLives = 3;

    // Mentési kulcsok a PlayerPrefs rendszerhez (csak a haladáshoz és az élethez)
    private static string LevelKey = "LevelsUnlocked";
    private static string LivesKey = "CurrentLives";

    // Visszaadja a legmagasabb feloldott szint számát
    public static int GetUnlockedLevel()
    {
        return PlayerPrefs.GetInt(LevelKey, 1);
    }

    // Segédfüggvény: kinyeri a számot a jelenet nevébõl
    public static int GetLevelNumberFromScene(string sceneName)
    {
        string numberPart = System.Text.RegularExpressions.Regex.Match(sceneName, @"\d+").Value;
        return string.IsNullOrEmpty(numberPart) ? 1 : int.Parse(numberPart);
    }

    // Szint befejezésekor hívódik meg: beküldi a rekordidõt a JSON-be és kezeli a haladást
    // ÚJÍTÁS: Most már kéri a pontos idõt és a pálya nevét paraméterként!
    // Szint befejezésekor hívódik meg: lekéri a Unity belsõ óráját és beküldi a JSON-be
    public static void CompleteLevel(int levelCompleted, string levelId)
    {
        // 1. IDÕ LEKÉRÉSE A UNITY-TÕL (A jelenet betöltése óta eltelt másodpercek)
        float timeSpent = Time.timeSinceLevelLoad;

        // 2. IDÕ BEKÜLDÉSE AZ ÚJ SCOREMANAGER-NEK
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.AddScore(levelId, timeSpent);
        }
        else
        {
            Debug.LogError("Nem található a ScoreManager a jelenetben! Az idõ nem lett elmentve.");
        }

        // 3. KÖVETKEZÕ SZINT FELOLDÁSA
        int currentProgress = GetUnlockedLevel();
        if (levelCompleted >= currentProgress)
        {
            PlayerPrefs.SetInt(LevelKey, levelCompleted + 1);
            PlayerPrefs.Save();
        }
    }

    // Élet levonása; ha elfogy, alaphelyzetbe állítja a játékot
    public static void LoseLife()
    {
        int current = PlayerPrefs.GetInt(LivesKey, MaxLives);
        current--;

        if (current <= 0)
        {
            ResetGame();
        }
        else
        {
            PlayerPrefs.SetInt(LivesKey, current);
            PlayerPrefs.Save();
        }
    }

    // Teljes mentés törlése és visszatérés a Hub-ba
    public static void ResetGame()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("MainHub");
    }

    // Csak az aktív játékos profiljának (haladásának) nullázása
    public static void ResetCurrentPlayerData()
    {
        // Megjegyzés: A JSON Scoreboard rekordjait ez a gomb szándékosan nem törli, 
        // hiszen a dicsõséglista (Top 3) állandó marad, mint a játéktermi gépeken!
        PlayerPrefs.SetInt(LevelKey, 1);
        PlayerPrefs.DeleteKey(LivesKey);
        PlayerPrefs.Save();
    }
}