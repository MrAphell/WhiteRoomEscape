using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class HubManagerPlayModeTests
{
    HubManager manager;
    GameObject cube;

    [UnitySetUp]
    public IEnumerator Setup()
    {
        var obj = new GameObject();
        manager = obj.AddComponent<HubManager>();

        cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.AddComponent<BoxCollider>();

        var entrance = cube.AddComponent<LevelEntrance>();
        entrance.sceneToLoad = "Game_5";

        manager.allLevelCubes = new GameObject[] { cube };

        PlayerPrefs.SetString("ActiveProfileName", "Test");
        PlayerPrefs.SetInt("Test_LevelsUnlocked", 1);

        yield return null;
    }

    [UnityTearDown]
    public IEnumerator TearDown()
    {
        if (manager != null) Object.Destroy(manager.gameObject);
        if (cube != null) Object.Destroy(cube);
        yield return null;
    }

    [UnityTest]
    public IEnumerator LockedLevel_DisablesCollider()
    {
        manager.UpdateAllCubes();
        yield return null;

        Assert.IsFalse(cube.GetComponent<Collider>().enabled);
    }
}