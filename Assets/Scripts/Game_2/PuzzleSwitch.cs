using UnityEngine;

public class PuzzleSwitch : MonoBehaviour
{
    [Header("Beállítások")]
    [SerializeField] private Material _onMaterial;
    [SerializeField] private Material _offMaterial;
    [Header("Állapot")]
    public bool isOn = false;

    private Renderer _renderer;
    private SwitchManager _manager;
    private void Start()
    {
        _renderer = GetComponent<Renderer>();
        _manager = FindAnyObjectByType<SwitchManager>();
        UpdateVisuals();
    }

    public void Toggle()
    {
        isOn = !isOn;
        UpdateVisuals();

        if (_manager != null) _manager.CheckSolution();
    }

    private void UpdateVisuals()
    {
        if (_renderer != null)
        {
            _renderer.material = isOn ? _onMaterial : _offMaterial;
        }
    }
}