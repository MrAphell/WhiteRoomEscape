using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using StarterAssets;

public class InteractionController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float _rayDistance = 3f;
    [SerializeField] private LayerMask _interactionLayer;
    [SerializeField] private Transform _cameraRoot;

    [Header("UI")]
    [SerializeField] private Image _crosshair;
    [SerializeField] private TextMeshProUGUI _promptText;
    [SerializeField] private GameObject _keypadUI;

    private FirstPersonController _fpsController;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        _fpsController = GetComponent<FirstPersonController>();
    }

    private void Update()
    {
        if (_keypadUI != null && _keypadUI.activeSelf) return;

        CheckForInteractable();
    }

    private void CheckForInteractable()
    {
        Ray ray = new Ray(_cameraRoot.position, _cameraRoot.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, _rayDistance, _interactionLayer))
        {
            if (_crosshair != null) _crosshair.color = Color.green;
            if (_promptText != null)
            {
                _promptText.gameObject.SetActive(true);
                _promptText.text = "Press [E] to use ";
            }

            if (Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0))
            {
                PuzzleSwitch pSwitch = hit.collider.GetComponent<PuzzleSwitch>();
                if (pSwitch != null)
                {
                    pSwitch.Toggle();
                    return;
                }

                MovableObject movable = hit.collider.GetComponent<MovableObject>();
                if (movable != null)
                {
                    movable.Interact();
                    return;
                }

                LevelEntrance entrance = hit.collider.GetComponent<LevelEntrance>();
                if (entrance != null)
                {
                    entrance.EnterLevel();
                    return;
                }

                GameExit exit = hit.collider.GetComponent<GameExit>();
                if (exit != null)
                {
                    exit.QuitGame();
                    return;
                }

                InteractWithSpecialObjects(hit.collider.name);
            }
        }
        else
        {
            if (_crosshair != null) _crosshair.color = Color.white;
            if (_promptText != null) _promptText.gameObject.SetActive(false);
        }
    }

    private void InteractWithSpecialObjects(string objectName)
    {
        switch (objectName)
        {
            case "Keypad_Trigger":
                OpenKeypad();
                break;

            case "Locked_Door":
                Debug.Log("Ez zárva van. Használd a Keypadet!");
                if (_promptText != null) _promptText.text = "LOCKED - Find the Code!";
                break;
        }
    }

    private void OpenKeypad()
    {
        if (_keypadUI != null)
        {
            _keypadUI.SetActive(true);
            if (_fpsController != null) _fpsController.enabled = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            if (_crosshair != null) _crosshair.enabled = false;
        }
    }

    public void CloseKeypad()
    {
        if (_keypadUI != null)
        {
            _keypadUI.SetActive(false);
            if (_fpsController != null) _fpsController.enabled = true;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            if (_crosshair != null) _crosshair.enabled = true;
        }
    }
}