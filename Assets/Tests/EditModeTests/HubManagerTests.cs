using NUnit.Framework;
using UnityEngine;

public class HubManagerTests
{
    private GameObject _hubManagerObject;
    private HubManager _hubManager;
    private GameObject _testCube;

    private const string TestProfile = "TestProfile_Hub";

    [SetUp]
    public void Setup()
    {
        PlayerPrefs.SetString("ActiveProfileName", TestProfile);
        PlayerPrefs.DeleteKey(TestProfile + "_LevelsUnlocked");
        PlayerPrefs.Save();

        _hubManagerObject = new GameObject("HubManager_TestObject");
        _hubManager = _hubManagerObject.AddComponent<HubManager>();

        TestReflectionHelper.SetPrivateField(_hubManager, "_propBlock", new MaterialPropertyBlock());
    }

    [TearDown]
    public void TearDown()
    {
        PlayerPrefs.DeleteKey(TestProfile + "_LevelsUnlocked");
        PlayerPrefs.Save();

        if (_hubManagerObject != null)
            Object.DestroyImmediate(_hubManagerObject);

        if (_testCube != null)
            Object.DestroyImmediate(_testCube);
    }

    [Test]
    public void UpdateAllCubes_WhenLevelIsLocked_DisablesCollider()
    {
        PlayerPrefs.SetInt(TestProfile + "_LevelsUnlocked", 2);
        PlayerPrefs.Save();

        _testCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        _testCube.name = "LockedLevelCube";

        LevelEntrance entrance = _testCube.AddComponent<LevelEntrance>();
        entrance.sceneToLoad = "Game_3";

        _hubManager.allLevelCubes = new GameObject[] { _testCube };

        _hubManager.UpdateAllCubes();

        Collider col = _testCube.GetComponent<Collider>();
        Assert.IsFalse(col.enabled, "Zárt pálya esetén a collidernek letiltottnak kell lennie.");

    }

    [Test]
    public void UpdateAllCubes_WhenLevelIsCurrentUnlocked_EnablesCollider()
    {
        PlayerPrefs.SetInt(TestProfile + "_LevelsUnlocked", 2);
        PlayerPrefs.Save();

        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.name = "CurrentLevelCube";

        LevelEntrance entrance = cube.AddComponent<LevelEntrance>();
        entrance.sceneToLoad = "Game_2";

        _hubManager.allLevelCubes = new GameObject[] { cube };

        _hubManager.UpdateAllCubes();

        Collider col = cube.GetComponent<Collider>();

        Assert.IsTrue(col.enabled,
            "Az aktuálisan elérhetõ pálya colliderének engedélyezettnek kell lennie.");

        Object.DestroyImmediate(cube);
    }

    [Test]
    public void UpdateAllCubes_WhenLevelIsAlreadyCompleted_EnablesCollider()
    {
        PlayerPrefs.SetInt(TestProfile + "_LevelsUnlocked", 4);
        PlayerPrefs.Save();

        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.name = "CompletedLevelCube";

        LevelEntrance entrance = cube.AddComponent<LevelEntrance>();
        entrance.sceneToLoad = "Game_2";

        _hubManager.allLevelCubes = new GameObject[] { cube };

        _hubManager.UpdateAllCubes();

        Collider col = cube.GetComponent<Collider>();

        Assert.IsTrue(col.enabled,
            "A már teljesített pálya colliderének továbbra is engedélyezettnek kell lennie.");

        Object.DestroyImmediate(cube);
    }
}