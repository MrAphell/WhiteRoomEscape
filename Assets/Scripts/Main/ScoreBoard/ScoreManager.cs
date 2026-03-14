using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Linq;

// 1. A "Név + Idõ" csomag, amit mentünk
[System.Serializable]
public class ScoreEntry
{
    public string playerName;
    public float time;
}

// 2. A Pálya szintû adat: Ez tárolja egy adott pálya Top 3 rekordját
[System.Serializable]
public class LevelRecord
{
    public string levelID;
    public List<ScoreEntry> topScores = new List<ScoreEntry>();

    public LevelRecord(string id)
    {
        levelID = id;
    }
}

// 3. Ezt az osztályt fogjuk JSON-né alakítani
[System.Serializable]
public class ScoreDatabase
{
    public List<LevelRecord> records = new List<LevelRecord>();
}

// 4. Maga a Manager, ami kezeli a fájlokat és a logikát
public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    private ScoreDatabase _database;
    private string _saveFilePath;

    private void Awake()
    {
        // Singleton minta, hogy bárhonnan el lehessen érni
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Ne semmisüljön meg pályaváltáskor
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // Kijelöljük a mentési fájl helyét a gépen (scoreboard_v2.json néven, hogy a régi verzióval ne akadjon össze)
        _saveFilePath = Application.persistentDataPath + "/scoreboard_v2.json";
        LoadScores();
    }

    // JSON betöltése
    private void LoadScores()
    {
        if (File.Exists(_saveFilePath))
        {
            string json = File.ReadAllText(_saveFilePath);
            _database = JsonUtility.FromJson<ScoreDatabase>(json);
            Debug.Log("Scoreboard betöltve innen: " + _saveFilePath);
        }
        else
        {
            // Ha még nincs fájl, létrehozunk egy üreset
            _database = new ScoreDatabase();
            Debug.Log("Új Scoreboard adatbázis létrehozva.");
        }
    }

    // JSON mentése
    private void SaveScores()
    {
        string json = JsonUtility.ToJson(_database, true);
        File.WriteAllText(_saveFilePath, json);
        Debug.Log("Scoreboard elmentve.");
    }

    // Új idõ hozzáadása és a Top 3 menedzselése
    public void AddScore(string levelID, float newTime)
    {
        // Lekérjük az aktív játékos nevét a PlayerPrefs-bõl
        string currentPlayer = PlayerPrefs.GetString("CurrentPlayerName", "Default");

        // 1. Keresés: Megnézzük, van-e már ilyen pálya az adatbázisban
        LevelRecord record = _database.records.Find(r => r.levelID == levelID);

        // 2. Ha még sosem játszották ezt a pályát, létrehozunk egy új listát neki
        if (record == null)
        {
            record = new LevelRecord(levelID);
            _database.records.Add(record);
        }

        // 3. Beszúrás: Hozzáadjuk az új adatot (név + idõ) a listához
        record.topScores.Add(new ScoreEntry { playerName = currentPlayer, time = newTime });

        // 4. Rendezés: Idõ szerint növekvõ sorrendbe rakjuk (leggyorsabb van elöl)
        record.topScores = record.topScores.OrderBy(x => x.time).ToList();

        // 5. Csonkolás: Ha 3-nál több idõ lett a listában, a legrosszabbakat kidobjuk
        if (record.topScores.Count > 3)
        {
            // Eltávolítunk mindent a 3. indextõl kezdve
            record.topScores.RemoveRange(3, record.topScores.Count - 3);
        }

        // 6. Kiírjuk az új állapotot a JSON fájlba
        SaveScores();

        Debug.Log($"Új idõ rögzítve a {levelID} pályán: {newTime} ({currentPlayer}). Jelenlegi csúcstartó: {record.topScores[0].playerName} - {record.topScores[0].time}");
    }

    // Segédfüggvény a UI számára, hogy késõbb ki tudja olvasni az adatokat
    public List<ScoreEntry> GetTopScores(string levelID)
    {
        LevelRecord record = _database.records.Find(r => r.levelID == levelID);

        if (record != null)
        {
            return record.topScores; // Visszaadjuk a mentett rekordokat
        }

        return new List<ScoreEntry>(); // Ha még nincs adat, egy üres listát adunk vissza hiba helyett
    }
}