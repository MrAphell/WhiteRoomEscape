using UnityEngine;
using TMPro;

public class SwitchManager : MonoBehaviour
{
    [Header("A 4 Kapcsoló")]
    public PuzzleSwitch switch1;
    public PuzzleSwitch switch2;
    public PuzzleSwitch switch3;
    public PuzzleSwitch switch4;

    [Header("A Helyes Megoldás (Pipáld be ami legyen ON)")]
    public bool solve1;
    public bool solve2;
    public bool solve3;
    public bool solve4;

    [Header("Jutalom")]
    [SerializeField] private GameObject _codeDisplayObject;

    public void CheckSolution()
    {
        bool isSolved = (switch1.isOn == solve1 &&
                         switch2.isOn == solve2 &&
                         switch3.isOn == solve3 &&
                         switch4.isOn == solve4);

        if (isSolved)
        {
            Debug.Log("REJTVÉNY MEGOLDVA!");
            if (_codeDisplayObject != null) _codeDisplayObject.SetActive(true);
        }
        else
        {
            if (_codeDisplayObject != null) _codeDisplayObject.SetActive(false);
        }
    }
}