using UnityEngine;

[RequireComponent(typeof(Collider), typeof(Animator))]
public class RagDoll : MonoBehaviour
{
    [SerializeField] private bool _isActive;
    [SerializeField] private Collider[] _dollColliders;
    [SerializeField] private Rigidbody[] _dollRigidbodies;

    public Collider _characterCollider;
    public Animator _animator;

    private void Start()
    {
        _characterCollider = GetComponent<Collider>();
        _animator = GetComponent<Animator>();
    }

    public void Activate()
    {
        _characterCollider.enabled = _isActive;
        _animator.enabled = _isActive;
        ColliderActivator(_dollColliders, !_isActive);
        KinematicActivator(_dollRigidbodies, _isActive);
        _isActive = !_isActive;
    }

    private void ColliderActivator(Collider[] colliders, bool active)
    {
        foreach (var collider in colliders)
        {
            collider.enabled = active;
        }
    }

    private void KinematicActivator(Rigidbody[] rigidbodies, bool active)
    {
        foreach (var rigidbody in rigidbodies)
        {
            rigidbody.isKinematic = active;
        }
    }
}
