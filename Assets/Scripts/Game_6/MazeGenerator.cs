using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Véletlenszerûen generált labirintust készítõ osztály Recursive Backtracker algoritmussal
public class MazeGenerator : MonoBehaviour
{
    [Header("Referenciák")]
    public MazeCell cellPrefab; // Egyetlen labirintus-cella (falakkal)
    public Transform playerObj; // A játékos transzformja
    public Transform exitObj;   // A kijárati objektum transzformja

    [Header("Beállítások")]
    public int width = 5;       // Labirintus szélessége (cellákban)
    public int height = 5;      // Labirintus magassága (cellákban)
    public float cellSize = 4f; // Egy cella fizikai mérete a világban

    [Header("Ajtó Magasság")]
    public float doorHeight = 1.2f;

    // Irányok konstansai a könnyebb olvashatóságért
    private const int DIR_FRONT = 1;
    private const int DIR_BACK = 2;
    private const int DIR_LEFT = 3;
    private const int DIR_RIGHT = 4;

    private MazeCell[,] _grid;  // A cellák kétdimenziós tömbje
    private bool[,] _visited;   // Nyilvántartja, melyik cellán járt már a generátor

    void Start()
    {
        ClearOldMaze(); // Esetleges régi labirintus törlése
        StartCoroutine(GenerateMazeRoutine()); // Generálás indítása
    }

    // Törli a hierarchiából az összes korábban legenerált cellát
    private void ClearOldMaze()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

    // A labirintusgenerálás fõ folyamata (Coroutine a várakozások miatt)
    IEnumerator GenerateMazeRoutine()
    {
        InitializeGrid(); // Üres rács felépítése minden fallal

        // Algoritmus futtatása a bal alsó sarokból (0,0)
        yield return StartCoroutine(RecursiveBacktracker(0, 0));

        // Várunk egy frame-et, hogy a Unity fizikailag is törölje a falakat a memóriából
        yield return null;

        PlaceObjectsAndRotatePlayer(); // Játékos és kijárat elhelyezése
    }

    // Létrehozza a cellákat a megadott méretben, alapértelmezetten minden fal áll
    private void InitializeGrid()
    {
        _grid = new MazeCell[width, height];
        _visited = new bool[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector3 pos = transform.position + new Vector3(x * cellSize, 0, y * cellSize);
                MazeCell newCell = Instantiate(cellPrefab, pos, Quaternion.identity, transform);

                newCell.name = $"Cell_{x}_{y}";
                _grid[x, y] = newCell;
            }
        }
    }

    // A labirintus "kivájása" verem (Stack) alapú bejárással
    IEnumerator RecursiveBacktracker(int startX, int startY)
    {
        Stack<Vector2Int> stack = new Stack<Vector2Int>();
        stack.Push(new Vector2Int(startX, startY));
        _visited[startX, startY] = true;

        while (stack.Count > 0)
        {
            Vector2Int current = stack.Peek();

            // Megkeressük a még meg nem látogatott szomszédos cellákat
            List<int> validNeighbors = GetUnvisitedNeighbors(current);

            if (validNeighbors.Count > 0)
            {
                // Véletlenszerû irány választása a szomszédok közül
                int direction = validNeighbors[Random.Range(0, validNeighbors.Count)];

                // Falak eltávolítása a jelenlegi és a célcella között
                Vector2Int nextPos = RemoveWallsAndGetNextPos(current, direction);

                _visited[nextPos.x, nextPos.y] = true;
                stack.Push(nextPos);
            }
            else
            {
                stack.Pop(); // Ha nincs merre menni, visszalépünk (backtrack)
            }
        }
        yield return null;
    }

    // Ellenõrzi, hogy a szomszédos cellák közül melyek nincsenek még meglátogatva
    private List<int> GetUnvisitedNeighbors(Vector2Int current)
    {
        List<int> neighbors = new List<int>();
        int x = current.x;
        int y = current.y;

        if (y + 1 < height && !_visited[x, y + 1]) neighbors.Add(DIR_FRONT);
        if (y - 1 >= 0 && !_visited[x, y - 1]) neighbors.Add(DIR_BACK);
        if (x - 1 >= 0 && !_visited[x - 1, y]) neighbors.Add(DIR_LEFT);
        if (x + 1 < width && !_visited[x + 1, y]) neighbors.Add(DIR_RIGHT);

        return neighbors;
    }

    // Eltávolítja a falat az aktuális és a következõ cella közül
    private Vector2Int RemoveWallsAndGetNextPos(Vector2Int current, int direction)
    {
        int x = current.x;
        int y = current.y;
        int nextX = x;
        int nextY = y;

        // 1. Saját fal törlése
        _grid[x, y].RemoveWall(direction);

        // 2. Szomszéd koordinátájának meghatározása és a szemközti falának törlése
        switch (direction)
        {
            case DIR_FRONT:
                nextY++;
                if (IsInsideGrid(x, nextY)) _grid[x, nextY].RemoveWall(DIR_BACK);
                break;
            case DIR_BACK:
                nextY--;
                if (IsInsideGrid(x, nextY)) _grid[x, nextY].RemoveWall(DIR_FRONT);
                break;
            case DIR_LEFT:
                nextX--;
                if (IsInsideGrid(nextX, y)) _grid[nextX, y].RemoveWall(DIR_RIGHT);
                break;
            case DIR_RIGHT:
                nextX++;
                if (IsInsideGrid(nextX, y)) _grid[nextX, y].RemoveWall(DIR_LEFT);
                break;
        }

        return new Vector2Int(nextX, nextY);
    }

    private bool IsInsideGrid(int x, int y)
    {
        return x >= 0 && y >= 0 && x < width && y < height;
    }

    // A kész labirintusba elhelyezi a játékost és a kijáratot
    void PlaceObjectsAndRotatePlayer()
    {
        float halfSize = cellSize / 2f;

        if (playerObj != null)
        {
            CharacterController cc = playerObj.GetComponent<CharacterController>();
            if (cc) cc.enabled = false; // Kikapcsoljuk a fizikát a teleportálás idejére

            playerObj.position = transform.position + new Vector3(0, 1.5f, 0);
            RotatePlayerToOpenPath(); // Játékos beforgatása a folyosó irányába

            if (cc) cc.enabled = true;
        }

        if (exitObj != null)
        {
            PlaceExitDoor(halfSize); // Kijárat elhelyezése az utolsó cellában
        }
    }

    // A játékos induláskor az üres út felé fog nézni a fal helyett
    private void RotatePlayerToOpenPath()
    {
        MazeCell startCell = _grid[0, 0];
        Quaternion targetRotation = Quaternion.Euler(0, 45, 0);

        if (startCell.wallFront == null) targetRotation = Quaternion.Euler(0, 0, 0);
        else if (startCell.wallRight == null) targetRotation = Quaternion.Euler(0, 90, 0);

        playerObj.rotation = targetRotation;
    }

    // Kijárati ajtó elhelyezése a labirintus átlós végpontján (width-1, height-1)
    private void PlaceExitDoor(float halfSize)
    {
        int endX = width - 1;
        int endY = height - 1;
        Vector3 endCenter = transform.position + new Vector3(endX * cellSize, 0, endY * cellSize);
        Vector3 doorPos = endCenter + new Vector3(halfSize, doorHeight, 0);

        exitObj.position = doorPos;
        exitObj.rotation = Quaternion.Euler(0, 90, 0);
    }
}