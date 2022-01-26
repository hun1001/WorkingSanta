using UnityEngine;
using UnityEngine.UI;
using Prototype.Delivery.Elevator;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Prototype.Delivery.Elevator
{
    public class ElevatorFloorButton : MonoBehaviour
    {
        [SerializeField] List<Toggle> floorButtons;
        [SerializeField] RectTransform buttonPadList;
        [SerializeField] GameObject buttonPrefab;

        private List<HomeElement> targetHome;

        private void Awake()
        {
            for (int i = 1; i <= DeliveryManager.Instance.Elevator.TopFloor; i++)
            {
                var button = Instantiate(buttonPrefab, buttonPadList);
                button.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = i.ToString();
                button.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = i.ToString();
                floorButtons.Add(button.GetComponent<Toggle>());
            }

            ButtonManager.Instance.AddHandler(this);
            targetHome = ParcelManager.Instance.TargetHomes;
            for (int i = 0; i < floorButtons.Count; i++)
            {
                int temp = i;
                floorButtons[i].onValueChanged.AddListener((value) => OnValueChanged(temp, value));
            }
        }

        private void CheckSelectButton()
        {
            List<int> tempA = DeliveryManager.Instance.Elevator.TargetFloors;
            List<int> tempB = new List<int>();

            foreach (var floor in targetHome)
            {
                tempB.Add(floor.Floor);
            }

            tempA.Sort();
            tempB.Sort();

            if (tempA.SequenceEqual(tempB))
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
            DeliveryManager.Instance.Elevator.Door.Close();
        }

        private void CloseButtonWindow()
        {
            gameObject.SetActive(false);
        }
    }
}