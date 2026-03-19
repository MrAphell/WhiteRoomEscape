using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class MainMenuControllerTests
{
    private GameObject _menuObject;
    private MainMenuController _controller;

    private GameObject _welcomeObject;
    private TextMeshProUGUI _welcomeText;

    private GameObject _dropdownObject;
    private TMP_Dropdown _dropdown;

    private GameObject _inputObject;
    private TMP_InputField _inputField;

    private string _backupAllProfiles;
    private string _backupLastSelected;
    private string _backupActiveProfile;

    [SetUp]
    public void Setup()
    {
        _backupAllProfiles = PlayerPrefs.GetString("AllProfiles", "Default");
        _backupLastSelected = PlayerPrefs.GetString("LastSelectedProfile", "Default");
        _backupActiveProfile = PlayerPrefs.GetString("ActiveProfileName", "Default");

        PlayerPrefs.DeleteKey("AllProfiles");
        PlayerPrefs.DeleteKey("LastSelectedProfile");
        PlayerPrefs.DeleteKey("ActiveProfileName");
        PlayerPrefs.Save();

        _menuObject = new GameObject("MainMenuController_TestObject");
        _controller = _menuObject.AddComponent<MainMenuController>();

        _welcomeObject = new GameObject("WelcomeText");
        _welcomeText = _welcomeObject.AddComponent<TextMeshProUGUI>();

        _dropdownObject = new GameObject("ProfileDropdown");
        _dropdown = _dropdownObject.AddComponent<TMP_Dropdown>();

        _inputObject = new GameObject("NewProfileInput");
        _inputField = _inputObject.AddComponent<TMP_InputField>();

        _controller.welcomeText = _welcomeText;
        _controller.profileDropdown = _dropdown;
        _controller.newProfileInput = _inputField;

        TestReflectionHelper.SetPrivateField(_controller, "profileNames", new List<string> { "Default" });
        TestReflectionHelper.SetPrivateField(_controller, "currentProfile", "Default");
    }

    [TearDown]
    public void TearDown()
    {
        if (_inputObject != null) Object.DestroyImmediate(_inputObject);
        if (_dropdownObject != null) Object.DestroyImmediate(_dropdownObject);
        if (_welcomeObject != null) Object.DestroyImmediate(_welcomeObject);
        if (_menuObject != null) Object.DestroyImmediate(_menuObject);

        PlayerPrefs.SetString("AllProfiles", _backupAllProfiles);
        PlayerPrefs.SetString("LastSelectedProfile", _backupLastSelected);
        PlayerPrefs.SetString("ActiveProfileName", _backupActiveProfile);
        PlayerPrefs.Save();
    }

    [Test]
    public void CreateNewProfile_WithValidName_AddsProfileAndSetsItActive()
    {
        _controller.newProfileInput.text = "PlayerOne";

        _controller.CreateNewProfile();

        List<string> profileNames = TestReflectionHelper.GetPrivateField<List<string>>(_controller, "profileNames");
        string currentProfile = TestReflectionHelper.GetPrivateField<string>(_controller, "currentProfile");

        Assert.Contains("PlayerOne", profileNames,
            "Az új profilt hozzá kellett volna adni a profileNames listához.");
        Assert.AreEqual("PlayerOne", currentProfile,
            "Az új profilnak kellett volna aktív profillá válnia.");
        Assert.AreEqual("PlayerOne", PlayerPrefs.GetString("ActiveProfileName"),
            "Az ActiveProfileName PlayerPrefs kulcsnak is az új profilra kellett volna váltania.");
    }

    [Test]
    public void CreateNewProfile_WithValidName_ClearsInputField()
    {
        _controller.newProfileInput.text = "PlayerTwo";

        _controller.CreateNewProfile();

        Assert.AreEqual(string.Empty, _controller.newProfileInput.text,
            "Sikeres létrehozás után az input mezõt ki kellett volna üríteni.");
    }

    [Test]
    public void CreateNewProfile_WithDuplicateName_DoesNotAddDuplicate()
    {
        _controller.newProfileInput.text = "PlayerOne";
        _controller.CreateNewProfile();

        _controller.newProfileInput.text = "PlayerOne";
        _controller.CreateNewProfile();

        List<string> profileNames = TestReflectionHelper.GetPrivateField<List<string>>(_controller, "profileNames");

        int count = 0;
        foreach (string profile in profileNames)
        {
            if (profile == "PlayerOne")
                count++;
        }

        Assert.AreEqual(1, count,
            "Duplikált profilnév nem kerülhet be többször a listába.");
    }

    [Test]
    public void DeleteCurrentProfile_WhenDefaultIsActive_DoesNotRemoveDefault()
    {
        _controller.DeleteCurrentProfile();

        List<string> profileNames = TestReflectionHelper.GetPrivateField<List<string>>(_controller, "profileNames");
        string currentProfile = TestReflectionHelper.GetPrivateField<string>(_controller, "currentProfile");

        Assert.Contains("Default", profileNames,
            "A Default profilnak törlés után is meg kell maradnia.");
        Assert.AreEqual("Default", currentProfile,
            "A jelenlegi profilnak továbbra is Defaultnak kell maradnia.");
    }

    [Test]
    public void DeleteCurrentProfile_WhenNonDefaultIsActive_RemovesItAndFallsBackToDefault()
    {
        TestReflectionHelper.SetPrivateField(_controller, "profileNames", new List<string> { "Default", "PlayerOne" });
        TestReflectionHelper.SetPrivateField(_controller, "currentProfile", "PlayerOne");

        _controller.DeleteCurrentProfile();

        List<string> profileNames = TestReflectionHelper.GetPrivateField<List<string>>(_controller, "profileNames");
        string currentProfile = TestReflectionHelper.GetPrivateField<string>(_controller, "currentProfile");

        Assert.IsFalse(profileNames.Contains("PlayerOne"),
            "A nem alapértelmezett aktív profilt törölni kellett volna.");
        Assert.AreEqual("Default", currentProfile,
            "Törlés után a jelenlegi profilnak Defaultnak kell lennie.");
        Assert.AreEqual("Default", PlayerPrefs.GetString("ActiveProfileName"),
            "A PlayerPrefs-ben is Defaultnak kell maradnia az aktív profilnak.");
    }
}