using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerBoosts : MonoBehaviour
{
    [SerializeField] private EnergyViewer _energy;
    [SerializeField] private PlayerInteraction _interaction;
    [SerializeField] private PlayerVFX _vfx;

    [SerializeField] private float _arrowSpeed;
    [SerializeField] private float _arrowDelay;
    
    [SerializeField] private float _slapSpeed;
    
    [SerializeField] private float _energySpeed;

    private Coroutine _arrowCoroutine;
    private Coroutine _slapCoroutine;
    private Coroutine _energyCoroutine;
    private Collider _collider;

    private float _currentSpeed = 0;

    public event UnityAction<float> ChangedSpeed;

    private void Start()
    {
        _energy.ReachedMaxEnergy += OnReachedMaxEnergy;
        _interaction.Slaped += OnSlaped;
        _collider = GetComponent<Collider>();
    }

    private void OnDisable()
    {
        _energy.ReachedMaxEnergy -= OnReachedMaxEnergy;
        _interaction.Slaped -= OnSlaped;
    }

    public void ActivateArrow()
    {
        if (_arrowCoroutine == null)
        {
            _arrowCoroutine = Activate(_arrowSpeed, _arrowDelay);
            OnSlaped();
        }
    }

    private void OnSlaped()
    {
        if (_slapCoroutine != null)
        {
            StopCoroutine(_slapCoroutine);
            
            if (_currentSpeed > 0)
                _currentSpeed -= _slapSpeed;
        }
        _slapCoroutine = Activate(_slapSpeed, _vfx.SlapDelay);
    }

    private void OnReachedMaxEnergy()
    {
        if (_energyCoroutine == null)
        {
            _energyCoroutine = Activate(_energySpeed, _vfx.EnergyDelay);
            _collider.isTrigger = false;
        }
    }

    private Coroutine Activate(float speed, float delay)
    {
        _currentSpeed += speed;
        Coroutine startedCoroutine = StartCoroutine(Deactivate(speed, delay));
        ChangedSpeed?.Invoke(_currentSpeed);
        return startedCoroutine;
    }

    private IEnumerator Deactivate(float speed, float delay)
    {
        yield return new WaitForSeconds(delay);
        _currentSpeed -= speed;
        ChangedSpeed?.Invoke(_currentSpeed);

        if (!_collider.isTrigger)
            _collider.isTrigger = true;
    }
}