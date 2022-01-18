using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    public void OnBeginDrag(PointerEventData eventData)
    {
        // �̰� ���� �ʿ� �Ѱ�?
        Debug.Log("Begin Drag");
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = new Vector3(Camera.main.ScreenToWorldPoint(eventData.position).x, Camera.main.ScreenToWorldPoint(eventData.position).y, 0);
    }

    public void OnDrop(PointerEventData eventData)
    {
        // ���������� ���� �������� ���� ��ġ�� ������ ��ġ�� �÷��θ� fade out �� ���� ��Ȳ�� ���� ��ġ��
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // �̰� ���� �ʿ� �Ѱ�?
        Debug.Log("OnEndDrag");
    }
}

