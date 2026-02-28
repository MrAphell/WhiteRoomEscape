using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class EnergyManager : MonoBehaviour
{
    public static EnergyManager Instance;

    [Header("Energia Beállítások")]
    public int maxEnergy = 10;
    private int _currentEnergy;

    [Header("UI Kijelzõk")]
    public TextMeshProUGUI energyText;
    public TextMeshProUGUI interactionText;

    void Awake()
    {
        if (Instance == null) Instance = this;
    }

    void Start()
    {
        _currentEnergy = maxEnergy;
        UpdateEnergyUI();
        HideInteraction();
    }

    public bool TryConsumeEnergy(int amount)
    {
        // Ha van elég energia a nyitáshoz
        if (_currentEnergy >= amount)
        {
            _currentEnergy -= amount;
            UpdateEnergyUI();

            // Pont 0
            if (_currentEnergy <= 0)
            {
                Debug.Log("Elfogyott az energia! Pálya újraindítása...");
                RestartLevel();
            }

            return true;
        }

        // Ha nincs elég energia
        ShowInteraction("Not enough energy!");

        Debug.Log("Kevés az energia a nyitáshoz! Pálya újraindítása...");
        RestartLevel(); // Büntetés és újraindítás!

        return false;
    }

    public void RestoreEnergy(int amount)
    {
        _currentEnergy += amount;
        if (_currentEnergy > maxEnergy) _currentEnergy = maxEnergy;
        UpdateEnergyUI();
    }

    private void UpdateEnergyUI()
    {
        if (energyText != null)
        {
            energyText.text = "Energy: " + _currentEnergy + " / " + maxEnergy;

            // Pirosan villog, ha már nagyon kevés van (3 vagy kevesebb)
            energyText.color = _currentEnergy <= 3 ? Color.red : Color.green;
        }
    }

    public void ShowInteraction(string message)
    {
        if (interactionText != null)
        {
            interactionText.text = message;
            interactionText.gameObject.SetActive(true);
        }
    }

    public void HideInteraction()
    {
        if (interactionText != null)
        {
            interactionText.gameObject.SetActive(false);
        }
    }

    // --- ÚJ RÉSZ: Pálya újraindítása ---
    private void RestartLevel()
    {
        // Újratölti az éppen aktív pályát (Game_8)
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);

        // Opcionális: Ha az életeket is akarod csökkenteni, vedd ki a kommentet az alábbi sor elõl:
        // GameManager.LoseLife();
    }
}