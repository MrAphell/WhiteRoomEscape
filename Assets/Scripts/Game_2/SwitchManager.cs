using UnityEngine;
using TMPro;

// A kapcsolókból álló logikai rejtvény állapotát és megoldását ellenõrzõ osztály
public class SwitchManager : MonoBehaviour
{
    [Header("A 4 Kapcsoló")]
    public PuzzleSwitch switch1;
    public PuzzleSwitch switch2;
    public PuzzleSwitch switch3;
    public PuzzleSwitch switch4;

    [Header("A Helyes Megoldás (Pipáld be ami legyen ON)")]
    // Az Inspectorban itt állítható be a rejtvény kulcsa (melyik kapcsoló álljon fel/le)
    public bool solve1;
    public bool solve2;
    public bool solve3;
    public bool solve4;

    [Header("Jutalom")]
    [SerializeField] private GameObject _codeDisplayObject; // Az objektum, ami a helyes kód felett jelenik meg (pl. szöveg)

    // Minden egyes kapcsolóváltáskor meghívjuk ezt az ellenõrzést
    public void CheckSolution()
    {
        // Összehasonlítjuk az összes kapcsoló aktuális állapotát a várt megoldással
        bool isSolved = (switch1.isOn == solve1 &&
                         switch2.isOn == solve2 &&
                         switch3.isOn == solve3 &&
                         switch4.isOn == solve4);

        if (isSolved)
        {
            // SIKER: Megjelenítjük a kódot vagy a kijáratot
            Debug.Log("REJTVÉNY MEGOLDVA!");
            if (_codeDisplayObject != null) _codeDisplayObject.SetActive(true);
        }
        else
        {
            // Ha elállítják a helyes sorrendet, a kód ismét eltûnik
            if (_codeDisplayObject != null) _codeDisplayObject.SetActive(false);
        }
    }
}