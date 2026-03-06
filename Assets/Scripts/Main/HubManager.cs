using UnityEngine;
using System.Text.RegularExpressions;

// A központi helyszín (Hub) szintválasztó kockáinak vizuális és logikai állapotát kezeli
public class HubManager : MonoBehaviour
{
    [Header("ÖSSZES pálya kocka")]
    public GameObject[] allLevelCubes; // A pályákhoz tartozó interaktív kockák listája

    [Header("Szín Beállítások")]
    public Color lockedColor = Color.red;      // Zárolt pálya színe
    public Color unlockedColor = Color.gray;   // Elérhetõ, de még nem teljesített pálya színe
    public Color completedColor = Color.green; // Már sikeresen befejezett pálya színe

    // Anyagtulajdonságok hatékony módosítására szolgáló blokk (Shader változókhoz)
    private MaterialPropertyBlock _propBlock;

    private void Start()
    {
        _propBlock = new MaterialPropertyBlock();
        UpdateAllCubes(); // Induláskor beállítjuk az összes kocka aktuális állapotát
    }

    // Végigmegy a listán és frissíti az összes kockát a mentett haladás alapján
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

    // Egyetlen kocka színének és interakciójának (Collider) beállítása
    private void UpdateSingleCube(GameObject cube)
    {
        Renderer rend = cube.GetComponent<Renderer>();
        LevelEntrance entrance = cube.GetComponent<LevelEntrance>();
        Collider col = cube.GetComponent<Collider>();

        // Ha hiányoznak a szükséges komponensek, nem csinálunk semmit
        if (rend == null || entrance == null) return;

        // Kinyerjük a pálya számát a betöltendõ jelenet nevébõl (Regex)
        int levelNum = 999;
        string numberPart = Regex.Match(entrance.sceneToLoad, @"\d+").Value;
        if (!string.IsNullOrEmpty(numberPart))
        {
            levelNum = int.Parse(numberPart);
        }

        // Lekérjük a GameManager-tõl, hogy hol tart jelenleg a játékos
        int currentProgress = GameManager.GetUnlockedLevel();

        Color targetColor;

        // 1.Eset: Már teljesített pálya
        if (levelNum < currentProgress)
        {
            targetColor = completedColor;
            if (col != null) col.enabled = true; // Bármikor újra beléphet
        }
        // 2.Eset: Aktuálisan soron következõ, elérhetõ pálya
        else if (levelNum == currentProgress)
        {
            targetColor = unlockedColor;
            if (col != null) col.enabled = true;
        }
        // 3.Eset: Zárolt pálya
        else
        {
            targetColor = lockedColor;
            if (col != null) col.enabled = false; // Nem lehet rákattintani/belépni
        }

        // Színbeállítás alkalmazása (MaterialPropertyBlock)
        rend.GetPropertyBlock(_propBlock);
        _propBlock.SetColor("_Color", targetColor);
        rend.SetPropertyBlock(_propBlock);
    }
}