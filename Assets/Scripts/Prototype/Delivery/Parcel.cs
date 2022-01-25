using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Prototype.Delivery
{
    public class Parcel : MonoBehaviour
    {
        private Image image;

        private int address;

        void Start()
        {
            image = GetComponent<Image>();
        }
    }
}
