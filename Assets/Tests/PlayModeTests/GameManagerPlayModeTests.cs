using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class GameManagerPlayModeTests
{
    const string profile = "TestProfile";
    private string originalProfile;

    [UnitySetUp]
    public IEnumerator Setup()
    {
        originalProfile = PlayerPrefs.GetString("ActiveProfileName", "Default");

        PlayerPrefs.SetString("ActiveProfileName", profile);

        PlayerPrefs.SetInt(profile + "_CurrentLives", 3);
        PlayerPrefs.Save();

        yield return null;
    }

    [UnityTearDown]
    public IEnumerator TearDown()
    {
        PlayerPrefs.DeleteKey(profile + "_CurrentLives");
        PlayerPrefs.DeleteKey(profile + "_LevelsUnlocked");

        PlayerPrefs.SetString("ActiveProfileName", originalProfile);
        PlayerPrefs.Save();

        yield return null;
    }

    [UnityTest]
    public IEnumerator LoseLife_DecreasesLife()
    {
        GameManager.LoseLife();
        yield return null;

        int lives = PlayerPrefs.GetInt(profile + "_CurrentLives");
        Assert.AreEqual(2, lives);
    }

    [UnityTest]
    public IEnumerator LoseLife_WhenZero_ResetsData()
    {
        PlayerPrefs.SetInt(profile + "_CurrentLives", 1);

        GameManager.LoseLife();
        yield return null;

        bool hasLives = PlayerPrefs.HasKey(profile + "_CurrentLives");
        Assert.IsFalse(hasLives);
    }
}