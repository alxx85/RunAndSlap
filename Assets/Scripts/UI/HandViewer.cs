using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandViewer : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private GlovesCollection _gloves;

    private int _decreaseMaxGloves = 2;

    private void Start()
    {
        _slider.maxValue = _gloves.CollectionCount - _decreaseMaxGloves;
        _gloves.ChangedCollection += OnChangedGlovesCollection;
    }

    private void OnDisable()
    {
        _gloves.ChangedCollection -= OnChangedGlovesCollection;
    }

    private void OnChangedGlovesCollection()
    {
        if (_slider.value < _slider.maxValue)
            _slider.value++;
    }
}
