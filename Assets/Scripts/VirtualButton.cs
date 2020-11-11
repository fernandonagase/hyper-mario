using UnityEngine;
using UnityEngine.EventSystems;

public class VirtualButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private Animator animCtrl;

    public bool Pressed { get; private set; } = false;
    public bool DownEvent { get; private set; } = false;
    public bool UpEvent { get; private set; } = false;

    private void Start()
    {
        animCtrl = GetComponent<Animator>();
    }

    private void LateUpdate()
    {
        UpEvent = false;
        DownEvent = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Pressed = true;
        DownEvent = true;
        animCtrl.SetBool("isPressed", Pressed);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Pressed = false;
        UpEvent = true;
        animCtrl.SetBool("isPressed", Pressed);
    }
}
