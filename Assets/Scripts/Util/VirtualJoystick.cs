using UnityEngine;
using UnityEngine.EventSystems;

public class VirtualJoystick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    private Animator _stickAnimator;
    private RectTransform _joystickRect;
    private RectTransform _stickRect;
    private Vector2 _input;

    // Start is called before the first frame update
    void Start()
    {
        _stickAnimator = GetComponentInChildren<Animator>();
        _joystickRect = GetComponent<RectTransform>();
        _stickRect = _stickAnimator.GetComponent<RectTransform>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            _joystickRect,
            eventData.position,
            eventData.pressEventCamera,
            out pos
        );
        _input.x = 2.0f * pos.x / _joystickRect.sizeDelta.x;
        _input.y = 2.0f * pos.y / _joystickRect.sizeDelta.y;
        _input = _input.magnitude > 1.0f ? _input.normalized : _input;

        _stickRect.anchoredPosition = new Vector2(
            _input.x * _joystickRect.sizeDelta.x / 3.0f,
            _input.y * _joystickRect.sizeDelta.y / 3.0f
        );
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _stickAnimator.SetBool("isPressed", true);
        OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _input = Vector2.zero;
        _stickRect.anchoredPosition = Vector2.zero;

        _stickAnimator.SetBool("isPressed", false);
    }

    public float GetHorizontal()
    {
        return _input.x;
    }

    public float GetVertical()
    {
        return _input.y;
    }
}
