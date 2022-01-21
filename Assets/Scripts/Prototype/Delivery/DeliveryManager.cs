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
        Right = 2
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

    [Serializable]
    public class BuildingElement
    {
        public int TopFloor;
        public float ElevatorSpeed;
        public float ResidentEventProbability;
    }

    public class DeliveryManager : MonoSingleton<DeliveryManager>
    {
        public ElevatorCore Elevator { get { return elevator; } }

        [SerializeField] Text debugText;
        [SerializeField] Text floorListContent;
        [SerializeField] CanvasGroup floorList;
        [SerializeField] ElevatorCore elevator;
        [SerializeField] HomeElement[] targetHomes;

        private ResultStat resultStat = new ResultStat();
        private int score = 0;
        private bool isStart = false;
        private float startTime = 5f;
        private float timeTotal = 0f;
        private int targetIndex = 0;
        
        private void Awake()
        {
            ButtonManager.Instance.AddHandler(this);

            floorList.alpha = 0;
            floorList.blocksRaycasts = false;
        }

        private void Start()
        {
            //TODO: 시작 전 건물 선택
            OpenFloorList();
            elevator.Door.Open();
            elevator.OnElevatorArrival += OnElevatorArrival;

            foreach (var home in targetHomes)
            {
                floorListContent.text += $"{home.Floor}{((int)home.Direction).ToString("00")}호\n";
            }
        }

        private void OnElevatorArrival(int floor)
        {
            if (targetHomes[targetIndex].Floor == floor)
            {
                targetIndex++;
            }
        }

        private void Update()
        {
            UpdateDebugText();

            if (isStart)
            {
                timeTotal += Time.deltaTime;
            }
            else
            {
                startTime -= Time.deltaTime;
                if (startTime <= 0)
                {
                    isStart = true;
                    startTime = 5f;
                    elevator.TargetFloor = elevator.TopFloor;
                    elevator.MoveElevator();
                }
            }
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

        public void UpdateDebugText()
        {
            string text = "";
            text += $"Started: {isStart}\n";
            text += "=====ElevatorInfo=====\n";
            text += $"CurrentFloor: {elevator.CurrentFloor}/{elevator.TopFloor}\n";
            text += $"TargetFloor: {elevator.TargetFloor}\n";
            text += $"IsMoving: {elevator.IsMoving}\n";
            text += $"ElevatorDoorOpened: {elevator.Door.IsOpen}\n";
            text += $"TimeToNextFloor: {elevator.TimeToNextFloor}\n";
            text += $"ResidentEventProbability: {elevator.ResidentEventProbability}\n";
            text += "=====ResultStat=====\n";
            text += $"Score: {score}\n";
            text += $"Success: {resultStat.SuccessFloorList.Count}\n";
            text += $"Fails: {resultStat.FailFloorList.Count}\n";
            text += $"Total: {resultStat.Total}\n";
            text += $"SuccessRate: {(float)resultStat.SuccessFloorList.Count / resultStat.Total * 100}%\n";
            text += "=====Timer=====\n";
            text += $"StartTime: {startTime}\n";
            text += $"TotalTime: {timeTotal}\n";
            debugText.text = text;
        }
    }
}
