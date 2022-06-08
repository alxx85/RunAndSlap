using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressViewer : MonoBehaviour
{
    [SerializeField] private Transform _finish;
    [SerializeField] private Transform _player;

    private Slider _slider;

    private void Start()
    {
        _slider = GetComponent<Slider>();
        _slider.maxValue = _finish.position.z;
    }

    private void FixedUpdate()
    {
        _slider.value = _player.position.z;
    }
}
