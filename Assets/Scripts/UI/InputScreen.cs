using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class InputScreen : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private float _sensitivity;
    [SerializeField] private float _roadSize;

    private float _touchPosition;
    private float newPosition = 0;
    
    private const float Half = 2f;


    public event UnityAction<float, float> ChangedPosition;

    public virtual void OnDrag(PointerEventData eventData)
    {
        float screenSize = Screen.width;
        float newInputPosition = eventData.position.x;
        float delta = newInputPosition - _touchPosition;
        float roadCenter = _roadSize / Half;

        newPosition += (delta * _roadSize / screenSize * _sensitivity);
        newPosition = Mathf.Clamp(newPosition, -roadCenter, roadCenter);
        Debug.Log(delta);
        ChangedPosition?.Invoke(newPosition, delta);
        _touchPosition = newInputPosition;
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        _touchPosition = eventData.position.x;
        OnDrag(eventData);
    }

    public virtual void OnPointerUp(PointerEventData eventData)
    {
        _touchPosition = 0;
    }
}
