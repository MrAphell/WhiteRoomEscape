using NUnit.Framework;
using TMPro;
using UnityEngine;

public class KeypadSystemTests
{
    private GameObject _keypadObject;
    private KeypadSystem _keypadSystem;

    private GameObject _displayObject;
    private TextMeshProUGUI _displayText;

    private GameObject _lockedDoor;
    private GameObject _openDoor;
    private GameObject _uiPanel;

    private const string TestProfile = "TestProfile";

    [SetUp]
    public void Setup()
    {
        // Tesztprofil elõkészítése, hogy LoseLife hívásnál is izoláltak maradjunk
        PlayerPrefs.SetString("ActiveProfileName", TestProfile);
        PlayerPrefs.DeleteKey(TestProfile + "_LevelsUnlocked");
        PlayerPrefs.DeleteKey(TestProfile + "_CurrentLives");
        PlayerPrefs.Save();

        // Keypad objektum
        _keypadObject = new GameObject("KeypadSystem_TestObject");
        _keypadSystem = _keypadObject.AddComponent<KeypadSystem>();

        // TMP kijelzõ
        _displayObject = new GameObject("DisplayText");
        _displayText = _displayObject.AddComponent<TextMeshProUGUI>();
        _displayText.text = "";

        // Ajtók
        _lockedDoor = new GameObject("LockedDoor");
        _openDoor = new GameObject("OpenDoor");

        _lockedDoor.SetActive(true);
        _openDoor.SetActive(false);

        // UI panel
        _uiPanel = new GameObject("UIPanel");
        _uiPanel.SetActive(true);

        // Private mezõk beállítása reflectionnel
        TestReflectionHelper.SetPrivateField(_keypadSystem, "_correctCode", "1234");
        TestReflectionHelper.SetPrivateField(_keypadSystem, "_displayText", _displayText);
        TestReflectionHelper.SetPrivateField(_keypadSystem, "_lockedDoor", _lockedDoor);
        TestReflectionHelper.SetPrivateField(_keypadSystem, "_openDoorObject", _openDoor);
        TestReflectionHelper.SetPrivateField(_keypadSystem, "_uiPanel", _uiPanel);
        TestReflectionHelper.SetPrivateField(_keypadSystem, "_currentInput", "");
    }

    [TearDown]
    public void TearDown()
    {
        PlayerPrefs.DeleteKey(TestProfile + "_LevelsUnlocked");
        PlayerPrefs.DeleteKey(TestProfile + "_CurrentLives");
        PlayerPrefs.SetString("ActiveProfileName", "Default");
        PlayerPrefs.Save();

        if (_keypadObject != null) Object.DestroyImmediate(_keypadObject);
        if (_displayObject != null) Object.DestroyImmediate(_displayObject);
        if (_lockedDoor != null) Object.DestroyImmediate(_lockedDoor);
        if (_openDoor != null) Object.DestroyImmediate(_openDoor);
        if (_uiPanel != null) Object.DestroyImmediate(_uiPanel);
    }

    // 7.
    [Test]
    public void AddDigit_AddsNumberToInput()
    {
        _keypadSystem.AddDigit("1");

        string currentInput = TestReflectionHelper.GetPrivateField<string>(_keypadSystem, "_currentInput");

        Assert.AreEqual("1", currentInput,
            "Az AddDigit() hívásnak hozzá kellett volna adnia a számot a bemenethez.");
        Assert.AreEqual("1", _displayText.text,
            "A kijelzõn is meg kellett volna jelenjen az aktuális bemenet.");
    }

    // 8.
    [Test]
    public void AddDigit_DoesNotAllowMoreThanFourCharacters()
    {
        _keypadSystem.AddDigit("1");
        _keypadSystem.AddDigit("2");
        _keypadSystem.AddDigit("3");
        _keypadSystem.AddDigit("4");
        _keypadSystem.AddDigit("9");

        string currentInput = TestReflectionHelper.GetPrivateField<string>(_keypadSystem, "_currentInput");

        Assert.AreEqual(4, currentInput.Length,
            "A bemenet hossza nem lehet több 4 karakternél.");
        Assert.AreEqual("1234", currentInput,
            "Az ötödik karaktert már nem lett volna szabad hozzáadni.");
    }

    // 9.
    [Test]
    public void DeleteLastDigit_RemovesLastCharacter()
    {
        _keypadSystem.AddDigit("1");
        _keypadSystem.AddDigit("2");
        _keypadSystem.AddDigit("3");

        _keypadSystem.DeleteLastDigit();

        string currentInput = TestReflectionHelper.GetPrivateField<string>(_keypadSystem, "_currentInput");

        Assert.AreEqual("12", currentInput,
            "A DeleteLastDigit() metódusnak törölnie kellett volna az utolsó karaktert.");
        Assert.AreEqual("12", _displayText.text,
            "A kijelzõn is a törölt értéknek megfelelõ szövegnek kell megjelennie.");
    }

    // 10.
    [Test]
    public void AddDigit_WhenCorrectCodeEntered_DisplaysSuccess()
    {
        _keypadSystem.AddDigit("1");
        _keypadSystem.AddDigit("2");
        _keypadSystem.AddDigit("3");
        _keypadSystem.AddDigit("4");

        Assert.AreEqual("SUCCESS", _displayText.text,
            "Helyes kód esetén a kijelzõn SUCCESS szövegnek kell megjelennie.");
        Assert.AreEqual(Color.green, _displayText.color,
            "Helyes kód esetén a kijelzõ színe zöld kell legyen.");
    }

    // 11.
    [Test]
    public void AddDigit_WhenCorrectCodeEntered_OpensDoorAndDisablesLockedDoor()
    {
        _keypadSystem.AddDigit("1");
        _keypadSystem.AddDigit("2");
        _keypadSystem.AddDigit("3");
        _keypadSystem.AddDigit("4");

        Assert.IsTrue(_openDoor.activeSelf,
            "Helyes kód esetén a nyitott ajtó objektumnak aktívnak kell lennie.");
        Assert.IsFalse(_lockedDoor.activeSelf,
            "Helyes kód esetén a zárt ajtó objektumnak inaktívnak kell lennie.");
    }

    // 12.
    [Test]
    public void AddDigit_WhenWrongFourDigitCodeEntered_DisplaysError()
    {
        _keypadSystem.AddDigit("9");
        _keypadSystem.AddDigit("9");
        _keypadSystem.AddDigit("9");
        _keypadSystem.AddDigit("9");

        Assert.AreEqual("ERROR", _displayText.text,
            "Hibás 4 jegyû kód esetén a kijelzõn ERROR szövegnek kell megjelennie.");
        Assert.AreEqual(Color.red, _displayText.color,
            "Hibás kód esetén a kijelzõ színe piros kell legyen.");
    }
}