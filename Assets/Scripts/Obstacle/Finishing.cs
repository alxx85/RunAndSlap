using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Finishing : MonoBehaviour
{
    [SerializeField] private ParticleSystem[] _vfxFinish;
    [SerializeField] private bool _playerStop;

    public UnityEvent Events;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerMovement player))
        {
            if (_playerStop == false)
                ActivateVFX();
            else
                player.Finished();
        }

        Events?.Invoke();
    }

    private void ActivateVFX()
    {
        foreach (var vfx in _vfxFinish)
        {
            vfx.Play();
        }
    }
}
