using UnityEngine;
using System.Collections;

public class MemoryButton : MonoBehaviour
{
    [Header("Beállítások")]
    public int buttonID;
    public Color normalColor;
    public Color flashColor;
    public float flashDuration = 0.5f;

    [Header("Referencia")]
    public SimonManager gameManager;

    private Renderer _rend;
    private bool _isFlashing = false;

    private void Start()
    {
        _rend = GetComponent<Renderer>();
        _rend.material.color = normalColor;
    }

    public void OnInteract()
    {
        if (_isFlashing) return;

        gameManager.PlayerPressedButton(buttonID);

        StartCoroutine(FlashRoutine());
    }

    public void ComputerActivate()
    {
        StartCoroutine(FlashRoutine());
    }

    private IEnumerator FlashRoutine()
    {
        _isFlashing = true;
        _rend.material.color = flashColor;

        yield return new WaitForSeconds(flashDuration);

        _rend.material.color = normalColor;
        _isFlashing = false;
    }
}