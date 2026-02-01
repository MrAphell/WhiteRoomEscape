using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEntrance : MonoBehaviour
{
    [Header("Hova vigyen ez az ajtó?")]
    public string sceneToLoad;

    public void EnterLevel()
    {
        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            PlayerPrefs.SetString("LastScene", SceneManager.GetActiveScene().name);

            SceneManager.LoadScene(sceneToLoad);
        }
        else
        {
            Debug.LogError("Nincs beállítva Scene név ezen az ajtón: " + gameObject.name);
        }
    }
}