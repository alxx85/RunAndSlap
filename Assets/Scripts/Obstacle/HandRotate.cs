using UnityEngine;
using DG.Tweening;

public class HandRotate : MonoBehaviour
{
    [SerializeField] private float _timeRotate;

    private const float Angle = 360f;

    private void Start()
    {
        transform.DOLocalRotate(new Vector3(0, Angle, 0), _timeRotate, RotateMode.FastBeyond360)
                                .SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear);
    }
}
