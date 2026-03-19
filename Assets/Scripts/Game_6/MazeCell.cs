using UnityEngine;

// A labirintus egyetlen cellájának szerkezetét és falait kezelõ osztály
public class MazeCell : MonoBehaviour
{
    [Header("Falak Referenciája")]
    public GameObject wallFront; // Elülsõ fal
    public GameObject wallBack;  // Hátulsó fal
    public GameObject wallLeft;  // Bal fal
    public GameObject wallRight; // Jobb fal

    // Ezt a függvényt hívja meg a MazeGenerator, amikor utat tör a labirintusban
    public void RemoveWall(int direction)
    {
        // Az irányok számozása megegyezik a generátorban használt konstansokkalS
        switch (direction)
        {
            case 1:
                if (wallFront) wallFront.SetActive(false);
                break;
            case 2:
                if (wallBack) wallBack.SetActive(false);
                break;
            case 3:
                if (wallLeft) wallLeft.SetActive(false);
                break;
            case 4:
                if (wallRight) wallRight.SetActive(false);
                break;
        }
    }
}