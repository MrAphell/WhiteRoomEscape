using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class MovableObjectPlayModeTests
{
    GameObject obj;
    MovableObject movable;

    [UnitySetUp]
    public IEnumerator Setup()
    {
        obj = new GameObject("MovableObject");
        movable = obj.AddComponent<MovableObject>();

        PlayModeReflectionHelper.SetPrivateField(movable, "_moveOffset", new Vector3(0, 0, 5));
        PlayModeReflectionHelper.SetPrivateField(movable, "_speed", 5f);

        obj.transform.position = Vector3.zero;

        yield return null;
    }

    [UnityTearDown]
    public IEnumerator TearDown()
    {
        Object.Destroy(obj);
        yield return null;
    }

    [UnityTest]
    public IEnumerator Prompt_Initial_IsMove()
    {
        yield return null;

        Assert.AreEqual("Press [E] to Move", movable.GetPrompt());
    }

    [UnityTest]
    public IEnumerator Interact_ChangesPrompt()
    {
        movable.Interact();
        yield return null;

        Assert.AreEqual("Press [E] to Reset", movable.GetPrompt());
    }

    [UnityTest]
    public IEnumerator Interact_ObjectMoves()
    {
        Vector3 start = obj.transform.position;

        movable.Interact();

        for (int i = 0; i < 20; i++)
            yield return null;

        Assert.Greater(obj.transform.position.z, start.z);
    }

    [UnityTest]
    public IEnumerator InteractTwice_ReturnsToStart()
    {
        Vector3 start = obj.transform.position;

        movable.Interact();
        for (int i = 0; i < 20; i++) yield return null;

        movable.Interact();
        for (int i = 0; i < 20; i++) yield return null;

        Assert.Less(
            Vector3.Distance(obj.transform.position, start),
            3f
        );
    }
}