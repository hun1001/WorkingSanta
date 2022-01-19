using UnityEngine;
using Prototype.Delivery.Elevator;
using System.Collections;
using DG.Tweening;
using System;

namespace Prototype.Delivery.Elevator
{
    public class ElevatorDoor : MonoBehaviour
    {
        public bool IsOpen { get { return isOpen; } }
        public event Action OnOpen;
        public event Action OnClose;

        [SerializeField] RectTransform ElevatorDoorLeft;
        [SerializeField] RectTransform ElevatorDoorRight;

        private bool isOpen = false;

        public void Open()
        {
            StartCoroutine(OpenCoroutine());
        }

        public void Close()
        {
            StartCoroutine(CloseCoroutine());
        }

        private IEnumerator OpenCoroutine()
        {
            if (isOpen)
            {
                yield break;
            }

            ElevatorDoorLeft.DOAnchorPosX(-675, 3f).SetEase(Ease.InOutExpo);
            ElevatorDoorRight.DOAnchorPosX(675, 3f).SetEase(Ease.InOutExpo);
            yield return new WaitForSeconds(3f);

            isOpen = true;
        }

        private IEnumerator CloseCoroutine()
        {
            if (!isOpen)
            {
                yield break;
            }

            isOpen = false;

            ElevatorDoorLeft.DOAnchorPosX(0, 3f).SetEase(Ease.InOutExpo);
            ElevatorDoorRight.DOAnchorPosX(0, 3f).SetEase(Ease.InOutExpo);
            yield return new WaitForSeconds(3f);
        }
    }
}