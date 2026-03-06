using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEntrance : MonoBehaviour, IInteractable
{
    [Header("Hova vigyen ez az ajtó?")]
    public string sceneToLoad; // A betöltendõ pálya pontos neve az Inspectorban
    public void Interact()
    {
        EnterLevel();
    }

    public string GetPrompt()
    {
        return $"Enter to {sceneToLoad}";
    }

    private void EnterLevel()
    {
        // Ellenõrizzük, hogy elfelejtettük-e megadni a pálya nevét
        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            // Elmentjük az aktuális pályát: Ez kell a Hub-ba való visszatéréshez, 
            // hogy tudjuk, melyik ajtó elé tegyük vissza a karaktert.
            PlayerPrefs.SetString("LastScene", SceneManager.GetActiveScene().name);
            PlayerPrefs.Save();

            // Scene betöltése
            SceneManager.LoadScene(sceneToLoad);
        }
        else
        {
            // Ha üres a mezõ, hibaüzenetet küldünk a fejlesztõnek a konzolra
            Debug.LogError("Nincs beállítva Scene név ezen az ajtón: " + gameObject.name);
        }
    }
}