using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using StarterAssets;

public class InteractionControllerTests
{
    private GameObject _controllerObject;
    private InteractionController _interactionController;

    private GameObject _keypadUI;
    private GameObject _crosshairObject;
    private Image _crosshair;

    private GameObject _promptObject;
    private TextMeshProUGUI _promptText;

    private GameObject _cameraRootObject;
    private Transform _cameraRoot;

    private FirstPersonController _fpsController;

    [SetUp]
    public void Setup()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        _controllerObject = new GameObject("InteractionController_TestObject");
        _interactionController = _controllerObject.AddComponent<InteractionController>();

        _fpsController = _controllerObject.AddComponent<FirstPersonController>();

        _keypadUI = new GameObject("KeypadUI");
        _keypadUI.SetActive(false);

        _crosshairObject = new GameObject("Crosshair");
        _crosshair = _crosshairObject.AddComponent<Image>();
        _crosshair.enabled = true;

        _promptObject = new GameObject("PromptText");
        _promptText = _promptObject.AddComponent<TextMeshProUGUI>();

        _cameraRootObject = new GameObject("CameraRoot");
        _cameraRoot = _cameraRootObject.transform;

        TestReflectionHelper.SetPrivateField(_interactionController, "_keypadUI", _keypadUI);
        TestReflectionHelper.SetPrivateField(_interactionController, "_crosshair", _crosshair);
        TestReflectionHelper.SetPrivateField(_interactionController, "_promptText", _promptText);
        TestReflectionHelper.SetPrivateField(_interactionController, "_cameraRoot", _cameraRoot);
        TestReflectionHelper.SetPrivateField(_interactionController, "_fpsController", _fpsController);
    }

    [TearDown]
    public void TearDown()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (_cameraRootObject != null)
            Object.DestroyImmediate(_cameraRootObject);

        if (_promptObject != null)
            Object.DestroyImmediate(_promptObject);

        if (_crosshairObject != null)
            Object.DestroyImmediate(_crosshairObject);

        if (_keypadUI != null)
            Object.DestroyImmediate(_keypadUI);

        if (_controllerObject != null)
            Object.DestroyImmediate(_controllerObject);
    }

    [Test]
    public void OpenKeypad_ActivatesKeypadUI()
    {
        _interactionController.OpenKeypad();

        Assert.IsTrue(_keypadUI.activeSelf,
            "Az OpenKeypad() után a keypad UI-nak aktívnak kell lennie.");
    }

    [Test]
    public void OpenKeypad_DisablesFirstPersonController()
    {
        _fpsController.enabled = true;

        _interactionController.OpenKeypad();

        Assert.IsFalse(_fpsController.enabled,
            "Az OpenKeypad() után a FirstPersonController komponenst le kell tiltani.");
    }

    [Test]
    public void OpenKeypad_HidesCrosshair()
    {
        _crosshair.enabled = true;

        _interactionController.OpenKeypad();

        Assert.IsFalse(_crosshair.enabled,
            "Az OpenKeypad() után a crosshairnek elrejtett állapotban kell lennie.");
    }

    [Test]
    public void CloseKeypad_DeactivatesKeypadUI()
    {
        _keypadUI.SetActive(true);

        _interactionController.CloseKeypad();

        Assert.IsFalse(_keypadUI.activeSelf,
            "A CloseKeypad() után a keypad UI-nak inaktívnak kell lennie.");
    }

    [Test]
    public void CloseKeypad_EnablesFirstPersonController()
    {
        _fpsController.enabled = false;

        _interactionController.CloseKeypad();

        Assert.IsTrue(_fpsController.enabled,
            "A CloseKeypad() után a FirstPersonController komponenst újra engedélyezni kell.");
    }

    [Test]
    public void CloseKeypad_ShowsCrosshair()
    {
        _crosshair.enabled = false;

        _interactionController.CloseKeypad();

        Assert.IsTrue(_crosshair.enabled,
            "A CloseKeypad() után a crosshairnek újra láthatónak kell lennie.");
    }
}