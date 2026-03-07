using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEntrance : MonoBehaviour, IInteractable
{
    [Header("Beállítások")]
    public string sceneToLoad; // Hova vigyen az ajtó? (Kijárat esetén ez a "MainHub" lesz)

    [Header("Kijárat Mód")]
    [Tooltip("Pipáld be, ha ez az ajtó a pálya VÉGÉN van, és menteni kell az idõt!")]
    public bool isLevelExit = false;

    public void Interact()
    {
        EnterLevel();
    }

    public string GetPrompt()
    {
        // Ha kijárat, mást írjon ki a képernyõre a kurzor!
        if (isLevelExit) return "Return to Hub";

        return $"Enter to {sceneToLoad}";
    }

    private void EnterLevel()
    {
        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            string currentSceneName = SceneManager.GetActiveScene().name;

            // HA EZ EGY KIJÁRAT A PÁLYA VÉGÉN:
            // Beküldjük a sikeres idõt a GameManager-nek, mielõtt kilépnénk!
            if (isLevelExit)
            {
                int levelNumber = GameManager.GetLevelNumberFromScene(currentSceneName);
                GameManager.CompleteLevel(levelNumber, currentSceneName);
                Debug.Log($"Pálya teljesítve! Adatok beküldve: {currentSceneName}.");
            }

            // Elmentjük az aktuális pályát: Ez kell a Hub-ba való visszatéréshez, 
            // hogy tudjuk, melyik ajtó elé tegyük vissza a karaktert.
            PlayerPrefs.SetString("LastScene", currentSceneName);
            PlayerPrefs.Save();

            // Scene betöltése
            SceneManager.LoadScene(sceneToLoad);
        }
        else
        {
            Debug.LogError("Nincs beállítva Scene név ezen az ajtón: " + gameObject.name);
        }
    }
}