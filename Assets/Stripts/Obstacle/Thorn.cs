using UnityEngine;
using DG.Tweening;

public class Thorn : MonoBehaviour
{
    [SerializeField] private ActivateThorn _button;
    [SerializeField] private float _delay;

    private float _duration = 0.5f;

    private void OnEnable()
    {
        _button.ButtonDown += OnButtonDown;
    }

    private void OnDisable()
    {
        _button.ButtonDown -= OnButtonDown;
    }

    private void OnButtonDown()
    {
        transform.DOMoveY(0, _duration, false).SetEase(Ease.Linear).SetDelay(_delay);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out EnemyInteraction enemy))
        {
            enemy.TakeDamage();
        }
    }
}
