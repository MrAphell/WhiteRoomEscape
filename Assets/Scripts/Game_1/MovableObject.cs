using UnityEngine;

public class MovableObject : MonoBehaviour
{
    [Header("Mozgás Beállítások")]
    [SerializeField] private Vector3 _moveOffset = new Vector3(0, 0, 0);
    [SerializeField] private float _speed = 2f;

    private Vector3 _startPosition;
    private Vector3 _targetPosition;
    private bool _isMoved = false;

    private void Start()
    {
        _startPosition = transform.position;
        _targetPosition = _startPosition + _moveOffset;
    }

    private void Update()
    {
        Vector3 destination = _isMoved ? _targetPosition : _startPosition;
        transform.position = Vector3.Lerp(transform.position, destination, Time.deltaTime * _speed);
    }

    public void Interact()
    {
        _isMoved = !_isMoved;

    }
}