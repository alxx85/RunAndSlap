using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HandCollect : MonoBehaviour
{
    [SerializeField] private ParticleSystem _vfxCollectHand;

    public event UnityAction<HandCollect> Collect;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerInteraction player))
        {
            Instantiate(_vfxCollectHand, transform.position, Quaternion.identity);
            Collect?.Invoke(this);
            Debug.Log("Pick up");
            Destroy(gameObject);
        }
    }
}
