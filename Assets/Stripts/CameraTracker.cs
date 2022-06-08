using UnityEngine;
using DG.Tweening;

public class CameraTracker : MonoBehaviour
{
    [SerializeField] private PlayerMovement _player;
    [SerializeField] private float _acceleration;

    private Vector3 _offset;

    private void Start()
    {
        _offset = transform.position - _player.transform.position;
    }

    private void Update()
    {
        transform.DOMove(_player.transform.position + _offset, _acceleration);
    }
}
