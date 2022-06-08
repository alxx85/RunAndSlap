using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(RagDoll))]
public abstract class Interaction : MonoBehaviour
{
    private bool _isAlive = true;

    public RagDoll _ragDoll { get; private set; }

    public event UnityAction Dying;

    public bool IsAlive => _isAlive;

    private void Start()
    {
        _ragDoll = GetComponent<RagDoll>();
    }

    public void TakeDamage()
    {
        _isAlive = false;
        _ragDoll.Activate();
        Dying?.Invoke();
        VFXDamage();
    }

    protected abstract void VFXDamage();
}
