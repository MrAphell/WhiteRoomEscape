using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.Events;

public class SimonManager : MonoBehaviour
{
    [Header("Komponensek")]
    public MemoryButton[] buttons;
    public TextMeshProUGUI statusText;

    [Header("Játék Beállítások")]
    public int roundsToWin = 5;

    [Header("Sebesség (Nehézség)")]
    public float startDelay = 1.0f;   // Kezdõ sebesség
    public float minDelay = 0.2f;     // Maximális sebesség (nem megy ez alá)
    public float speedUpStep = 0.15f; // Ennyit gyorsul körönként

    [Header("Események (Win)")]
    public UnityEvent OnGameWin;

    // Privát változók
    private List<int> _correctSequence = new List<int>();
    private List<int> _playerInput = new List<int>();
    private bool _isPlayerTurn = false;
    private bool _isGameRunning = false;

    private void Start()
    {
        if (statusText) statusText.text = "Press start button!";
    }

    public void StartTheGame()
    {
        if (_isGameRunning) return;

        _isGameRunning = true;
        StartCoroutine(StartGameRoutine());
    }

    IEnumerator StartGameRoutine()
    {
        if (statusText) statusText.text = "The game is starting...";
        yield return new WaitForSeconds(1f);

        _correctSequence.Clear();
        _playerInput.Clear();
        StartRound();
    }

    void StartRound()
    {
        _isPlayerTurn = false;
        _playerInput.Clear();

        int randomButtonID = Random.Range(0, buttons.Length);
        _correctSequence.Add(randomButtonID);

        if (statusText) statusText.text = "Listen! (" + _correctSequence.Count + ". turn)";

        StartCoroutine(PlaySequence());
    }

    IEnumerator PlaySequence()
    {
        yield return new WaitForSeconds(1f);

        float currentDelay = startDelay - (_correctSequence.Count * speedUpStep);
        currentDelay = Mathf.Max(currentDelay, minDelay);

        foreach (int buttonID in _correctSequence)
        {
            buttons[buttonID].ComputerActivate();
            yield return new WaitForSeconds(currentDelay);
        }

        _isPlayerTurn = true;
        if (statusText) statusText.text = "It's your turn!";
    }

    public void PlayerPressedButton(int id)
    {
        if (!_isPlayerTurn) return;

        _playerInput.Add(id);

        int currentIndex = _playerInput.Count - 1;

        if (_playerInput[currentIndex] == _correctSequence[currentIndex])
        {
            if (_playerInput.Count == _correctSequence.Count)
            {
                _isPlayerTurn = false;

                if (_correctSequence.Count >= roundsToWin)
                {
                    WinGame();
                }
                else
                {
                    StartCoroutine(NextRoundDelay());
                }
            }
        }
        else
        {
            GameOver();
        }
    }

    IEnumerator NextRoundDelay()
    {
        if (statusText) statusText.text = "Correct!";
        yield return new WaitForSeconds(1.5f);
        StartRound();
    }

    void GameOver()
    {
        _isPlayerTurn = false;
        _isGameRunning = false;

        if (statusText) statusText.text = "Fault! Press Start to Try again...";
        Debug.Log("Game Over");
    }

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