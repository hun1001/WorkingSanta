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

    public class ResultStat
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

    public class GameInfo
    {
        public ResultStat resultStat { private set; get; } = new ResultStat();
        public int score = 0;
        public bool isStart = false;
        public float startTime = 5f;
        public float timeTotal = 0f;

        public void CopyInfo(GameInfo info)
        {
            resultStat = info.resultStat;
            score = info.score;
            isStart = info.isStart;
            startTime = info.startTime;
            timeTotal = info.timeTotal;
        }
    }

    public class DeliveryManager : MonoSingleton<DeliveryManager>
    {
        public ElevatorCore Elevator { get { return elevator; } }
        public GameInfo GameInfo { get { return gameInfo; } }

        [SerializeField] Text floorListContent;
        [SerializeField] CanvasGroup floorList;
        [SerializeField] ElevatorCore elevator;
        [SerializeField] HomeElement[] targetHomes;

        
        GameInfo gameInfo = new GameInfo();

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
            if (gameInfo.isStart)
            {
                gameInfo.timeTotal += Time.deltaTime;
            }
            else
            {
                gameInfo.startTime -= Time.deltaTime;
                if (gameInfo.startTime <= 0)
                {
                    gameInfo.isStart = true;
                    gameInfo.startTime = 5f;
                    elevator.TargetFloor = elevator.TopFloor;
                    elevator.MoveElevator();
                }
            }
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
