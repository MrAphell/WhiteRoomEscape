using TMPro;
using UnityEngine;

// Számkódos beléptetõ rendszer, amely ajtót nyit és kezeli a játékos haladását
public class KeypadSystem : MonoBehaviour, IInteractable
{
    [Header("Beállítások")]
    [SerializeField] private string _correctCode = "0000"; // A helyes megoldás
    [SerializeField] private TextMeshProUGUI _displayText;   // A kód kijelzõje

    [Header("Ajtó Rendszer")]
    [SerializeField] private GameObject _lockedDoor;      // A zárt ajtó modellje
    [SerializeField] private GameObject _openDoorObject;   // A nyitott ajtó modellje

    [Header("UI")]
    [SerializeField] private GameObject _uiPanel;         // A teljes grafikus felület

    private string _currentInput = ""; // Az eddig beírt számok

    public void Interact()
    {
        InteractionController controller = FindAnyObjectByType<InteractionController>();
        if (controller != null) controller.OpenKeypad();
    }

    public string GetPrompt() => "Press [E] to use Keypad";

    private void Update()
    {
        // Csak akkor figyeljük a billentyûket, ha látható a panel
        if (!_uiPanel.activeSelf) return;
        HandleKeyboardInput();
    }

    // Fizikai billentyûzet gombjainak figyelése
    private void HandleKeyboardInput()
    {
        for (int i = 0; i <= 9; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha0 + i) || Input.GetKeyDown(KeyCode.Keypad0 + i))
                AddDigit(i.ToString());
        }
        if (Input.GetKeyDown(KeyCode.Backspace)) DeleteLastDigit();
        if (Input.GetKeyDown(KeyCode.Escape)) ClosePanel();
    }

    // Új szám hozzáadása a beírt kódhoz
    public void AddDigit(string digit)
    {
        if (_currentInput.Length >= 4) return;
        _currentInput += digit;
        UpdateDisplay();
        CheckCode();
    }

    // A beírt kód ellenõrzése
    private void CheckCode()
    {
        if (_currentInput == _correctCode)
        {
            _displayText.text = "SUCCESS";
            _displayText.color = Color.green;

            // Kinyitjuk a kijárathoz vezetõ ajtót
            if (_openDoorObject != null) _openDoorObject.SetActive(true);
            if (_lockedDoor != null) _lockedDoor.SetActive(false);

            // TÖRÖLTÜK A GAMEMANAGER HÍVÁST! 
            // A mentést majd az Exit_To_Hub ajtón lévõ LevelEntrance script végzi el,
            // miután a játékos átment a kinyílt ajtón.

            Invoke("ClosePanel", 1f); // Egy másodperc múlva bezárjuk a panelt
        }
        else if (_currentInput.Length == 4)
        {
            // Hibás kód esetén életlevonás és reset
            _displayText.text = "ERROR";
            _displayText.color = Color.red;
            _currentInput = "";
            GameManager.LoseLife();
        }
    }

    // Kijelzõ frissítése a beírt karakterekkel
    private void UpdateDisplay() { if (_displayText.text != "SUCCESS" && _displayText.text != "ERROR") _displayText.text = _currentInput; }

    // Panel bezárása és az irányítás visszaadása a karakternek (EZ LEGYEN PUBLIC!)
    public void ClosePanel()
    {
        InteractionController controller = FindAnyObjectByType<InteractionController>();
        if (controller != null) controller.CloseKeypad();
        else _uiPanel.SetActive(false);
    }

    // Utolsó karakter törlése (EZ IS LEGYEN PUBLIC!)
    public void DeleteLastDigit()
    {
        if (_currentInput.Length > 0)
        {
            _currentInput = _currentInput.Substring(0, _currentInput.Length - 1);
            UpdateDisplay();
        }
    }
}