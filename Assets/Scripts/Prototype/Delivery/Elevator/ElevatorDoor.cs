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

        private Coroutine autoCloseCoroutine;
        private bool isOpen = false;

        public void Open()
        {
            StartCoroutine(OpenCoroutine());
        }

        public void Close()
        {
            StartCoroutine(CloseCoroutine());
        }

        public IEnumerator OpenCoroutine()
        {
            if (isOpen || DeliveryManager.Instance.Elevator.IsMoving) yield break;

            ElevatorDoorLeft.DOAnchorPosX(-675, 3f).SetEase(Ease.InOutExpo);
            ElevatorDoorRight.DOAnchorPosX(675, 3f).SetEase(Ease.InOutExpo);
            yield return new WaitForSeconds(3f);
            autoCloseCoroutine = StartCoroutine(AutoCloseCoroutine());
            isOpen = true;
        }

        public IEnumerator CloseCoroutine()
        {
            if (DeliveryManager.Instance.Elevator.Direction == Direction.Down)
                EventManager.TriggerEvent("ElevatorDown");
            else
                EventManager.TriggerEvent("ElevatorUp");
                
            if (autoCloseCoroutine != null) StopCoroutine(autoCloseCoroutine);

            if (!isOpen)
            {
                yield break;
            }

            isOpen = false;

            ElevatorDoorLeft.DOAnchorPosX(0, 3f).SetEase(Ease.InOutExpo);
            ElevatorDoorRight.DOAnchorPosX(0, 3f).SetEase(Ease.InOutExpo);
            yield return new WaitForSeconds(3f);
            DeliveryManager.Instance.Elevator.MoveElevator();
        }

        private IEnumerator AutoCloseCoroutine()
        {
            yield return new WaitForSeconds(3f);
            Close();
        }
    }
}