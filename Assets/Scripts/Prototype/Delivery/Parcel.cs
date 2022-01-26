using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Prototype.Delivery
{
    public class Parcel : MonoBehaviour
    {
        public ParcelType Type { get { return type; } set { type = value; } }
        
        [SerializeField] ParcelType type;

        private Image image;

        private int address;

        private void Awake()
        {
            image = GetComponent<Image>();
        }

        public void UpdateBox()
        {
            image.sprite = ParcelManager.Instance.ParcelTypes.Find(x => x.Type == type).Sprite;
        }
    }
}
