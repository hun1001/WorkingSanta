using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Prototype.Delivery
{
    public class DragItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
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

        public void OnEndDrag(PointerEventData eventData)
        {
            if (DeliveryManager.Instance.Elevator.Door.IsOpen)
            {
                if (transform.position.y > -2.8f)
                {
                    if (transform.position.x < 0)
                    {
                        ParcelManager.Instance.OnDrop(this, Direction.Left);
                    }
                    else
                    {
                        ParcelManager.Instance.OnDrop(this, Direction.Right);
                    }
                }
                else
                {
                    transform.position = startPosition;
                }
            }
            else
            {
                transform.position = startPosition;
            }
        }
    }
}
