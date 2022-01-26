using UnityEngine;
using UnityEngine.UI;
using Prototype.Delivery.Elevator;
using System;

namespace Prototype.Delivery.Elevator
{
    public class ElevatorFloorButton : MonoBehaviour
    {
        [SerializeField] private Button exitButton;
        [SerializeField] private Toggle[] floorButtons;

        private void Awake()
        {
            ButtonManager.Instance.AddHandler(this);

            for (int i = 0; i < floorButtons.Length; i++)
            {
                int temp = i;
                floorButtons[i].onValueChanged.AddListener((value) => OnValueChanged(temp, value));
            }
        }

        private void OnValueChanged(int index, bool value)
        {
            if (value)
            {
                DeliveryManager.Instance.Elevator.AddTargetFloor(index + 1);
            }
            else
            {
                DeliveryManager.Instance.Elevator.RemoveTargetFloor(index + 1);
            }
        }

        private void OnCloseButtonPad()
        {
            CloseButtonWindow();
        }

        private void CloseButtonWindow()
        {
            gameObject.SetActive(false);
        }
    }
}