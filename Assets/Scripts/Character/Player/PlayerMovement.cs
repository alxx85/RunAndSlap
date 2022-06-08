using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PlayerInteraction), typeof(PlayerBoosts))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private InputScreen _input;
    [SerializeField] private float _moveSpeed;

    private PlayerInteraction _interaction;
    private PlayerBoosts _boost;
    private float _boostSpeed = 0f;
    private float _currentAngle = 0f;
    private float _angleRotate = 25f;
    private float _rotateSpeed = 1.5f;

    public event UnityAction Dying;

    public float Speed => _moveSpeed;

    private void Start()
    {
        _interaction = GetComponent<PlayerInteraction>();
        _boost = GetComponent<PlayerBoosts>();
        _input.ChangedPosition += OnChangedPosition;
        _boost.ChangedSpeed += OnChangedSpeed;
    }

    private void OnDisable()
    {
        _input.ChangedPosition -= OnChangedPosition;
        _boost.ChangedSpeed -= OnChangedSpeed;
    }

    private void Update()
    {
        if (_interaction.IsAlive == false)
        {
            Dying?.Invoke();
            enabled = false;
        }

        float fullSpeed = (_moveSpeed + _boostSpeed) * Time.deltaTime;
        float _finishPosition = transform.position.z + _moveSpeed;
        float forwardPosition = Mathf.MoveTowards(transform.position.z, _finishPosition, fullSpeed);
        transform.position = new Vector3(transform.position.x, transform.position.y, forwardPosition);
        ChangeRotation(0);
    }

    private void OnChangedSpeed(float speed)
    {
        _boostSpeed = speed;
    }

    public void Finished()
    {
        _moveSpeed = 0;
        _boostSpeed = 0;
    }

    private void OnChangedPosition(float changedPosition, float delta)
    {
        Vector3 position = transform.position;
        Vector3 newPosition = new Vector3(changedPosition, position.y, position.z);
        float distance = position.x - changedPosition;

        if (Mathf.Abs(distance) > .5f)
        {
            float angle = delta > 0 ? 1 : -1;
            ChangeRotation(angle);
        }

        transform.position = newPosition;
    }

    private void ChangeRotation(float direction)
    {
        if (direction == 0)
        {
            _currentAngle = Mathf.MoveTowards(_currentAngle, 0, _rotateSpeed);
        }
        else
        {
            _currentAngle = direction * _angleRotate;
        }

        transform.localRotation = Quaternion.Euler(0, _currentAngle, 0);
    }
}