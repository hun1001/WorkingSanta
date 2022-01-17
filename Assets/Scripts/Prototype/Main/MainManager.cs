using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prototype;

namespace Prototype_Main
{
    public class MainManager : MonoBehaviour
    {
        private void Awake()
        {
            ButtonManager.Instance.AddHandler(this);
        }

        private void onDeliveryStart()
        {
            Debug.Log("Delivery Start");
        }
    }
}
