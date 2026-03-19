using NUnit.Framework;
using UnityEngine;

public class PauseManagerTests
{
    private GameObject _pauseManagerObject;
    private PauseManager _pauseManager;
    private GameObject _pausePanel;

    [SetUp]
    public void Setup()
    {
        PauseManager.Instance = null;
        PauseManager.IsPaused = false;
        Time.timeScale = 1f;

        _pauseManagerObject = new GameObject("PauseManager_TestObject");
        _pauseManager = _pauseManagerObject.AddComponent<PauseManager>();

        _pausePanel = new GameObject("PausePanel");
        _pausePanel.SetActive(false);

        _pauseManager.pausePanel = _pausePanel;
    }

    [TearDown]
    public void TearDown()
    {
        Time.timeScale = 1f;
        PauseManager.IsPaused = false;
        PauseManager.Instance = null;

        if (_pausePanel != null)
            Object.DestroyImmediate(_pausePanel);

        if (_pauseManagerObject != null)
            Object.DestroyImmediate(_pauseManagerObject);
    }

    [Test]
    public void PauseGame_ActivatesPausePanel()
    {
        _pauseManager.PauseGame();

        Assert.IsTrue(_pausePanel.activeSelf,
            "PauseGame() után a pause panelnek aktívnak kell lennie.");
    }

    [Test]
    public void PauseGame_SetsTimeScaleToZero()
    {
        _pauseManager.PauseGame();

        Assert.AreEqual(0f, Time.timeScale,
            "PauseGame() után a Time.timeScale értékének 0-nak kell lennie.");
    }

    [Test]
    public void PauseGame_SetsIsPausedToTrue()
    {
        _pauseManager.PauseGame();

        Assert.IsTrue(PauseManager.IsPaused,
            "PauseGame() után az IsPaused értékének true-nak kell lennie.");
    }

    [Test]
    public void ResumeGame_DeactivatesPausePanel()
    {
        _pausePanel.SetActive(true);

        _pauseManager.ResumeGame();

        Assert.IsFalse(_pausePanel.activeSelf,
            "ResumeGame() után a pause panelnek inaktívnak kell lennie.");
    }

    [Test]
    public void ResumeGame_SetsTimeScaleToOne()
    {
        Time.timeScale = 0f;
        PauseManager.IsPaused = true;

        _pauseManager.ResumeGame();

        Assert.AreEqual(1f, Time.timeScale,
            "ResumeGame() után a Time.timeScale értékének 1-nek kell lennie.");
    }

    [Test]
    public void ResumeGame_SetsIsPausedToFalse()
    {
        PauseManager.IsPaused = true;

        _pauseManager.ResumeGame();

        Assert.IsFalse(PauseManager.IsPaused,
            "ResumeGame() után az IsPaused értékének false-nak kell lennie.");
    }
}