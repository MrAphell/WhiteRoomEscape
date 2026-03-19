using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class PauseManagerPlayModeTests
{
    private GameObject pauseObject;
    private PauseManager pauseManager;
    private GameObject pausePanel;

    [UnitySetUp]
    public IEnumerator Setup()
    {
        PauseManager.Instance = null;
        PauseManager.IsPaused = false;
        Time.timeScale = 1f;

        pauseObject = new GameObject("PauseManager");
        pauseManager = pauseObject.AddComponent<PauseManager>();

        pausePanel = new GameObject("PausePanel");
        pausePanel.SetActive(false);

        pauseManager.pausePanel = pausePanel;

        yield return null;
    }

    [UnityTearDown]
    public IEnumerator TearDown()
    {
        Object.Destroy(pauseObject);
        Object.Destroy(pausePanel);

        PauseManager.Instance = null;
        PauseManager.IsPaused = false;
        Time.timeScale = 1f;

        yield return null;
    }

    [UnityTest]
    public IEnumerator PauseGame_StopsTimeAndShowsPanel()
    {
        pauseManager.PauseGame();
        yield return null;

        Assert.AreEqual(0f, Time.timeScale, "Az idõnek (Time.timeScale) 0-ra kellett volna állnia.");
        Assert.IsTrue(PauseManager.IsPaused, "A statikus IsPaused változónak igaznak kell lennie.");
        Assert.IsTrue(pausePanel.activeSelf, "A Pause panelnek meg kellett volna jelennie.");
    }

    [UnityTest]
    public IEnumerator ResumeGame_RestoresTimeAndHidesPanel()
    {
        pauseManager.PauseGame();
        yield return null;

        pauseManager.ResumeGame();
        yield return null;

        Assert.AreEqual(1f, Time.timeScale, "Az idõnek (Time.timeScale) vissza kellett volna állnia 1-re.");
        Assert.IsFalse(PauseManager.IsPaused, "A statikus IsPaused változónak hamisnak kell lennie.");
        Assert.IsFalse(pausePanel.activeSelf, "A Pause panelnek el kellett volna tûnnie.");
    }
}