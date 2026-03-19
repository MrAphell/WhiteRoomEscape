using NUnit.Framework;
using UnityEngine;

public class GameManagerTests
{
    private const string TestProfile = "TestProfile";

    // Minden teszt elõtt lefut
    [SetUp]
    public void Setup()
    {
        PlayerPrefs.SetString("ActiveProfileName", TestProfile);

        // Csak a tesztprofil kulcsait töröljük
        PlayerPrefs.DeleteKey(TestProfile + "_LevelsUnlocked");
        PlayerPrefs.DeleteKey(TestProfile + "_CurrentLives");

        PlayerPrefs.Save();
    }

    [TearDown]
    public void TearDown()
    {
        PlayerPrefs.DeleteKey(TestProfile + "_LevelsUnlocked");
        PlayerPrefs.DeleteKey(TestProfile + "_CurrentLives");

        PlayerPrefs.SetString("ActiveProfileName", "Default");
        PlayerPrefs.Save();
    }

    // 1.
    [Test]
    public void GetLevelNumberFromScene_Game1_Returns1()
    {
        int result = GameManager.GetLevelNumberFromScene("Game_1");

        Assert.AreEqual(1, result,
            "A Game_1 jelenetbõl az 1-es szintet kellett volna kinyerni.");
    }

    // 2.
    [Test]
    public void GetLevelNumberFromScene_Game9_Returns9()
    {
        int result = GameManager.GetLevelNumberFromScene("Game_9");

        Assert.AreEqual(9, result,
            "A Game_9 jelenetbõl a 9-es szintet kellett volna kinyerni.");
    }

    // 3.
    [Test]
    public void GetLevelNumberFromScene_MainHub_Returns1()
    {
        int result = GameManager.GetLevelNumberFromScene("MainHub");

        Assert.AreEqual(1, result,
            "A MainHub esetén az alapértelmezett szintnek 1-nek kell lennie.");
    }

    // 4.
    [Test]
    public void ResetCurrentPlayerData_RemovesLevelsUnlockedKey()
    {
        // elõkészítés
        PlayerPrefs.SetInt(TestProfile + "_LevelsUnlocked", 5);
        PlayerPrefs.Save();

        // mûvelet
        GameManager.ResetCurrentPlayerData();

        // ellenõrzés
        bool hasKey = PlayerPrefs.HasKey(TestProfile + "_LevelsUnlocked");

        Assert.IsFalse(hasKey,
            "A ResetCurrentPlayerData után a LevelsUnlocked kulcsnak törlõdnie kellett volna.");
    }

    // 5.
    [Test]
    public void ResetCurrentPlayerData_RemovesCurrentLivesKey()
    {
        // elõkészítés
        PlayerPrefs.SetInt(TestProfile + "_CurrentLives", 2);
        PlayerPrefs.Save();

        // mûvelet
        GameManager.ResetCurrentPlayerData();

        // ellenõrzés
        bool hasKey = PlayerPrefs.HasKey(TestProfile + "_CurrentLives");

        Assert.IsFalse(hasKey,
            "A ResetCurrentPlayerData után a CurrentLives kulcsnak törlõdnie kellett volna.");
    }

    // 6.
    [Test]
    public void GetUnlockedLevel_WhenNoSaveExists_Returns1()
    {
        // biztosítjuk hogy nincs mentés
        PlayerPrefs.DeleteKey(TestProfile + "_LevelsUnlocked");
        PlayerPrefs.Save();

        int result = GameManager.GetUnlockedLevel();

        Assert.AreEqual(1, result,
            "Ha nincs mentés, az alapértelmezett unlocked level 1 kell legyen.");
    }
}