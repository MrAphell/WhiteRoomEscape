using System.Collections;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.TestTools;

public class KeypadSystemPlayModeTests
{
    private GameObject keypadObject;
    private KeypadSystem keypad;

    private GameObject lockedDoor;
    private GameObject openDoor;
    private GameObject uiPanel;
    private TextMeshProUGUI display;

    [UnitySetUp]
    public IEnumerator Setup()
    {
        keypadObject = new GameObject("Keypad");
        keypad = keypadObject.AddComponent<KeypadSystem>();

        lockedDoor = new GameObject("LockedDoor");
        openDoor = new GameObject("OpenDoor");
        uiPanel = new GameObject("UIPanel");

        display = new GameObject("Display").AddComponent<TextMeshProUGUI>();

        lockedDoor.SetActive(true);
        openDoor.SetActive(false);
        uiPanel.SetActive(true);

        PlayModeReflectionHelper.SetPrivateField(keypad, "_correctCode", "1234");
        PlayModeReflectionHelper.SetPrivateField(keypad, "_lockedDoor", lockedDoor);
        PlayModeReflectionHelper.SetPrivateField(keypad, "_openDoorObject", openDoor);
        PlayModeReflectionHelper.SetPrivateField(keypad, "_uiPanel", uiPanel);
        PlayModeReflectionHelper.SetPrivateField(keypad, "_displayText", display);

        yield return null;
    }

    [UnityTearDown]
    public IEnumerator TearDown()
    {
        Object.Destroy(keypadObject);
        Object.Destroy(lockedDoor);
        Object.Destroy(openDoor);
        Object.Destroy(uiPanel);
        Object.Destroy(display.gameObject);

        yield return null;
    }

    [UnityTest]
    public IEnumerator CorrectCode_OpensDoor()
    {
        keypad.AddDigit("1");
        keypad.AddDigit("2");
        keypad.AddDigit("3");
        keypad.AddDigit("4");

        yield return null;

        Assert.AreEqual("SUCCESS", display.text, "A kijelzõn nem a SUCCESS felirat jelent meg.");
        Assert.IsFalse(lockedDoor.activeSelf, "A zárt ajtónak el kellett volna tûnnie.");
        Assert.IsTrue(openDoor.activeSelf, "A nyitott ajtónak meg kellett volna jelennie.");
    }
}