using UnityEngine;

// Energiafogyasztással mûködõ, zsanéron forduló ajtómechanika
public class EnergyDoor : MonoBehaviour, IInteractable
{
    // Az interfész kötelezõ elemei: az interakció elindítása és a kiírandó szöveg
    public void Interact() => ToggleDoor();
    public string GetPrompt() => GetDoorInfo();

    [Header("Ajtó Beállítások")]
    public string doorName = "Ajtó";
    public int energyCost = 4; // Mennyi energiába kerül kinyitni

    [Header("Nyitás Beállítások")]
    public Transform hinge;        // Az objektum, ami körül az ajtó elfordul (zsanér)
    public float openAngle = 90f;  // Mekkora szögben nyíljon ki
    public float openSpeed = 5f;   // Milyen gyorsan mozogjon az ajtó

    private bool _isOpen = false;
    private Quaternion _closedRotation;
    private Quaternion _openRotation;

    void Start()
    {
        if (hinge != null)
        {
            // Kezdéskor elmentjük a zárt és kiszámoljuk a nyitott állapot forgási adatait
            _closedRotation = hinge.localRotation;
            _openRotation = Quaternion.Euler(hinge.localEulerAngles + new Vector3(0, openAngle, 0));
        }
        else
        {
            Debug.LogError("HIBA: Nincs zsanér (Hinge) hozzárendelve az ajtóhoz: " + gameObject.name);
        }
    }

    void Update()
    {
        if (hinge == null) return;

        // Sima átmenet a két állapot (nyitott/zárt) között a célkeresztnek megfelelõen
        Quaternion target = _isOpen ? _openRotation : _closedRotation;
        hinge.localRotation = Quaternion.Lerp(hinge.localRotation, target, Time.deltaTime * openSpeed);
    }

    // Segédfüggvény: megmutatja a játékosnak, hogy a nyitás/zárás mennyi energiát igényel vagy ad vissza
    public string GetDoorInfo()
    {
        if (!_isOpen) return doorName + "\n[E] Open (-" + energyCost + " Energy)";
        else return doorName + "\n[E] Close (+" + energyCost + " Energy back)";
    }

    // Az ajtó mûködtetéséért felelõs logika
    public void ToggleDoor()
    {
        if (!_isOpen)
        {
            // Nyitáskor ellenõrizzük, van-e elég energiánk a központi EnergyManager-ben
            if (EnergyManager.Instance.TryConsumeEnergy(energyCost))
            {
                _isOpen = true;
            }
            else
            {
                // Ha nincs elég energia, hibaüzenetet villantunk fel
                EnergyManager.Instance.ShowInteraction("Not enough energy!");
            }
        }
        else
        {
            // Záráskor visszakapjuk az energiát, amit a nyitáshoz használtunk fel
            EnergyManager.Instance.RestoreEnergy(energyCost);
            _isOpen = false;
        }
    }
}