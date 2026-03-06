using UnityEngine;
using TMPro;

// A játékos zseblámpájának ki- és bekapcsolását, hanghatását és UI jelzését kezelõ osztály
public class FlashlightController : MonoBehaviour
{
    [Header("Beállítások")]
    [SerializeField] private AudioClip _clickSound;      // A kapcsoláskor hallható hangeffekt
    [SerializeField] private TextMeshProUGUI _promptText; // Segédszöveg
    private Light _myLight;            // Referencia a fényforrás komponensre
    private AudioSource _audioSource;   // Referencia a hang lejátszóra

    private void Start()
    {
        _myLight = GetComponent<Light>();
        _audioSource = GetComponent<AudioSource>();

        // Kezdéskor a zseblámpa alapértelmezetten ki van kapcsolva
        if (_myLight != null)
        {
            _myLight.enabled = false;
        }

        UpdatePrompt(); // UI állapot frissítése az indításkor
    }

    private void Update()
    {
        // Az 'F' billentyû megnyomására váltunk a lámpa állapota között
        if (Input.GetKeyDown(KeyCode.F))
        {
            ToggleFlashlight();
        }
    }

    // A lámpa állapotának megfordítása és a visszajelzések kezelése
    private void ToggleFlashlight()
    {
        if (_myLight != null)
        {
            _myLight.enabled = !_myLight.enabled;

            // Kapcsolási hang lejátszása, ha be van állítva
            if (_audioSource != null && _clickSound != null)
            {
                _audioSource.PlayOneShot(_clickSound);
            }

            UpdatePrompt();
        }
    }

    // A képernyõn megjelenõ tipp/szöveg kezelése (csak akkor látszik, ha nincs fény)
    private void UpdatePrompt()
    {
        if (_promptText != null && _myLight != null)
        {
            _promptText.gameObject.SetActive(!_myLight.enabled);
        }
    }
}