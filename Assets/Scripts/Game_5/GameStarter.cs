using UnityEngine;

public class GameStarter : MonoBehaviour, IInteractable
{
    public SimonManager simonManager;

    public void Interact()
    {
        if (simonManager != null) simonManager.StartTheGame();
    }

    public string GetPrompt() => "Press [E] to Start Game";
}