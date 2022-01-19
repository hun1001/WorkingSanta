using UnityEngine;
using UnityEngine.UI;
using Prototype.Delivery.Elevator;

namespace Prototype.Delivery.Elevator
{
    public class ElevatorFloorButton : MonoBehaviour
    {
        [SerializeField] private Button exitButton;

        private void Start()
        {
            exitButton.onClick.AddListener(CloseButtonWindow);
        }

        private void CloseButtonWindow()
        {
            gameObject.SetActive(false);
        }
    }
}