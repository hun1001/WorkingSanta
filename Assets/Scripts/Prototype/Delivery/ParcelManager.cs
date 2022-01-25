using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prototype.Delivery
{
    public enum ParcelType
    {
        A,
        B,
        C,
        D,
        E
    }

    [Serializable]
    public class ParcelElement
    {
        public ParcelType Type;
        public Sprite Sprite;
    }

    public class ParcelManager : MonoBehaviour
    {
        [SerializeField] List<ParcelElement> parcelTypes;
        [SerializeField] GameObject parcelPrefab;
        [SerializeField] Transform spawnPoint;


        void Start()
        {

        }
}
}
