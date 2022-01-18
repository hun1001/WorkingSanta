using System.Diagnostics;
using UnityEngine;
using Prototype.Delivery.Elevator;

namespace Prototype.Delivery.Elevator
{
    class ElevatorDoor : MonoBehaviour
    {
        public bool IsOpen { get { return _isOpen; } }

        [SerializeField] RectTransform elevatorDoorLeft;
        [SerializeField] RectTransform elevatorDoorRight;

        private bool isOpen = false;

        public void Open()
        {
            StartCoroutine(OpenCoroutine());
        }

        public void Close()
        {
            StartCoroutine(CloseCoroutine());
        }

        private IEnumerator openCoroutine()
        {
            if (isOpen)
            {
                yield break;
            }

            elevatorDoorLeft.DOAnchorPosX(-675, 3f).SetEase(Ease.InOutExpo);
            elevatorDoorRight.DOAnchorPosX(675, 3f).SetEase(Ease.InOutExpo);
            yield return new WaitForSeconds(3f);

            isOpen = true;
        }

        private IEnumerator closeCoroutine()
        {
            if (!isOpen)
            {
                yield break;
            }

            isOpen = false;

            elevatorDoorLeft.DOAnchorPosX(0, 3f).SetEase(Ease.InOutExpo);
            elevatorDoorRight.DOAnchorPosX(0, 3f).SetEase(Ease.InOutExpo);
            yield return new WaitForSeconds(3f);
        }
    }
}