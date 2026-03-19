using System.Collections;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.TestTools;

public class FlashlightControllerPlayModeTests
{
    GameObject obj;
    FlashlightController controller;
    Light lightComponent;

    TextMeshProUGUI prompt;

    [UnitySetUp]
    public IEnumerator Setup()
    {
        obj = new GameObject("Flashlight");

        lightComponent = obj.AddComponent<Light>();
        controller = obj.AddComponent<FlashlightController>();
        obj.AddComponent<AudioSource>();

        GameObject promptObj = new GameObject("Prompt");
        prompt = promptObj.AddComponent<TextMeshProUGUI>();

        PlayModeReflectionHelper.SetPrivateField(controller, "_promptText", prompt);

        yield return null;
    }

    [UnityTearDown]
    public IEnumerator TearDown()
    {
        Object.Destroy(obj);
        yield return null;
    }

    [UnityTest]
    public IEnumerator Start_LightOff()
    {
        yield return null;

        Assert.IsFalse(lightComponent.enabled);
    }

    [UnityTest]
    public IEnumerator Toggle_TurnsLightOn()
    {
        yield return null;

        PlayModeReflectionHelper.InvokePrivateMethod(controller, "ToggleFlashlight");

        yield return null;

        Assert.IsTrue(lightComponent.enabled);
    }

    [UnityTest]
    public IEnumerator ToggleTwice_TurnsLightOff()
    {
        yield return null;

        PlayModeReflectionHelper.InvokePrivateMethod(controller, "ToggleFlashlight");
        yield return null;

        PlayModeReflectionHelper.InvokePrivateMethod(controller, "ToggleFlashlight");
        yield return null;

        Assert.IsFalse(lightComponent.enabled);
    }
}