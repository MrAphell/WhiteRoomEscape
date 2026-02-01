using UnityEngine;

public class GameExit : MonoBehaviour
{
    public void QuitGame()
    {
        Debug.Log("A játék bezárul...");

        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}