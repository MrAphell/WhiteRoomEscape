using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class ScoreManagerTests
{
    private GameObject _scoreManagerObject;
    private ScoreManager _scoreManager;
    private string _savePath;

    private const string TestProfile = "TestProfile_Score";

    [SetUp]
    public void Setup()
    {
        PlayerPrefs.SetString("ActiveProfileName", TestProfile);
        PlayerPrefs.Save();

        _savePath = Path.Combine(Application.persistentDataPath, "scoreboard_v2_test.json");
        if (File.Exists(_savePath))
            File.Delete(_savePath);

        ScoreManager.Instance = null;

        _scoreManagerObject = new GameObject("ScoreManager_TestObject");
        _scoreManager = _scoreManagerObject.AddComponent<ScoreManager>();

        // EditMode-ban kézzel inicializáljuk a privát mezõket,
        // hogy ne az Awake/LoadScores lifecycle-re támaszkodjunk.
        TestReflectionHelper.SetPrivateField(_scoreManager, "_database", new ScoreDatabase());
        TestReflectionHelper.SetPrivateField(_scoreManager, "_saveFilePath", _savePath);
    }

    [TearDown]
    public void TearDown()
    {
        ScoreManager.Instance = null;

        if (_scoreManagerObject != null)
            Object.DestroyImmediate(_scoreManagerObject);

        if (File.Exists(_savePath))
            File.Delete(_savePath);
    }

    [Test]
    public void GetTopScores_WhenNoScoresExist_ReturnsEmptyList()
    {
        List<ScoreEntry> scores = _scoreManager.GetTopScores("Game_1");

        Assert.IsNotNull(scores, "A visszatérési érték nem lehet null.");
        Assert.AreEqual(0, scores.Count,
            "Ha még nincs mentett eredmény, üres listát kell visszaadnia.");
    }

    [Test]
    public void AddScore_AddsNewScoreToLevel()
    {
        _scoreManager.AddScore("Game_1", 42.5f);

        List<ScoreEntry> scores = _scoreManager.GetTopScores("Game_1");

        Assert.AreEqual(1, scores.Count,
            "Az elsõ eredmény hozzáadása után pontosan 1 score-nak kell lennie.");
        Assert.AreEqual(42.5f, scores[0].time,
            "A mentett idõnek meg kell egyeznie a hozzáadott idõvel.");
    }

    [Test]
    public void AddScore_SortsScoresInAscendingOrder()
    {
        _scoreManager.AddScore("Game_1", 30f);
        _scoreManager.AddScore("Game_1", 10f);
        _scoreManager.AddScore("Game_1", 20f);

        List<ScoreEntry> scores = _scoreManager.GetTopScores("Game_1");

        Assert.AreEqual(3, scores.Count,
            "Három eredmény hozzáadása után három score-nak kell lennie.");
        Assert.AreEqual(10f, scores[0].time,
            "A legjobb idõnek kell elsõ helyen lennie.");
        Assert.AreEqual(20f, scores[1].time,
            "A második legjobb idõnek kell második helyen lennie.");
        Assert.AreEqual(30f, scores[2].time,
            "A legrosszabb idõnek kell harmadik helyen lennie.");
    }

    [Test]
    public void AddScore_WhenMoreThanThreeScoresExist_KeepsOnlyTopThree()
    {
        _scoreManager.AddScore("Game_1", 40f);
        _scoreManager.AddScore("Game_1", 10f);
        _scoreManager.AddScore("Game_1", 30f);
        _scoreManager.AddScore("Game_1", 20f);

        List<ScoreEntry> scores = _scoreManager.GetTopScores("Game_1");

        Assert.AreEqual(3, scores.Count,
            "A ScoreManager csak a top 3 eredményt tarthatja meg.");
        Assert.AreEqual(10f, scores[0].time,
            "Az elsõ helyen a legjobb idõnek kell lennie.");
        Assert.AreEqual(20f, scores[1].time,
            "A második helyen a második legjobb idõnek kell lennie.");
        Assert.AreEqual(30f, scores[2].time,
            "A harmadik helyen a harmadik legjobb idõnek kell lennie.");
    }

    [Test]
    public void AddScore_UsesActiveProfileNameAsPlayerName()
    {
        _scoreManager.AddScore("Game_2", 15f);

        List<ScoreEntry> scores = _scoreManager.GetTopScores("Game_2");

        Assert.AreEqual(1, scores.Count,
            "Az eredménynek be kellett kerülnie a listába.");
        Assert.AreEqual(TestProfile, scores[0].playerName,
            "A mentett playerName mezõnek az aktív profil nevét kell tartalmaznia.");
    }
}