using UnityEngine;

// Egyedi kapcsoló objektum, amely a logikai rejtvény részeként mûködik
public class PuzzleSwitch : MonoBehaviour, IInteractable
{
    public void Interact() => Toggle();
    public string GetPrompt() => isOn ? "Switch OFF" : "Switch ON";

    [Header("Beállítások")]
    [SerializeField] private Material _onMaterial;  // Bekapcsolt állapotban
    [SerializeField] private Material _offMaterial; // Kikapcsolt állapotban

    [Header("Állapot")]
    public bool isOn = false; // A kapcsoló aktuális logikai állapota

    private Renderer _renderer;
    private SwitchManager _manager;

    private void Start()
    {
        _renderer = GetComponent<Renderer>();
        // Megkeressük a jelenetben a rejtvényt ellenõrzõ központi scriptet
        _manager = FindAnyObjectByType<SwitchManager>();

        UpdateVisuals(); // Kezdõ állapot beállítása
    }

    // A játékos interakciójakor meghívódó váltó funkció
    public void Toggle()
    {
        isOn = !isOn;    // Állapot megfordítása
        UpdateVisuals(); // Megjelenés frissítése

        // Értesítjük a managert, hogy ellenõrizze a teljes rejtvény állását
        if (_manager != null) _manager.CheckSolution();
    }

    // A kapcsoló színének módosítása az állapot függvényében
    private void UpdateVisuals()
    {
        if (_renderer != null)
        {
            _renderer.material = isOn ? _onMaterial : _offMaterial;
        }
    }
}