using UnityEngine;
using Prototype.Delivery.Elevator;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Collections.Generic;

namespace Prototype.Delivery.Elevator
{
    public class ElevatorCore : MonoBehaviour
    {
        public event Action<int> OnElevatorArrival;

        public ElevatorDoor Door { get { return door; } }
        public int TopFloor { get { return topFloor; } set { topFloor = value; } }
        public int TargetFloor
        {
            get
            {
                return targetFloor;
            }
            set
            {
                targetFloor = value;
                MoveElevator();
            }
        }
        public int CurrentFloor { get { return currentFloor; } }
        public float TimeToNextFloor { get { return timeToNextFloor; } }
        public float ResidentEventProbability { get { return residentEventProbability; } }
        public bool IsMoving { get { return isMoving; } }

        [SerializeField] int topFloor = 30;
        [SerializeField] Text floorText;
        [SerializeField] ElevatorDoor door;
        [SerializeField] ElevatorFloorButton floorButton;
        [SerializeField] float timeToNextFloor = 1f;

        private int currentFloor = 1;
        private int targetFloor = 1;
        private float residentEventProbability = 0.3f;
        private bool isMoving = false;
        private List<int> targetFloors = new List<int>();

        private void SetBuilding(BuildingElement building)
        {

        }

        private void UpdateUI()
        {
            floorText.text = $"{currentFloor}";
        }

        public void MoveElevator()
        {
            StartCoroutine(MoveElevatorCoroutine());
        }

        public void AddTargetFloor(int floor)
        {
            targetFloors.Add(floor);
        }

        public void RemoveTargetFloor(int floor)
        {
            targetFloors.Remove(floor);
        }

        private IEnumerator MoveElevatorCoroutine()
        {
            if (isMoving) yield break;
            isMoving = true;
            if (targetFloor == currentFloor) yield break;
            if (door.IsOpen)
            {
                yield return door.CloseCoroutine();
            }

            yield return new WaitForSeconds(2f);

            currentFloor++;

            UpdateUI();

            bool isArrival = false;

            if (targetFloors.Contains(currentFloor))
            {
                targetFloors.Remove(currentFloor);
                isArrival = true;
            }
            else if (UnityEngine.Random.Range(0, 1) < residentEventProbability)
            {
                // Debug.Log("ResidentEvent");
                // isArrival = true;
            }
            else if (currentFloor == targetFloor)
            {
                Debug.Log("Arrived");
                isArrival = true;
            }

            if (isArrival)
            {
                OnElevatorArrival?.Invoke(currentFloor);
                isMoving = false;
                yield return door.OpenCoroutine();
            }
            else
            {
                StartCoroutine(MoveElevatorCoroutine());
            }
        }
    }
}