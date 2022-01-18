using UnityEngine;
using Prototype.Delivery.Elevator;

namespace Prototype.Delivery.Elevator
{
    class Elevator : MonoBehaviour
    {
        [SerializeField] int topFloor = 30;
        [SerializeField] Text _textFloor;

        private int currentFloor = 1;
        private int targetFloor = 1;
        private bool elevatorDoorOpen = false;
    }
}