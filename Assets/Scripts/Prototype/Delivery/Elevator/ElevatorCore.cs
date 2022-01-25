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
        public int CurrentFloor { get { return currentFloor; } }
        public float TimeToNextFloor { get { return timeToNextFloor; } }
        public float ResidentEventProbability { get { return residentEventProbability; } set { residentEventProbability = value; } }
        public bool IsMoving { get { return isMoving; } }
        public Direction Direction { get { return direction; } }

        [SerializeField] int topFloor = 30;
        [SerializeField] Text elevatorFloor;
        [SerializeField] ElevatorDoor door;
        [SerializeField] ElevatorFloorButton floorButton;
        [SerializeField] float timeToNextFloor = 1f;
        [SerializeField] Text apartFloor;

        private int currentFloor = 1;
        private float residentEventProbability = 0.3f;
        private bool isMoving = false;
        private List<int> targetFloors = new List<int>();
        private Direction direction = Direction.Up;

        private void SetBuilding(BuildingElement building)
        {

        }

        private void UpdateUI()
        {
            elevatorFloor.text = $"{currentFloor}";
            apartFloor.text = $"{currentFloor}";
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

            if (direction.Equals(Direction.Up) && currentFloor.Equals(topFloor)) yield break;
            if (direction.Equals(Direction.Down) && currentFloor.Equals(1)) yield break;

            if (door.IsOpen)
            {
                yield return door.CloseCoroutine();
            }

            yield return new WaitForSeconds(timeToNextFloor);

            if (direction.Equals(Direction.Up))
            {
                currentFloor++;
            }
            else
            {
                currentFloor--;
            }

            UpdateUI();

            bool isArrival = false;
            float random = UnityEngine.Random.Range(0f, 1f);
            Debug.Log("random : " + random);

            if (targetFloors.Contains(currentFloor))
            {
                targetFloors.Remove(currentFloor);
                isArrival = true;
            }
            else if (direction.Equals(Direction.Down) && random < residentEventProbability)
            {
                Debug.Log("ResidentEvent");
                isArrival = true;
            }
            else if (currentFloor >= topFloor)
            {
                direction = Direction.Down;
                isArrival = true;
            }
            else if (currentFloor <= 1)
            {
                direction = Direction.Up;
                StartCoroutine(door.OpenCoroutine());
                isMoving = false;
                DeliveryManager.Instance.GameInfo.isStart = false;
                yield break;
            }

            if (isArrival)
            {
                OnElevatorArrival?.Invoke(currentFloor);
                isMoving = false;
                yield return door.OpenCoroutine();
            }
            else
            {
                isMoving = false;
                StartCoroutine(MoveElevatorCoroutine());
            }
        }
    }
}