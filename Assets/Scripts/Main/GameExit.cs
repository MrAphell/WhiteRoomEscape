using UnityEngine;

// Ez a script kezeli a játékból való kilépést, ha a játékos interakcióba lép az objektummal
public class GameExit : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        Debug.Log("Kilépés a játékból...");
        Application.Quit();
    }
    public string GetPrompt() => "Press [E] to Quit Game";
}