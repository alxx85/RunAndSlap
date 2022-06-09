using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EnergyViewer : MonoBehaviour
{
    [SerializeField] private PlayerInteraction _player;
    [SerializeField] private Slider _slider;
    [SerializeField] private float _maxSlapsCount;
    [SerializeField] private float _energyOfSlap;
    [SerializeField] private float _bonusSpeed;
    [SerializeField] private float _duration;


    private float _downSpeed = 0.5f;
    private bool _isActive = false;

    public event UnityAction ReachedMaxEnergy;

    private void OnEnable()
    {
        _slider.maxValue = _maxSlapsCount;
        _player.Slaped += OnSlapingEnemy;
    }

    private void OnDisable()
    {
        _player.Slaped -= OnSlapingEnemy;
    }

    private void Update()
    {
        if (_isActive == false)
        {
            if (_slider.value > 0)
                _slider.value = Mathf.MoveTowards(_slider.value, 0, _downSpeed * Time.deltaTime);
        }
        else
        {
            if (_slider.value > 0)
                _slider.value = Mathf.MoveTowards(_slider.value, 0, _downSpeed);
            else
                _isActive = false;
        }
    }

    private void OnSlapingEnemy()
    {
        if (_isActive == false)
        {
            if (_slider.value + _energyOfSlap <= _maxSlapsCount)
            {
                _slider.value += _energyOfSlap;
            }
            else
            {
                ReachedMaxEnergy?.Invoke();
                _isActive = true;
            }
        }
    }
}
