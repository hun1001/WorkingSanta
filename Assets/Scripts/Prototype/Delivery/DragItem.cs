using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    public void OnBeginDrag(PointerEventData eventData)
    {
        // 이게 굳이 필요 한가?
        Debug.Log("Begin Drag");
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = new Vector3(Camera.main.ScreenToWorldPoint(eventData.position).x, Camera.main.ScreenToWorldPoint(eventData.position).y, 0);
    }

    public void OnDrop(PointerEventData eventData)
    {
        // 엘레베이터 문이 열렸을때 왼쪽 위치나 오른쪽 위치에 올려두면 fade out 이 외의 상황은 원래 위치로
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // 이게 굳이 필요 한가?
        Debug.Log("OnEndDrag");
    }
}

