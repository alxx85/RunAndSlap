using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Collider))]
public class MoveArrow : MonoBehaviour
{
    [SerializeField] private Material _arrowMaterial;
    [SerializeField] private float _bonusSpeed;
    [SerializeField] private float _bonusDuration;

    private Collider _collider;
    private float _animationDuration = 1f;

    private void Start()
    {
        _collider = GetComponent<Collider>();
        Vector2 circlePosition = new Vector2(0, -1);
        _arrowMaterial.DOOffset(circlePosition, _animationDuration).SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out PlayerBoosts player))
        {
            player.ActivateArrow();
            _collider.enabled = false;
        }
    }
}
