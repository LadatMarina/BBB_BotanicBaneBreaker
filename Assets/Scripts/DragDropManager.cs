using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDropManager : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private Canvas canvas;
    private RectTransform rectTransform;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("On Pointer Down");
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("On Begin Drag");
    }


    public void OnDrag(PointerEventData eventData)
    {
        ///while onDrag "while i'm moving with the mouse clicked" the position of the object
        ///has to be the same of the pointer
  
        ///The delta vector provides the 2D location as the user moves the pointer.
        ///It is updated every frame. The 2D location starts at Vector2(0.0f, 0.0f) 
        ///when OnBeginDrag is called. As OnDrag is called the delta is updated. The 
        ///value of delta can change greatly.
        ///Then, we divide the vector with the scale of the canvas for adjusting the exact position
        rectTransform.anchoredPosition += eventData.delta/canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {

    }
}
