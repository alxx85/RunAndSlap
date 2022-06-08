using UnityEngine;
using UnityEngine.Events;

public class ActivateThorn : MonoBehaviour
{
    [SerializeField] private ParticleSystem _vfxButtonDown;

    private float _downPosition = -.1f;

    public event UnityAction ButtonDown;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerInteraction player))
        {
            ButtonDown?.Invoke();
            _vfxButtonDown.Play();
            transform.position = new Vector3(transform.position.x, _downPosition, transform.position.z);
        }
    }
}
