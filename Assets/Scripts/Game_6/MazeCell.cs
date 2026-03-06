using UnityEngine;

// A labirintus egyetlen cellájának szerkezetét és falait kezelõ osztály
public class MazeCell : MonoBehaviour
{
    [Header("Falak Referenciája")]
    public GameObject wallFront; // Északi fal
    public GameObject wallBack;  // Déli fal
    public GameObject wallLeft;  // Nyugati fal
    public GameObject wallRight; // Keleti fal

    // Ezt a függvényt hívja meg a MazeGenerator, amikor utat tör a labirintusban
    public void RemoveWall(int direction)
    {
        // Az irányok számozása megegyezik a generátorban használt konstansokkal
        // 1: Front (Elõre), 2: Back (Hátra), 3: Left (Bal), 4: Right (Jobb)
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