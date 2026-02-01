using UnityEngine;
using TMPro;

public class FlashlightController : MonoBehaviour
{
    [Header("Beállítások")]
    [SerializeField] private AudioClip _clickSound;
    [SerializeField] private TextMeshProUGUI _promptText;

    private Light _myLight;
    private AudioSource _audioSource;

    private void Start()
    {
        _myLight = GetComponent<Light>();
        _audioSource = GetComponent<AudioSource>();

        if (_myLight != null)
        {
            _myLight.enabled = false;
        }

        UpdatePrompt();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            ToggleFlashlight();
        }
    }

    private void ToggleFlashlight()
    {
        if (_myLight != null)
        {
            _myLight.enabled = !_myLight.enabled;

            if (_audioSource != null && _clickSound != null)
            {
                _audioSource.PlayOneShot(_clickSound);
            }

            UpdatePrompt();
        }
    }

    private void UpdatePrompt()
    {
        if (_promptText != null && _myLight != null)
        {
            _promptText.gameObject.SetActive(!_myLight.enabled);
        }
    }
}