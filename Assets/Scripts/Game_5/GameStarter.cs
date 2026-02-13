using UnityEngine;

public class GameStarter : MonoBehaviour
{
    public SimonManager simonManager;

    public void Interact()
    {
        if (simonManager != null)
        {
            simonManager.StartTheGame();
        }
    }
}