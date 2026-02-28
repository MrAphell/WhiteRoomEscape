using UnityEngine;

public class EnergyDoor : MonoBehaviour
{
    [Header("Ajtó Beállítások")]
    public string doorName = "Door";
    public int energyCost = 4;

    [Header("Nyitás Beállítások")]
    public Transform hinge;        // "Door_Hinge" objeltum!
    public float openAngle = 90f;  // Nyílás irány (lehet -90 is, hogy befelé nyíljon)
    public float openSpeed = 5f;   // Nyitás sebessége

    private bool _isOpen = false;
    private Quaternion _closedRotation;
    private Quaternion _openRotation;

    void Start()
    {
        if (hinge != null)
        {
            // Eltároljuk az alap (zárt) és a nyitott szöget
            _closedRotation = hinge.localRotation;
            _openRotation = Quaternion.Euler(hinge.localEulerAngles + new Vector3(0, openAngle, 0));
        }
        else
        {
            Debug.LogError("HIBA: Nem állítottad be a Hinge (Zsanér) objektumot az ajtónál: " + gameObject.name);
        }
    }

    void Update()
    {
        if (hinge == null) return;

        // Folyamatos, sima forgatás a Lerp segítségével
        if (_isOpen)
        {
            hinge.localRotation = Quaternion.Lerp(hinge.localRotation, _openRotation, Time.deltaTime * openSpeed);
        }
        else
        {
            hinge.localRotation = Quaternion.Lerp(hinge.localRotation, _closedRotation, Time.deltaTime * openSpeed);
        }
    }

    public string GetDoorInfo()
    {
        if (!_isOpen) return doorName + "\n[E] Open (-" + energyCost + " Energy)";
        else return doorName + "\n[E] Close (+" + energyCost + " Energy back)";
    }

    public void ToggleDoor()
    {
        if (!_isOpen)
        {
            // Nyitás és energiafogyasztás
            if (EnergyManager.Instance.TryConsumeEnergy(energyCost))
            {
                _isOpen = true;
            }
            else
            {
                EnergyManager.Instance.ShowInteraction("Not enough energy!");
            }
        }
        else
        {
            // Zárás és visszatérítés
            EnergyManager.Instance.RestoreEnergy(energyCost);
            _isOpen = false;
        }
    }
}