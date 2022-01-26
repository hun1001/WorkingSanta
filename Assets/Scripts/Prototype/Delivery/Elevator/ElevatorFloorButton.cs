using UnityEngine;
using UnityEngine.UI;
using Prototype.Delivery.Elevator;
using System;
using System.Collections.Generic;

namespace Prototype.Delivery.Elevator
{
    public class ElevatorFloorButton : MonoBehaviour
    {
        [SerializeField] private Button exitButton;
        [SerializeField] private Toggle[] floorButtons;

        private List<HomeElement> targetHome;

        private void Awake()
        {
            ButtonManager.Instance.AddHandler(this);
            targetHome = ParcelManager.Instance.TargetHomes;
            for (int i = 0; i < floorButtons.Length; i++)
            {
                int temp = i;
                floorButtons[i].onValueChanged.AddListener((value) => OnValueChanged(temp, value));
            }
        }

        private void CheckSelectButton()
        {
            int cnt = 0;
            foreach(var home in targetHome)
            {
                if(floorButtons[home.Floor] == true)
                {
                    cnt++;
                }
            }
            if(cnt == targetHome.Count)
            {
                CloseButtonPad();
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
            CheckSelectButton();
        }

        private void CloseButtonPad()
        {
            CloseButtonWindow();
        }

        private void CloseButtonWindow()
        {
            gameObject.SetActive(false);
        }
    }
}