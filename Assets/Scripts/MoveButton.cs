using UnityEngine;
using UnityEngine.EventSystems;

public class MoveButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool IsPressing { get; private set; }

    public void OnPointerDown(PointerEventData eventData)
    {
        IsPressing = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        IsPressing = false;
    }
}
