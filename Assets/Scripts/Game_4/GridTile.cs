using UnityEngine;
using System.Collections;

public class GridTile : MonoBehaviour
{
    public enum TileType { Safe, Trap, Goal }
    public enum TileState { Hidden, Revealed, Error }

    [Header("Állapotok")]
    public TileType type = TileType.Trap;
    public TileState state = TileState.Hidden;

    [Header("Megjelenés")]
    [SerializeField] private MeshRenderer _renderer;
    private Color _originalColor;

    // Koordináták a rácsban (a Managernek kell)
    public int x, y;

    private void Awake()
    {
        _renderer = GetComponent<MeshRenderer>();
        _originalColor = _renderer.material.color;
    }

    // Beállítja a csempe típusát (a Manager hívja meg)
    public void Setup(int xCoord, int yCoord, TileType tType)
    {
        x = xCoord;
        y = yCoord;
        type = tType;
        ResetTile();
    }

    // Vizuális felvillantás a pálya elején
    public void Flash(Color flashColor, float duration)
    {
        if (type == TileType.Safe || type == TileType.Goal)
        {
            StartCoroutine(FlashRoutine(flashColor, duration));
        }
    }

    private IEnumerator FlashRoutine(Color color, float duration)
    {
        _renderer.material.color = color;
        yield return new WaitForSeconds(duration);
        if (state == TileState.Hidden) _renderer.material.color = _originalColor;
    }

    // Amikor a játékos rálép (Trigger)
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Jelentjük a központnak, hogy ránk léptek
            FloorManager.Instance.OnTileStepped(this);
        }
    }

    public void SetRevealed()
    {
        state = TileState.Revealed;
        _renderer.material.color = Color.green;
    }

    public void SetError()
    {
        state = TileState.Error;
        _renderer.material.color = Color.red;
    }

    public void ResetTile()
    {
        state = TileState.Hidden;
        _renderer.material.color = _originalColor;
    }
}