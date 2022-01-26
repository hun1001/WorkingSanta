using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

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

    public class ParcelManager : MonoSingleton<ParcelManager>
    {
        public List<ParcelElement> ParcelTypes { get { return parcelTypes; } }
        public List<HomeElement> TargetHomes { get { return targetHomes; } }

        [SerializeField] CanvasGroup inventoryGroup;
        [SerializeField] GameObject boxPrefab;
        [SerializeField] List<ParcelElement> parcelTypes;
        [SerializeField] GameObject parcelPrefab;
        [SerializeField] Transform spawnPoint;
        [SerializeField] CanvasGroup floorList;
        [SerializeField] List<HomeElement> targetHomes;
        [SerializeField] GameObject targetElementPrefab;

        private void Awake()
        {
            ButtonManager.Instance.AddHandler(this);
            floorList.alpha = 0;
            floorList.blocksRaycasts = false;
        }

        private void Start()
        {
            List<int> floorList = new List<int>();

            for (int i = 2; i < DeliveryManager.Instance.Elevator.TopFloor; i++)
            {
                floorList.Add(i);
            }

            for (int i = 0; i < DeliveryManager.Instance.Elevator.TopFloor - 2; i++)
            {
                var target = UnityEngine.Random.Range(0, floorList.Count);
                targetHomes.Add(new HomeElement() 
                { 
                    Floor = floorList[target],
                    Type = (ParcelType)UnityEngine.Random.Range(0, 5),
                    Direction = (Direction)UnityEngine.Random.Range(1, 3)
                });
                floorList.RemoveAt(target);
            }

            ParcelManager pManager = ParcelManager.Instance;

            foreach (var home in targetHomes)
            {
                GameObject parcel = Instantiate(targetElementPrefab, this.floorList.transform.GetChild(1).GetChild(0).GetChild(0));
                parcel.transform.GetChild(0).GetComponent<Text>().text = $"{home.Floor}{((int)home.Direction).ToString("00")}호";
                parcel.transform.GetChild(1).GetComponent<Image>().sprite = pManager.ParcelTypes.Find(x => x.Type == home.Type).Sprite;

                GameObject box = Instantiate(boxPrefab, inventoryGroup.transform);
                var boxParcel = box.GetComponent<Parcel>();
                boxParcel.Type = home.Type;
                boxParcel.UpdateBox();
                box.transform.SetSiblingIndex(UnityEngine.Random.Range(0, inventoryGroup.transform.childCount));
            }
            OpenFloorList();

            InvokeRepeating("UpdateFloorList", 3, 3);
        }

        private void UpdateFloorList()
        {
            for (int i = 0; i < this.floorList.transform.GetChild(1).GetChild(0).GetChild(0).childCount; i++)
            {
                var child = this.floorList.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(i);
                child.SetSiblingIndex(UnityEngine.Random.Range(0, this.floorList.transform.GetChild(1).GetChild(0).GetChild(0).childCount));
            }
        }

        public void OnDrop(DragItem dragItem, Direction direction)
        {
            Destroy(dragItem.gameObject);
            var parcel = targetHomes.Find(x => 
                x.Floor == DeliveryManager.Instance.Elevator.CurrentFloor &&
                x.Direction == direction &&
                x.Type == dragItem.GetComponent<Parcel>().Type
            );
            if (parcel == null)
            {
                DeliveryManager.Instance.OnDelivery(false);
                return;
            }
            targetHomes.RemoveAt(targetHomes.IndexOf(parcel));
            DeliveryManager.Instance.OnDelivery(true);
        }

        public void OpenFloorList()
        {
            floorList.DOFade(1, 0.5f);
            floorList.blocksRaycasts = true;
        }

        public void CloseFloorList()
        {
            floorList.DOFade(0, 0.5f);
            floorList.blocksRaycasts = false;
        }

        private void OnCloseFloorListButton()
        {
            CloseFloorList();
        }
    }
}
