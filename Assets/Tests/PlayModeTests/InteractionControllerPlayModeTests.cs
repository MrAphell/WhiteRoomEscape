using System.Collections;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;
using StarterAssets;

public class InteractionControllerPlayModeTests
{
    private GameObject player;
    private InteractionController controller;
    private FirstPersonController fps;

    private Transform cameraRoot;
    private Image crosshair;
    private TextMeshProUGUI prompt;
    private GameObject keypad;

    [UnitySetUp]
    public IEnumerator Setup()
    {
        player = new GameObject("Player");
        controller = player.AddComponent<InteractionController>();

        fps = player.AddComponent<FirstPersonController>();
        fps.enabled = false;

        cameraRoot = new GameObject("CameraRoot").transform;
        crosshair = new GameObject("Crosshair").AddComponent<Image>();
        prompt = new GameObject("Prompt").AddComponent<TextMeshProUGUI>();

        keypad = new GameObject("Keypad");
        keypad.SetActive(false);

        PlayModeReflectionHelper.SetPrivateField(controller, "_cameraRoot", cameraRoot);
        PlayModeReflectionHelper.SetPrivateField(controller, "_crosshair", crosshair);
        PlayModeReflectionHelper.SetPrivateField(controller, "_promptText", prompt);
        PlayModeReflectionHelper.SetPrivateField(controller, "_keypadUI", keypad);
        PlayModeReflectionHelper.SetPrivateField(controller, "_fpsController", fps);

        yield return null;
    }

    [UnityTearDown]
    public IEnumerator TearDown()
    {
        if (player != null) Object.DestroyImmediate(player);
        if (cameraRoot != null) Object.DestroyImmediate(cameraRoot.gameObject);
        if (crosshair != null) Object.DestroyImmediate(crosshair.gameObject);
        if (prompt != null) Object.DestroyImmediate(prompt.gameObject);
        if (keypad != null) Object.DestroyImmediate(keypad);

        yield return null;
    }


    [UnityTest]
    public IEnumerator OpenKeypad_ActivatesUI()
    {
        controller.OpenKeypad();
        yield return null;

        Assert.IsTrue(keypad.activeSelf, "A Keypad UI nem aktiválódott!");
    }

    [UnityTest]
    public IEnumerator OpenKeypad_DisablesCrosshair()
    {
        controller.OpenKeypad();
        yield return null;

        Assert.IsFalse(crosshair.enabled, "A célkereszt nem tûnt el a Keypad megnyitásakor!");
    }

    [UnityTest]
    public IEnumerator CloseKeypad_HidesUI()
    {
        controller.OpenKeypad();
        yield return null;

        controller.CloseKeypad();

        Assert.IsFalse(keypad.activeSelf, "A Keypad UI nem tûnt el a bezáráskor!");
    }

    [UnityTest]
    public IEnumerator CloseKeypad_ShowsCrosshair()
    {
        controller.OpenKeypad();
        yield return null;

        controller.CloseKeypad();

        Assert.IsTrue(crosshair.enabled, "A célkereszt nem jelent meg újra a bezáráskor!");
    }

    [UnityTest]
    public IEnumerator OpenThenCloseKeypad_ControlFlowWorks()
    {
        fps.enabled = true;
        crosshair.enabled = true;

        controller.OpenKeypad();
        yield return null;

        Assert.IsTrue(keypad.activeSelf, "A Keypad nem nyílt meg.");
        Assert.IsFalse(crosshair.enabled, "A célkereszt nem tûnt el.");
        Assert.IsFalse(fps.enabled, "Az irányítás nem állt le.");

        controller.CloseKeypad();

        Assert.IsFalse(keypad.activeSelf, "A Keypad nem zárt be.");
        Assert.IsTrue(crosshair.enabled, "A célkereszt nem jött vissza.");
        Assert.IsTrue(fps.enabled, "Az irányítás nem indult újra.");
    }
}