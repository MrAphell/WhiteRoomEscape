using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static int MaxLives = 3;

    // Ez a kulcs mondja meg, ki az aktuális játékos (ezt a Profilválasztónál állítjuk be)
    private static string CurrentProfileKey = "ActiveProfileName";

    // Visszaadja az éppen bejelentkezett profil nevét (ha nincs, "Default" lesz)
    public static string GetCurrentProfile()
    {
        return PlayerPrefs.GetString(CurrentProfileKey, "Default");
    }

    // DINAMIKUS KULCSOK: Minden profilnak saját kulcsa lesz (pl. "Rami_LevelsUnlocked")
    private static string GetLevelKey() => GetCurrentProfile() + "_LevelsUnlocked";
    private static string GetLivesKey() => GetCurrentProfile() + "_CurrentLives";

    public static int GetUnlockedLevel()
    {
        // Most már csak a saját profilja szintjét olvassa be
        return PlayerPrefs.GetInt(GetLevelKey(), 1);
    }

    public static int GetLevelNumberFromScene(string sceneName)
    {
        string numberPart = System.Text.RegularExpressions.Regex.Match(sceneName, @"\d+").Value;
        return string.IsNullOrEmpty(numberPart) ? 1 : int.Parse(numberPart);
    }

    public static void CompleteLevel(int levelCompleted, string levelId)
    {
        float timeSpent = Time.timeSinceLevelLoad;

        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.AddScore(levelId, timeSpent);
        }

        int currentProgress = GetUnlockedLevel();
        if (levelCompleted >= currentProgress)
        {
            // Mentés a profil-specifikus kulcsba
            PlayerPrefs.SetInt(GetLevelKey(), levelCompleted + 1);
            PlayerPrefs.Save();
        }
    }

    public static void LoseLife()
    {
        int current = PlayerPrefs.GetInt(GetLivesKey(), MaxLives);
        current--;

        if (current <= 0)
        {
            ResetCurrentPlayerData(); // Csak a saját adatait nullázza le!
            SceneManager.LoadScene("MainHub");
        }
        else
        {
            PlayerPrefs.SetInt(GetLivesKey(), current);
            PlayerPrefs.Save();
        }
    }

    // Ez most már csak az éppen aktív profilt törli ki, nem mindenkit!
    public static void ResetCurrentPlayerData()
    {
        PlayerPrefs.DeleteKey(GetLevelKey());
        PlayerPrefs.DeleteKey(GetLivesKey());
        PlayerPrefs.Save();
    }

    public static void MasterReset()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("MainHub");
    }
}