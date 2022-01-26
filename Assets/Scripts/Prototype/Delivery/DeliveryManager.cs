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
        Right = 2,
        Up = 3,
        Down = 4
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
        public ParcelType Type;
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

        [SerializeField] ElevatorCore elevator;

        GameInfo gameInfo = new GameInfo();

        private void Awake()
        {
            ButtonManager.Instance.AddHandler(this);
        }

        private void Start()
        {
            //TODO: 시작 전 건물 선택
            elevator.Door.Open();
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
                    elevator.MoveElevator();
                }
            }
        }

        public void OnDelivery(bool isSuccess)
        {
            gameInfo.resultStat.Total++;
            if (isSuccess)
            {
                gameInfo.resultStat.SuccessFloorList.Add(elevator.CurrentFloor);
            }
            else
            {
                gameInfo.resultStat.FailFloorList.Add(elevator.CurrentFloor);
            }
        }
    }
}
