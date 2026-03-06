using UnityEngine;
using UnityEngine.SceneManagement;

// A játékmenet mentéseit, az életeket és a szintek feloldását kezelõ központi osztály
public class GameManager : MonoBehaviour
{
    public static int MaxLives = 3;

    // Mentési kulcsok a PlayerPrefs rendszerhez
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

    // Szint befejezésekor hívódik meg: kezeli a rekordidõt és a haladást
    public static void CompleteLevel(int levelCompleted)
    {
        float timeSpent = Time.timeSinceLevelLoad;

        string currentPlayer = PlayerPrefs.GetString("CurrentPlayerName", "Default");
        string timeKey = currentPlayer + "_TotalTime";

        float previousBestTime = PlayerPrefs.GetFloat(timeKey, 0);

        // Csak akkor mentünk új idõt, ha az jobb a réginél
        if (previousBestTime == 0 || timeSpent < previousBestTime)
        {
            PlayerPrefs.SetFloat(timeKey, timeSpent);
            PlayerPrefs.Save();
        }

        // Következõ szint feloldása, ha az aktuálisat elõször vittük ki
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

    // Csak az aktív játékos profiljának nullázása
    public static void ResetCurrentPlayerData()
    {
        string currentPlayer = PlayerPrefs.GetString("CurrentPlayerName", "Default");

        PlayerPrefs.DeleteKey(currentPlayer + "_TotalTime");
        PlayerPrefs.SetInt(LevelKey, 1);
        PlayerPrefs.DeleteKey(LivesKey);

        PlayerPrefs.Save();
    }
}