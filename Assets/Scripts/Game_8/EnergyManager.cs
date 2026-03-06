using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

// A játékos korlátozott erõforrásait (energia) és az ehhez kapcsolódó UI-t kezelõ rendszer
public class EnergyManager : MonoBehaviour
{
    // Singleton példány a globális eléréshez (pl. ajtónyitásnál)
    public static EnergyManager Instance;

    [Header("Energia Beállítások")]
    public int maxEnergy = 10; // Maximális kapacitás
    private int _currentEnergy; // Aktuális energiaszint

    [Header("UI Kijelzõk")]
    public TextMeshProUGUI energyText;      // Az energiaszint szöveges megjelenítõje
    public TextMeshProUGUI interactionText; // Interakciós üzenetek (pl. hibaüzenet)

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

    // Energia levonásának megkísérlése (pl. ajtó kinyitásakor hívódik meg)
    public bool TryConsumeEnergy(int amount)
    {
        // Sikeres levonás, ha van elég egység
        if (_currentEnergy >= amount)
        {
            _currentEnergy -= amount;
            UpdateEnergyUI();

            // Ha elfogyott minden forrás, a pálya automatikusan újraindul
            if (_currentEnergy <= 0)
            {
                Debug.Log("Elfogyott az energia! Pálya újraindítása...");
                RestartLevel();
            }

            return true;
        }

        // Ha nincs elég energia, hibaüzenet és kényszerített újraindítás (büntetés)
        ShowInteraction("Not enough energy!");
        Debug.Log("Kevés az energia a nyitáshoz! Pálya újraindítása...");
        RestartLevel();

        return false;
    }

    // Energia visszatöltése (pl. töltõállomásokhoz vagy bónuszokhoz)
    public void RestoreEnergy(int amount)
    {
        _currentEnergy += amount;
        if (_currentEnergy > maxEnergy) _currentEnergy = maxEnergy;
        UpdateEnergyUI();
    }

    // Az UI szöveg és szín frissítése (kritikus szintnél piros jelzés)
    private void UpdateEnergyUI()
    {
        if (energyText != null)
        {
            energyText.text = "Energy: " + _currentEnergy + " / " + maxEnergy;

            // Vizuális figyelmeztetés 3 egység alatt
            energyText.color = _currentEnergy <= 3 ? Color.red : Color.green;
        }
    }

    // Szöveges üzenet megjelenítése a képernyõn
    public void ShowInteraction(string message)
    {
        if (interactionText != null)
        {
            interactionText.text = message;
            interactionText.gameObject.SetActive(true);
        }
    }

    // Üzenet elrejtése
    public void HideInteraction()
    {
        if (interactionText != null)
        {
            interactionText.gameObject.SetActive(false);
        }
    }

    // Az aktuális jelenet újratöltése hiba vagy energiahiány esetén
    private void RestartLevel()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
}