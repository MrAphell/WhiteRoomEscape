using UnityEngine;
using UnityEngine.UI;
using TMPro;
using StarterAssets;

// Ez a központi vezérlõ felel a játékos interakcióiért
public class InteractionController : MonoBehaviour
{
    [Header("Beállítások")]
    [SerializeField] private float _rayDistance = 3f;      // Milyen messzire ér el a játékos keze
    [SerializeField] private LayerMask _interactionLayer; // Melyik rétegen vannak az interaktív tárgyak
    [SerializeField] private Transform _cameraRoot;       // Honnan induljon a sugár

    [Header("UI")]
    [SerializeField] private Image _crosshair;           // Célkereszt képe
    [SerializeField] private TextMeshProUGUI _promptText; // Felugró szöveg helye
    [SerializeField] private GameObject _keypadUI;        // A számkódos panel felülete

    private FirstPersonController _fpsController;

    private void Start()
    {
        // Alaphelyzetben rögzítjük és elrejtjük az egérmutatót
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Eltároljuk a mozgásért felelõs komponenst
        _fpsController = GetComponent<FirstPersonController>();
    }

    private void Update()
    {
        // Ha nyitva van a kódbeütõ, nem akarunk közben másba belenyúlni
        if (_keypadUI != null && _keypadUI.activeSelf) return;

        // Folyamatosan nézzük, van-e valami elõttünk
        CheckForInteractable();
    }

    private void CheckForInteractable()
    {
        // Sugár indítása a kamera irányába
        Ray ray = new Ray(_cameraRoot.position, _cameraRoot.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, _rayDistance, _interactionLayer))
        {
            // Ha eltaláltunk valamit a rétegen, zöldre vált a célkereszt
            if (_crosshair != null) _crosshair.color = Color.green;

            // Megpróbáljuk lekérni az univerzális interakciós felületet a tárgyról
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();

            if (_promptText != null)
            {
                _promptText.gameObject.SetActive(true);
                // Ha van rajta IInteractable, lekérjük a saját szövegét, egyébként alap szöveg
                _promptText.text = (interactable != null) ? interactable.GetPrompt() : "Press [E] to interact";
            }

            // Ha megnyomjuk az 'E' gombot vagy a bal egérgombot, elindítjuk az interakciót
            if (interactable != null && (Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0)))
            {
                interactable.Interact();
            }
        }
        else
        {
            // Ha nem nézünk semmire, visszaállítjuk a célkeresztet és elrejtjük a szöveget
            if (_crosshair != null) _crosshair.color = Color.white;
            if (_promptText != null) _promptText.gameObject.SetActive(false);
        }
    }

    // Speciális kezelõ a Keypad megnyitásához (kikapcsolja a mozgást, bekapcsolja az egeret)
    public void OpenKeypad()
    {
        _keypadUI.SetActive(true);
        _fpsController.enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        _crosshair.enabled = false;
    }

    // Speciális kezelõ a Keypad bezárásához (visszaadja az irányítást a karakternek)
    public void CloseKeypad()
    {
        _keypadUI.SetActive(false);
        _fpsController.enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _crosshair.enabled = true;
    }
}