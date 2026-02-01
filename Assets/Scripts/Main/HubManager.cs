using UnityEngine;
using System.Text.RegularExpressions;

public class HubManager : MonoBehaviour
{
    [Header("ÖSSZES pálya kocka")]
    public GameObject[] allLevelCubes;

    [Header("Szín Beállítások")]
    public Color lockedColor = Color.red;
    public Color unlockedColor = Color.gray;
    public Color completedColor = Color.green;

    private MaterialPropertyBlock _propBlock;

    private void Start()
    {
        _propBlock = new MaterialPropertyBlock();
        UpdateAllCubes();
    }

    public void UpdateAllCubes()
    {
        for (int i = 0; i < allLevelCubes.Length; i++)
        {
            if (allLevelCubes[i] != null)
            {
                UpdateSingleCube(allLevelCubes[i]);
            }
        }
    }

    private void UpdateSingleCube(GameObject cube)
    {
        Renderer rend = cube.GetComponent<Renderer>();
        LevelEntrance entrance = cube.GetComponent<LevelEntrance>();
        Collider col = cube.GetComponent<Collider>();

        if (rend == null || entrance == null) return;

        int levelNum = 999;
        string numberPart = Regex.Match(entrance.sceneToLoad, @"\d+").Value;
        if (!string.IsNullOrEmpty(numberPart))
        {
            levelNum = int.Parse(numberPart);
        }

        int currentProgress = GameManager.GetUnlockedLevel();

        Color targetColor;

        if (levelNum < currentProgress)
        {
            targetColor = completedColor;

            if (col != null) col.enabled = true;
        }
        else if (levelNum == currentProgress)
        {
            targetColor = unlockedColor;

            if (col != null) col.enabled = true;
        }
        else
        {
            targetColor = lockedColor;

            if (col != null) col.enabled = false;
        }

        rend.GetPropertyBlock(_propBlock);
        _propBlock.SetColor("_Color", targetColor);
        rend.SetPropertyBlock(_propBlock);
    }
}