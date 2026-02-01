using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Linq;

public class ScoreboardController : MonoBehaviour
{
    [Header("UI Elemek (Az 5 szövegdoboz)")]
    public TextMeshProUGUI[] entrySlots;

    [System.Serializable]
    public class ScoreEntry
    {
        public string playerName;
        public float time;
    }

    private List<ScoreEntry> allScores = new List<ScoreEntry>();

    private void OnEnable()
    {
        LoadAndShowScores();
    }

    public void LoadAndShowScores()
    {
        allScores.Clear();

        string savedProfiles = PlayerPrefs.GetString("AllProfiles", "Default");
        string[] names = savedProfiles.Split(',');

        foreach (string name in names)
        {
            float realTime = PlayerPrefs.GetFloat(name + "_TotalTime", 0);

            if (realTime > 0)
            {
                ScoreEntry entry = new ScoreEntry();
                entry.playerName = name;
                entry.time = realTime;
                allScores.Add(entry);
            }
        }

        allScores = allScores.OrderBy(x => x.time).ToList();

        for (int i = 0; i < entrySlots.Length; i++)
        {
            if (i < allScores.Count)
            {
                ScoreEntry data = allScores[i];
                string formattedTime = FormatTime(data.time);
                entrySlots[i].text = (i + 1) + ". " + data.playerName + " - " + formattedTime;
            }
            else
            {
                entrySlots[i].text = (i + 1) + ". ---";
            }
        }
    }

    string FormatTime(float timeInSeconds)
    {
        int minutes = Mathf.FloorToInt(timeInSeconds / 60);
        int seconds = Mathf.FloorToInt(timeInSeconds % 60);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}