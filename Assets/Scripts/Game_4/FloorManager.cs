using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FloorManager : MonoBehaviour
{
    public static FloorManager Instance;

    [Header("Beállítások")]
    [SerializeField] private GameObject _tilePrefab;
    [SerializeField] private int _gridSize = 20;
    [SerializeField] private float _tileSpacing = 1.0f;

    [Header("Nyerés Beállítások")]
    [SerializeField] private GameObject _exitToHub;

    private GridTile[,] _grid;
    private List<GridTile> _safePath = new List<GridTile>();

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        GenerateGrid();
        GeneratePath();
        StartCoroutine(ShowPathPreview());
    }

    // A rács létrehozása a pontos koordináták alapján
    void GenerateGrid()
    {
        _grid = new GridTile[_gridSize, _gridSize];

        // A kezdõpont (jobb alsó sarok a képed alapján) -> X: 9.5, Z: -9.5
        float startX = 9.5f;
        float startZ = -9.5f;

        for (int x = 0; x < _gridSize; x++)
        {
            for (int y = 0; y < _gridSize; y++)
            {
                // Az X koordináta csökken (9.5-tõl -9.5-ig), a Z nõ (-9.5-tõl 9.5-ig)
                Vector3 pos = new Vector3(startX - (x * _tileSpacing), transform.position.y, startZ + (y * _tileSpacing));
                GameObject t = Instantiate(_tilePrefab, pos, Quaternion.identity, transform);
                _grid[x, y] = t.GetComponent<GridTile>();
                _grid[x, y].Setup(x, y, GridTile.TileType.Trap);
            }
        }
    }

    // Az út generálása a (9.5, 0, -9.5) sarokból az ajtóig
    void GeneratePath()
    {
        _safePath.Clear();
        foreach (var tile in _grid)
        {
            if (tile != null) tile.Setup(tile.x, tile.y, GridTile.TileType.Trap);
        }

        int curX = 0; // Ez felel meg a 9.5-ös X koordinátának
        int curY = 0; // Ez felel meg a -9.5-ös Z koordinátának

        // A kezdõkocka mindig biztonságos
        _grid[curX, curY].type = GridTile.TileType.Safe;
        _safePath.Add(_grid[curX, curY]);

        // Megyünk elõre (Z irányba) az ajtó vonaláig
        while (curY < _gridSize - 1)
        {
            int rand = Random.Range(0, 3); // 0: Elõre, 1: Balra, 2: Jobbra

            // Ne menjünk le a pályáról oldalt
            if (rand == 1 && curX >= _gridSize - 1) rand = 0;
            if (rand == 2 && curX <= 0) rand = 0;

            if (rand == 0) curY++;      // Egy lépés elõre
            else if (rand == 1) curX++; // Egy lépés oldalra
            else if (rand == 2) curX--; // Egy lépés a másik oldalra

            GridTile current = _grid[curX, curY];

            if (!_safePath.Contains(current))
            {
                current.type = GridTile.TileType.Safe;
                _safePath.Add(current);
            }
        }

        // Ha elértük az utolsó sort, kihúzzuk az utat az ajtóig (X = 0, ami a rácsban a 9-es vagy 10-es index)
        int doorIndexX = 9;
        while (curX != doorIndexX)
        {
            if (curX < doorIndexX) curX++;
            else curX--;

            GridTile current = _grid[curX, curY];
            if (!_safePath.Contains(current))
            {
                current.type = GridTile.TileType.Safe;
                _safePath.Add(current);
            }
        }

        // Az ajtó elõtti utolsó csempe a Cél
        _grid[curX, curY].type = GridTile.TileType.Goal;
    }

    // Ezt hívják meg a csempék, ha rájuk lépsz
    public void OnTileStepped(GridTile tile)
    {
        if (tile.state == GridTile.TileState.Revealed) return;

        if (tile.type == GridTile.TileType.Safe || tile.type == GridTile.TileType.Goal)
        {
            tile.SetRevealed();
            if (tile.type == GridTile.TileType.Goal) WinLevel();
        }
        else
        {
            tile.SetError();
            Invoke("ResetPlayer", 0.5f); // Fél másodperc múlva reset
        }
    }

    // A Játékos visszadobása és a pálya újragenerálása
    void ResetPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            // Trükk: CharacterController esetén ki kell kapcsolni, hogy át lehessen tenni máshová!
            CharacterController cc = player.GetComponent<CharacterController>();
            if (cc != null) cc.enabled = false;

            // Visszadobjuk pontosan a (9.5, Y, -9.5) koordinátára
            player.transform.position = new Vector3(9.5f, player.transform.position.y, -9.5f);

            if (cc != null) cc.enabled = true;
        }

        // Pálya reset és újratervezés
        foreach (var tile in _grid) tile.ResetTile();
        GeneratePath();
        StartCoroutine(ShowPathPreview());
    }

    private IEnumerator ShowPathPreview()
    {
        yield return new WaitForSeconds(0.5f);
        foreach (var tile in _safePath)
        {
            tile.Flash(Color.darkMagenta, 20f);
        }
    }

    void WinLevel()
    {
        Debug.Log("Pálya teljesítve! Kijárat aktiválása...");

        // Ha be van állítva az objektum, akkor bekapcsoljuk (láthatóvá és használhatóvá tesszük)
        if (_exitToHub != null)
        {
            _exitToHub.SetActive(true);
        }

        // GameManager.CompleteLevel(4); // Ha a GameManager is be lesz kötve, ezt majd kiveheted a kommentbõl
    }
}