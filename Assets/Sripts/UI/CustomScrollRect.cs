using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CustomScrollRect : ScrollRect, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private bool canDrag = true;

    public override void OnBeginDrag(PointerEventData eventData)
    {
        if (canDrag)
        {
            base.OnBeginDrag(eventData);
        }
    }
    public override void OnDrag(PointerEventData eventData)
    {
        if (canDrag)
        {
            base.OnDrag(eventData);
        }
    }
    public override void OnEndDrag(PointerEventData eventData)
    {
        if (canDrag)
        {
            base.OnEndDrag(eventData);
        }
    }
}
