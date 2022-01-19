using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Prototype.Delivery.Elevator;

namespace Prototype.Delivery
{
    public enum Direction
    {
        Left = 1,
        Right
    }

    class ResultStat
    {
        public int Total;
        public List<int> SuccessFloorList = new List<int>();
        public List<int> FailFloorList = new List<int>();
    }

    [Serializable]
    public class HomeElement
    {
        public int Floor;
        public Direction Direction;
        public bool IsSuccess;
    }

    public class DeliveryManager : MonoSingleton<DeliveryManager>
    {
        public ElevatorCore ElevatorCore { get { return elevator; } }

        [SerializeField] Text floorListContent;
        [SerializeField] CanvasGroup floorList;
        [SerializeField] ElevatorCore elevator;
        [SerializeField] HomeElement[] targetHomes;

        private ResultStat resultStat = new ResultStat();
        private int score = 0;
        
        private void Awake()
        {
            ButtonManager.Instance.AddHandler(this);
        }

        private void Start()
        {
            floorList.alpha = 0;
            floorList.blocksRaycasts = false;

            elevator.ElevatorDoor.Open();
        }

        private void OnCloseFloorListButton()
        {
            CloseFloorList();
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
    }
}
