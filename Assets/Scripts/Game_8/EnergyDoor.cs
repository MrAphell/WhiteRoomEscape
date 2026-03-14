using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

// Energiafogyasztással mûködõ, zsanéron forduló ajtómechanika, ami kijárat is lehet
public class EnergyDoor : MonoBehaviour, IInteractable
{
    public void Interact() => ToggleDoor();
    public string GetPrompt() => GetDoorInfo();

    [Header("Ajtó Beállítások")]
    public string doorName = "Ajtó";
    public int energyCost = 4; // Mennyi energiába kerül kinyitni

    [Header("Nyitás Beállítások")]
    public Transform hinge;        // Az objektum, ami körül az ajtó elfordul (zsanér)
    public float openAngle = 90f;  // Mekkora szögben nyíljon ki
    public float openSpeed = 5f;   // Milyen gyorsan mozogjon az ajtó

    [Header("Kijárat Beállítások (Opcionális)")]
    [Tooltip("Pipáld be, ha ez a pálya legutolsó ajtaja, ami visszavisz a Hub-ba!")]
    public bool isLevelExit = false;
    public string sceneToLoad = "MainHub";

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
    }

    void Update()
    {
        if (hinge == null) return;

        // Sima átmenet a két állapot (nyitott/zárt) között
        Quaternion target = _isOpen ? _openRotation : _closedRotation;
        hinge.localRotation = Quaternion.Lerp(hinge.localRotation, target, Time.deltaTime * openSpeed);
    }

    // Szöveg a képernyõn
    public string GetDoorInfo()
    {
        if (!_isOpen) return doorName + "\n[E] Open (-" + energyCost + " Energy)";
        else if (isLevelExit) return ""; // Ha kijárat és kinyílt, már ne írjon ki semmit, mert mindjárt tölt a pálya
        else return doorName + "\n[E] Close (+" + energyCost + " Energy back)";
    }

    // Az ajtó mûködtetéséért felelõs logika
    public void ToggleDoor()
    {
        if (!_isOpen)
        {
            // Nyitáskor ellenõrizzük az energiát
            if (EnergyManager.Instance.TryConsumeEnergy(energyCost))
            {
                _isOpen = true;

                // HA EZ EGY KIJÁRAT, INDÍTSUK EL A KILÉPÉSI FOLYAMATOT!
                if (isLevelExit)
                {
                    StartCoroutine(ExitLevelRoutine());
                }
            }
            else
            {
                EnergyManager.Instance.ShowInteraction("Not enough energy!");
            }
        }
        else
        {
            // Ha ez egy kijárat, ne lehessen visszacsukni (mert már úgyis tölt a Hub)
            if (isLevelExit) return;

            // Sima ajtónál visszakapjuk az energiát záráskor
            EnergyManager.Instance.RestoreEnergy(energyCost);
            _isOpen = false;
        }
    }

    private IEnumerator ExitLevelRoutine()
    {
        yield return new WaitForSeconds(0.5f);

        string currentSceneName = SceneManager.GetActiveScene().name;

        // Pontszám/Idõ mentése a GameManageren keresztül
        int levelNumber = GameManager.GetLevelNumberFromScene(currentSceneName);
        GameManager.CompleteLevel(levelNumber, currentSceneName);
        Debug.Log($"Pálya teljesítve! Adatok beküldve: {currentSceneName}.");

        // Hub pozíció mentése
        PlayerPrefs.SetString("LastScene", currentSceneName);
        PlayerPrefs.Save();

        // Pályaváltás
        SceneManager.LoadScene(sceneToLoad);
    }
}