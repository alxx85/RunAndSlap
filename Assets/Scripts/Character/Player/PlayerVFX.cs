using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVFX : MonoBehaviour
{
    [SerializeField] private EnergyViewer _energyViewer;
    [SerializeField] private ParticleSystem _run;
    [SerializeField] private ParticleSystem _energy;
    [SerializeField] private ParticleSystem _cloudBurst;

    private PlayerMovement _movement;
    private PlayerInteraction _interaction;

    public float SlapDelay => _run.main.duration;
    public float EnergyDelay => _energy.main.duration;

    private void Awake()
    {
        _movement = GetComponent<PlayerMovement>();
        _interaction = GetComponent<PlayerInteraction>();
        _interaction.Slaped += OnSlaped;
        _movement.Dying += OnDying;
    }

    private void OnEnable()
    {
        _energyViewer.ReachedMaxEnergy += OnReachedMaxEnergy;
    }

    private void OnDisable()
    {
        _energyViewer.ReachedMaxEnergy -= OnReachedMaxEnergy;
        _interaction.Slaped -= OnSlaped;
        _movement.Dying -= OnDying;
    }

    private void OnDying()
    {
        _run.Stop();
        _energy.Stop();
    }

    private void OnReachedMaxEnergy()
    {
        _energy.Play();
        Vector3 position = transform.position;
        Instantiate(_cloudBurst, position, Quaternion.identity);
    }

    private void OnSlaped()
    {
        _run.Play();
    }
}
