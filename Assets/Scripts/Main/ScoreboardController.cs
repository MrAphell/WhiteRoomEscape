using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Linq;

// A scoreboard megjelenítéséért és az adatok rangsorolásáért felelõs vezérlõ
public class ScoreboardController : MonoBehaviour
{
    [Header("UI Elemek (Az 5 szövegdoboz)")]
    public TextMeshProUGUI[] entrySlots; // A listában szereplõ sorok UI referenciái

    [System.Serializable]
    public class ScoreEntry
    {
        public string playerName; // Játékos neve
        public float time;        // Elért idõeredmény
    }

    // A memóriában tárolt pontszámok listája a rendezéshez
    private List<ScoreEntry> allScores = new List<ScoreEntry>();

    // Amikor a scoreboard megjelenik (aktiválódik a panel), frissítjük az adatokat
    private void OnEnable()
    {
        LoadAndShowScores();
    }

    // Betölti a mentett profilokat, lekéri az idõket és kiírja õket a képernyõre
    public void LoadAndShowScores()
    {
        allScores.Clear();

        // Profilnevek lekérése a mentésbõl, vesszõvel elválasztva vannak tárolva
        string savedProfiles = PlayerPrefs.GetString("AllProfiles", "Default");
        string[] names = savedProfiles.Split(',');

        // Végigmegyünk minden néven és kikeressük a hozzájuk tartozó legjobb idõt
        foreach (string name in names)
        {
            float realTime = PlayerPrefs.GetFloat(name + "_TotalTime", 0);

            // Csak azokat adjuk hozzá, akiknek már van érvényes (0-nál nagyobb) idejük
            if (realTime > 0)
            {
                ScoreEntry entry = new ScoreEntry();
                entry.playerName = name;
                entry.time = realTime;
                allScores.Add(entry);
            }
        }

        // Idõ szerint növekvõ sorrendbe rakjuk a listát (a leggyorsabb van elöl)
        allScores = allScores.OrderBy(x => x.time).ToList();

        // Feltöltjük az UI slotokat a rangsorolt adatokkal
        for (int i = 0; i < entrySlots.Length; i++)
        {
            if (i < allScores.Count)
            {
                ScoreEntry data = allScores[i];
                string formattedTime = FormatTime(data.time);
                // Megjelenítés formátuma: "1. Név - 00:00"
                entrySlots[i].text = (i + 1) + ". " + data.playerName + " - " + formattedTime;
            }
            else
            {
                // Ha nincs több adat, üres sort mutatunk
                entrySlots[i].text = (i + 1) + ". ---";
            }
        }
    }

    // Segédfüggvény: a másodperceket perc:másodperc formátumra alakítja
    string FormatTime(float timeInSeconds)
    {
        int minutes = Mathf.FloorToInt(timeInSeconds / 60);
        int seconds = Mathf.FloorToInt(timeInSeconds % 60);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}