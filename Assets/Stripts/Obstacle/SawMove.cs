using UnityEngine;
using DG.Tweening;

public class SawMove : MonoBehaviour
{
    [SerializeField] private bool _onlyRotate = true;
    [SerializeField] private float _timeRotate;
    [SerializeField] private float _maxLeftPosition;
        
    private const float Angle = 360f;

    private void Start()
    {
        if (_onlyRotate == false)
        {
            transform.DOLocalRotate(new Vector3(Angle * _timeRotate, 0, 0), _timeRotate, RotateMode.FastBeyond360)
                .SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
            transform.DOLocalMoveZ(_maxLeftPosition, _timeRotate).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
        }
        else
        {
            transform.DOLocalRotate(new Vector3(Angle, 0, 0), _timeRotate, RotateMode.FastBeyond360)
               .SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Interaction character))
        {
            character.TakeDamage();
        }
    }
}
