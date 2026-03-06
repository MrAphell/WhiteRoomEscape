using UnityEngine;
using System.Collections;

public class MemoryButton : MonoBehaviour, IInteractable
{
    [Header("Beállítások")]
    public int buttonID;              // A gomb sorszáma a sorrend ellenõrzéséhez
    public Color normalColor;         // Ezt látjuk, ha nincs megnyomva
    public Color flashColor;          // Ezt látjuk, amikor felvillan
    public float flashDuration = 0.5f; // Villanás hossza másodpercben

    [Header("Referencia")]
    public SimonManager gameManager;  // A játék fõ agya, ide küldjük a kattintást

    private Renderer _rend;           // A gomb anyaga, ezen váltjuk a színeket
    private bool _isFlashing = false; // Jelzi, ha épp villog a gomb (hogy ne villogjon duplán)

    private void Start()
    {
        _rend = GetComponent<Renderer>();
        // Alaphelyzetbe állítjuk a színt indításkor
        _rend.material.color = normalColor;
    }

    // Az interfész kötelezõ része: ezt hívja meg a játékos kattintása
    public void Interact() => OnInteract();

    // Nem írunk ki szöveget a gombra, mert zavaró lenne a gyors játék közben
    public string GetPrompt() => "";

    // Játékos általi aktiválás
    public void OnInteract()
    {
        if (_isFlashing) return;

        // Szólunk a Managernek, hogy ezt a gombot nyomták meg
        gameManager.PlayerPressedButton(buttonID);
        StartCoroutine(FlashRoutine());
    }

    // Amikor a gép mutatja meg a sorrendet, ezt hívja meg
    public void ComputerActivate()
    {
        StartCoroutine(FlashRoutine());
    }

    // A villogtatás folyamata: színváltás, vár, majd visszaállás
    private IEnumerator FlashRoutine()
    {
        _isFlashing = true;
        _rend.material.color = flashColor;

        yield return new WaitForSeconds(flashDuration);

        _rend.material.color = normalColor;
        _isFlashing = false;
    }
}