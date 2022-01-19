using UnityEngine;
using Prototype.Delivery.Elevator;
using UnityEngine.UI;
using System.Collections;
using System;

namespace Prototype.Delivery.Elevator
{
    public class ElevatorCore : MonoBehaviour
    {
        public ElevatorDoor ElevatorDoor { get { return elevatorDoor; } }

        [SerializeField] int topFloor = 30;
        [SerializeField] Text floorText;
        [SerializeField] ElevatorDoor elevatorDoor;
        [SerializeField] ElevatorFloorButton floorButton;

        private int currentFloor = 1;
        private int targetFloor = 1;

        private void UpdateUI()
        {
            floorText.text = $"{currentFloor}";
        }

        public void MoveElevator()
        {
            StartCoroutine(MoveElevatorCoroutine());
        }

        private IEnumerator MoveElevatorCoroutine()
        {
            yield return new WaitForSeconds(2f);

            if (targetFloor == currentFloor) yield break;

            currentFloor++;

            UpdateUI();

            if (UnityEngine.Random.Range(0, 100) < 30)
            {
                yield break;
            }

            if (currentFloor == targetFloor)
            {
                Debug.Log("목표층에 도착");
            }
            else
            {
                StartCoroutine(MoveElevatorCoroutine());
            }
        }
    }
}