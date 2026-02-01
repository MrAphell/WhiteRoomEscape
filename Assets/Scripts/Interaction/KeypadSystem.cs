using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KeypadSystem : MonoBehaviour
{
    [Header("Beállítások")]
    [SerializeField] private string _correctCode = "0000";
    [SerializeField] private TextMeshProUGUI _displayText;

    [Header("Ajtó Rendszer")]
    [SerializeField] private GameObject _lockedDoor;
    [SerializeField] private GameObject _openDoorObject;

    [Header("UI")]
    [SerializeField] private GameObject _uiPanel;

    private string _currentInput = "";

    private void Update()
    {
        if (!_uiPanel.activeSelf) return;
        HandleKeyboardInput();
    }

    private void HandleKeyboardInput()
    {
        for (int i = 0; i <= 9; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha0 + i) || Input.GetKeyDown(KeyCode.Keypad0 + i))
            {
                AddDigit(i.ToString());
            }
        }

        if (Input.GetKeyDown(KeyCode.Backspace)) DeleteLastDigit();
        if (Input.GetKeyDown(KeyCode.Escape)) ClosePanel();
    }

    private void DeleteLastDigit()
    {
        if (_currentInput.Length > 0)
        {
            _currentInput = _currentInput.Substring(0, _currentInput.Length - 1);
            UpdateDisplay();
        }
    }

    public void AddDigit(string digit)
    {
        if (_currentInput.Length >= 4) return;
        _currentInput += digit;
        UpdateDisplay();
        CheckCode();
    }

    public void ClearInput()
    {
        _currentInput = "";
        _displayText.text = "ENTER CODE";
        _displayText.color = Color.white;
    }

    private void CheckCode()
    {
        if (_currentInput == _correctCode)
        {
            _displayText.text = "SUCCESS";
            _displayText.color = Color.green;

            if (_openDoorObject != null) _openDoorObject.SetActive(true);
            if (_lockedDoor != null) _lockedDoor.SetActive(false);

            int levelNum = 1;
            string sceneName = SceneManager.GetActiveScene().name;

            string numberPart = System.Text.RegularExpressions.Regex.Match(sceneName, @"\d+").Value;
            if (!string.IsNullOrEmpty(numberPart))
            {
                levelNum = int.Parse(numberPart);
            }

            GameManager.CompleteLevel(levelNum);

            Invoke("ClosePanel", 1f);
        }
        else if (_currentInput.Length == 4)
        {
            _displayText.text = "ERROR";
            _displayText.color = Color.red;
            _currentInput = "";

            GameManager.LoseLife();
        }
    }

    private void UpdateDisplay()
    {
        if (_displayText.text != "SUCCESS" && _displayText.text != "ERROR")
        {
            _displayText.text = _currentInput;
        }
    }

    private void ClosePanel()
    {
        InteractionController controller = FindAnyObjectByType<InteractionController>();
        if (controller != null)
        {
            controller.CloseKeypad();
        }
        else
        {
            _uiPanel.SetActive(false);
        }
    }
}