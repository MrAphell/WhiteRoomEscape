using UnityEngine;

// Tárgyak két pont közötti sima mozgatását végzõ script
public class MovableObject : MonoBehaviour, IInteractable
{
    [Header("Mozgás Beállítások")]
    [SerializeField] private Vector3 _moveOffset = new Vector3(0, 0, 0); // Elmozdulás mértéke az indulási ponthoz képest
    [SerializeField] private float _speed = 2f;                         // Mozgás sebessége

    private Vector3 _startPosition;  // Kezdõpont mentése
    private Vector3 _targetPosition; // Végpont kiszámolt értéke
    private bool _isMoved = false;   // Épp el van-e mozdítva a tárgy?

    private void Start()
    {
        // Rögzítjük az alaphelyzetet és meghatározzuk a célpontot
        _startPosition = transform.position;
        _targetPosition = _startPosition + _moveOffset;
    }

    private void Update()
    {
        // Eldöntjük, melyik pont felé kell épp haladnia
        Vector3 destination = _isMoved ? _targetPosition : _startPosition;

        // Sima átmenet a jelenlegi helyzet és a célpont között
        transform.position = Vector3.Lerp(transform.position, destination, Time.deltaTime * _speed);
    }

    // Interfész: kiírja a célkereszthez, hogy mit fog tenni az interakció
    public string GetPrompt() => _isMoved ? "Press [E] to Reset" : "Press [E] to Move";

    // Interfész: megfordítja a mozgás irányát
    public void Interact() => _isMoved = !_isMoved;
}