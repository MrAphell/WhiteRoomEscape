using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class ScoreboardUI : MonoBehaviour
{
    [Header("Felugró Ablak (Popup) Elemek")]
    public GameObject popupPanel;
    public TextMeshProUGUI titleText;     // A pálya cím
    public TextMeshProUGUI scoresText;    // Lista

    private void OnEnable()
    {
        // Alapból legyen kikapcsolva
        if (popupPanel != null) popupPanel.SetActive(false);
    }

    // Ezt a függvényt fogják hívni a Pálya gombok
    public void ShowScoresForLevel(int levelNumber)
    {
        // Azonosító
        string levelID = "Game_" + levelNumber;
        titleText.text = "Game " + levelNumber + " Records";

        if (ScoreManager.Instance == null)
        {
            scoresText.text = "Error: Results not found!";
            popupPanel.SetActive(true);
            return;
        }

        // Lekérjük a rekordokat
        List<ScoreEntry> scores = ScoreManager.Instance.GetTopScores(levelID);
        string displayText = "";

        // Összeállítjuk a 3 soros listát
        for (int i = 0; i < 3; i++)
        {
            if (i < scores.Count)
            {
                string formattedTime = FormatTime(scores[i].time);
                displayText += $"{i + 1}. {scores[i].playerName} - {formattedTime}\n";
            }
            else
            {
                displayText += $"{i + 1}. ---\n";
            }
        }

        // Szöveg megjelenítése és popup megnyitása
        scoresText.text = displayText;
        popupPanel.SetActive(true);
    }

    // Popup ablak "Bezárás" gombja
    public void ClosePopup()
    {
        if (popupPanel != null) popupPanel.SetActive(false);
    }

    // Másodpercek átalakítása
    private string FormatTime(float timeInSeconds)
    {
        int minutes = Mathf.FloorToInt(timeInSeconds / 60);
        int seconds = Mathf.FloorToInt(timeInSeconds % 60);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}