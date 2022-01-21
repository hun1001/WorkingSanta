using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Prototype.Delivery
{
    public class DragItem : MonoBehaviour, IBeginDragHandler, IDragHandler// , IDropHandler
    {
        private RectTransform inventoryRect;
        private RectTransform rectTransform;
        private Vector3 startPosition;

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            inventoryRect = transform.parent.GetComponent<RectTransform>();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            startPosition = transform.position;
        }

        public void OnDrag(PointerEventData eventData)
        {
            transform.position = new Vector3(Camera.main.ScreenToWorldPoint(eventData.position).x, Camera.main.ScreenToWorldPoint(eventData.position).y, 0);
        }

        // public void OnDrop(PointerEventData eventData)
        // {
        //     if (DeliveryManager.Instance.ElevatorCore.ElevatorDoor.IsOpen)
        //     {
        //         Debug.Log("isOverlapped(inventoryRect, rectTransform) : " + isOverlapped(inventoryRect, rectTransform));
        //         if (!isOverlapped(inventoryRect, rectTransform))
        //         {
        //             if (transform.position.x < 0)
        //             {
        //                 Debug.Log("Left");
        //             }
        //             else
        //             {
        //                 Debug.Log("Right");
        //             }
        //             return;
        //         }
        //     }
        //     transform.position = startPosition;
        // }

        // private bool isOverlapped(RectTransform rect1, RectTransform rect2)
        // {
            
        // }
    }
}
