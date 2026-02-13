using UnityEngine;

public class MazeCell : MonoBehaviour
{
    [Header("Falak Referenciája")]
    public GameObject wallFront;
    public GameObject wallBack;
    public GameObject wallLeft;
    public GameObject wallRight;

    // Ezt hívja majd a generátor, hogy eltüntesse a falat
    public void RemoveWall(int direction)
    {
        // 1: Front, 2: Back, 3: Left, 4: Right
        switch (direction)
        {
            case 1: if (wallFront) wallFront.SetActive(false); break;
            case 2: if (wallBack) wallBack.SetActive(false); break;
            case 3: if (wallLeft) wallLeft.SetActive(false); break;
            case 4: if (wallRight) wallRight.SetActive(false); break;
        }
    }
}