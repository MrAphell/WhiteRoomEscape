using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.Events;

// A memória játék (Simon Says) központi logikáját kezelõ osztály
public class SimonManager : MonoBehaviour
{
    [Header("Komponensek")]
    public MemoryButton[] buttons;    // A játékban résztvevõ színes gombok listája
    public TextMeshProUGUI statusText; // UI szöveg a játékos tájékoztatására

    [Header("Játék Beállítások")]
    public int roundsToWin = 5; // Hány sikeres kör kell a gyõzelemhez

    [Header("Sebesség (Nehézség)")]
    public float startDelay = 1.0f;   // Kezdeti várakozás a villanások között
    public float minDelay = 0.2f;     // A leggyorsabb lehetséges villanási sebesség
    public float speedUpStep = 0.15f; // Körönkénti gyorsulás mértéke

    [Header("Események (Win)")]
    public UnityEvent OnGameWin; // Esemény, ami gyõzelemkor fut le (pl. ajtónyitás)

    // Belsõ változók a sorozatok és állapotok követéséhez
    private List<int> _correctSequence = new List<int>(); // A gép által generált helyes sorrend
    private List<int> _playerInput = new List<int>();     // A játékos aktuális próbálkozása
    private bool _isPlayerTurn = false;                   // Igaz, ha a játékos jön
    private bool _isGameRunning = false;                  // Igaz, ha fut a játék

    private void Start()
    {
        if (statusText) statusText.text = "Press start button!";
    }

    // Játék indítása (külsõ gomb hívja meg)
    public void StartTheGame()
    {
        if (_isGameRunning) return;

        _isGameRunning = true;
        StartCoroutine(StartGameRoutine());
    }

    // Kezdeti inicializálás és listák ürítése
    IEnumerator StartGameRoutine()
    {
        if (statusText) statusText.text = "The game is starting...";
        yield return new WaitForSeconds(1f);

        _correctSequence.Clear();
        _playerInput.Clear();
        StartRound();
    }

    // Új kör indítása: egy új véletlen elemet adunk a sorozathoz
    void StartRound()
    {
        _isPlayerTurn = false;
        _playerInput.Clear();

        int randomButtonID = Random.Range(0, buttons.Length);
        _correctSequence.Add(randomButtonID);

        if (statusText) statusText.text = "Listen! (" + _correctSequence.Count + ". turn)";

        StartCoroutine(PlaySequence());
    }

    // A gép "elmutatja" a sorozatot a gombok villogtatásával
    IEnumerator PlaySequence()
    {
        yield return new WaitForSeconds(1f);

        // Sebesség kiszámítása az aktuális kör alapján (folyamatos gyorsítás)
        float currentDelay = startDelay - (_correctSequence.Count * speedUpStep);
        currentDelay = Mathf.Max(currentDelay, minDelay);

        foreach (int buttonID in _correctSequence)
        {
            buttons[buttonID].ComputerActivate(); // Gomb felvillantása
            yield return new WaitForSeconds(currentDelay);
        }

        _isPlayerTurn = true;
        if (statusText) statusText.text = "It's your turn!";
    }

    // A játékos gombnyomásainak feldolgozása
    public void PlayerPressedButton(int id)
    {
        if (!_isPlayerTurn) return;

        _playerInput.Add(id);
        int currentIndex = _playerInput.Count - 1;

        // Ellenõrizzük, hogy az utolsó gombnyomás helyes-e
        if (_playerInput[currentIndex] == _correctSequence[currentIndex])
        {
            // Ha a teljes sorozatot hibátlanul beütötte
            if (_playerInput.Count == _correctSequence.Count)
            {
                _isPlayerTurn = false;

                if (_correctSequence.Count >= roundsToWin)
                {
                    WinGame(); // Elértük a célkört
                }
                else
                {
                    StartCoroutine(NextRoundDelay()); // Jöhet a következõ kör
                }
            }
        }
        else
        {
            GameOver(); // Rossz gombot nyomott
        }
    }

    IEnumerator NextRoundDelay()
    {
        if (statusText) statusText.text = "Correct!";
        yield return new WaitForSeconds(1.5f);
        StartRound();
    }

    // Hiba esetén alaphelyzetbe állítjuk a játékot
    void GameOver()
    {
        _isPlayerTurn = false;
        _isGameRunning = false;

        if (statusText) statusText.text = "Fault! Press Start to Try again...";
        Debug.Log("Game Over");
    }

    // Siker esetén leállítjuk a játékot és aktiváljuk a külsõ eseményeket
    void WinGame()
    {
        _isPlayerTurn = false;
        _isGameRunning = false;

        if (statusText) statusText.text = "Door is open!";
        Debug.Log("Level Complete");

        if (OnGameWin != null)
        {
            OnGameWin.Invoke();
        }
    }
}