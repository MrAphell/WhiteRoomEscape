using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static int MaxLives = 3;

    private static string LevelKey = "LevelsUnlocked";
    private static string LivesKey = "CurrentLives";

    public static int GetUnlockedLevel()
    {
        return PlayerPrefs.GetInt(LevelKey, 1);
    }

    public static void CompleteLevel(int levelCompleted)
    {
        float timeSpent = Time.timeSinceLevelLoad;
        Debug.Log("Pálya teljesítve ennyi idõ alatt: " + timeSpent);

        string currentPlayer = PlayerPrefs.GetString("CurrentPlayerName", "Default");
        string timeKey = currentPlayer + "_TotalTime";

        float previousBestTime = PlayerPrefs.GetFloat(timeKey, 0);

        if (previousBestTime == 0 || timeSpent < previousBestTime)
        {
            PlayerPrefs.SetFloat(timeKey, timeSpent);
            PlayerPrefs.Save();
            Debug.Log("Új rekord: " + timeSpent);
        }

        int currentProgress = GetUnlockedLevel();

        if (levelCompleted >= currentProgress)
        {
            PlayerPrefs.SetInt(LevelKey, levelCompleted + 1);
            PlayerPrefs.Save();
        }
    }

    public static void LoseLife()
    {
        int current = PlayerPrefs.GetInt(LivesKey, MaxLives);
        current--;

        if (current <= 0)
        {
            Debug.Log("GAME OVER");
            ResetGame();
        }
        else
        {
            PlayerPrefs.SetInt(LivesKey, current);
            PlayerPrefs.Save();
        }
    }

    public static void ResetGame()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("MainHub");
    }

    public static void ResetCurrentPlayerData()
    {
        string currentPlayer = PlayerPrefs.GetString("CurrentPlayerName", "Default");

        Debug.Log("Adatok törlése ehhez a profilhoz: " + currentPlayer);

        PlayerPrefs.DeleteKey(currentPlayer + "_TotalTime");

        PlayerPrefs.SetInt(LevelKey, 1);

        PlayerPrefs.DeleteKey(LivesKey);

        PlayerPrefs.Save();
        
        Debug.Log("Sikeres törlés!");
    }
}