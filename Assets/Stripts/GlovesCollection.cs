using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GlovesCollection : MonoBehaviour
{
    private List<HandCollect> _hands;

    public event UnityAction ChangedCollection;

    public int CollectionCount => _hands.Count;

    private void Awake()
    {
        _hands = new List<HandCollect>(GetComponentsInChildren<HandCollect>());

        foreach (var hand in _hands)
        {
            hand.Collect += OnCollect;
        }
    }

    private void OnCollect(HandCollect hand)
    {
        hand.Collect -= OnCollect;
        _hands.Remove(hand);
        ChangedCollection?.Invoke();
    }
}
