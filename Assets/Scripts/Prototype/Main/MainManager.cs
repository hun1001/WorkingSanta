using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Prototype;
using UnityEngine.SceneManagement;

namespace Prototype_Main
{
    public class MainManager : MonoBehaviour
    {
        [SerializeField]
        private Image AlbaIcon;
        [SerializeField]
        private Sprite Delivery;

        private void Awake()
        {
            ButtonManager.Instance.AddHandler(this);
        }

        private void OnDeliveryStart()
        {
            //road other scene
            SceneManager.LoadScene("Prototype");
        }
        private void OnApplyD()
        {
            AlbaIcon.sprite = Delivery;
        }
        private void OnShop()
        {

        }
        private void OnAlba()
        {

        }
        private void OnWeekendAlba()
        {

        }
    }
}
