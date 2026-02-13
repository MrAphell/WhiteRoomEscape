using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MazeGenerator : MonoBehaviour
{
    [Header("Referenciák")]
    public MazeCell cellPrefab;
    public Transform playerObj;
    public Transform exitObj;

    [Header("Beállítások")]
    public int width = 5;
    public int height = 5;
    public float cellSize = 4f;

    [Header("Ajtó Magasság")]
    public float doorHeight = 1.2f;

    // Konstansok: "mágikus számokat" (1,2,3,4) használjunk a kódban
    private const int DIR_FRONT = 1;
    private const int DIR_BACK = 2;
    private const int DIR_LEFT = 3;
    private const int DIR_RIGHT = 4;

    private MazeCell[,] _grid;
    private bool[,] _visited;

    void Start()
    {
        ClearOldMaze();
        StartCoroutine(GenerateMazeRoutine());
    }

    // 1. TAKARÍTÁS (Külön függvényben)
    private void ClearOldMaze()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

    // 2. FÕ FOLYAMAT
    IEnumerator GenerateMazeRoutine()
    {
        InitializeGrid();

        // Megvárjuk, amíg az algoritmus végez
        yield return StartCoroutine(RecursiveBacktracker(0, 0));

        // FONTOS JAVÍTÁS! Várunk egy frame-et, hogy a Destroy() lefusson.
        // Ha ezt nem tesszük meg, a falak még "léteznek" a memóriában,
        // és a RotatePlayerToOpenPath rosszul döntene.
        yield return null;

        PlaceObjectsAndRotatePlayer();
    }

    // 3. RÁCS ÉPÍTÉSE
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

                newCell.name = $"Cell_{x}_{y}"; // Szebb elnevezés ($ jel)
                _grid[x, y] = newCell;
            }
        }
    }

    // 4. ALGORITMUS (Backtracker)
    IEnumerator RecursiveBacktracker(int startX, int startY)
    {
        Stack<Vector2Int> stack = new Stack<Vector2Int>();
        stack.Push(new Vector2Int(startX, startY));
        _visited[startX, startY] = true;

        while (stack.Count > 0)
        {
            Vector2Int current = stack.Peek();

            // Szomszédok összegyûjtése (kiszervezve)
            List<int> validNeighbors = GetUnvisitedNeighbors(current);

            if (validNeighbors.Count > 0)
            {
                int direction = validNeighbors[Random.Range(0, validNeighbors.Count)];

                // Falak törlése és lépés (kiszervezve)
                Vector2Int nextPos = RemoveWallsAndGetNextPos(current, direction);

                _visited[nextPos.x, nextPos.y] = true;
                stack.Push(nextPos);
            }
            else
            {
                stack.Pop();
            }

            // Ha látni akarod épülni, ide tehetsz: yield return new WaitForSeconds(0.05f);
        }
        yield return null;
    }

    //  Szomszédok keresése
    private List<int> GetUnvisitedNeighbors(Vector2Int current)
    {
        List<int> neighbors = new List<int>();
        int x = current.x;
        int y = current.y;

        // Front (y + 1)
        if (y + 1 < height && !_visited[x, y + 1]) neighbors.Add(DIR_FRONT);
        // Back (y - 1)
        if (y - 1 >= 0 && !_visited[x, y - 1]) neighbors.Add(DIR_BACK);
        // Left (x - 1)
        if (x - 1 >= 0 && !_visited[x - 1, y]) neighbors.Add(DIR_LEFT);
        // Right (x + 1)
        if (x + 1 < width && !_visited[x + 1, y]) neighbors.Add(DIR_RIGHT);

        return neighbors;
    }

    //  Falak kivétele és koordináta számítás
    private Vector2Int RemoveWallsAndGetNextPos(Vector2Int current, int direction)
    {
        int x = current.x;
        int y = current.y;
        int nextX = x;
        int nextY = y;

        // 1. Saját fal kivétele
        _grid[x, y].RemoveWall(direction);

        // 2. Szomszéd koordináta számítása és szomszéd falának kivétele
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

    // 5. Objektumok lerakása és játékos forgatása (külön függvényben)
    void PlaceObjectsAndRotatePlayer()
    {
        float halfSize = cellSize / 2f;

        // --- PLAYER ---
        if (playerObj != null)
        {
            CharacterController cc = playerObj.GetComponent<CharacterController>();
            if (cc) cc.enabled = false;

            playerObj.position = transform.position + new Vector3(0, 1.5f, 0);

            // Forgatás logika
            RotatePlayerToOpenPath();

            if (cc) cc.enabled = true;
        }

        // --- EXIT ---
        if (exitObj != null)
        {
            PlaceExitDoor(halfSize);
        }
    }

    private void RotatePlayerToOpenPath()
    {
        MazeCell startCell = _grid[0, 0];

        // Alapértelmezett (átlós), ha valami hiba lenne
        Quaternion targetRotation = Quaternion.Euler(0, 45, 0);

        // Itt most már mûködik a check, mert vártunk egy frame-et (yield return null)
        if (startCell.wallFront == null)
        {
            targetRotation = Quaternion.Euler(0, 0, 0);
        }
        else if (startCell.wallRight == null)
        {
            targetRotation = Quaternion.Euler(0, 90, 0);
        }

        playerObj.rotation = targetRotation;
    }

    //Ajtó
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