using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Prototype.Delivery.Elevator;

namespace Prototype.Delivery
{
    public class DebugManager : MonoBehaviour
    {
        [SerializeField] Text debugText;

        GameInfo gameInfo = new GameInfo();

        private void Start()
        {
            gameInfo.CopyInfo(DeliveryManager.Instance.GetGameInfo());
        }
        
        public void Update()
        {
            string text = "";
            text += $"Started: {gameInfo.isStart}\n";
            text += "=====ElevatorInfo=====\n";
            text += $"CurrentFloor: {Elevator.CurrentFloor}/{Elevator.TopFloor}\n";
            text += $"TargetFloor: {Elevator.TargetFloor}\n";
            text += $"IsMoving: {Elevator.IsMoving}\n";
            text += $"ElevatorDoorOpened: {Elevator.Door.IsOpen}\n";
            text += $"TimeToNextFloor: {Elevator.TimeToNextFloor}\n";
            text += $"ResidentEventProbability: {Elevator.ResidentEventProbability}\n";
            text += "=====ResultStat=====\n";
            text += $"Score: {gameInfo.score}\n";
            text += $"Success: {gameInfo.GetResultStat().SuccessFloorList.Count}\n";
            text += $"Fails: {gameInfo.GetResultStat().FailFloorList.Count}\n";
            text += $"Total: {gameInfo.GetResultStat().Total}\n";
            text += $"SuccessRate: {(float)gameInfo.GetResultStat().SuccessFloorList.Count / gameInfo.GetResultStat().Total * 100}%\n";
            text += "=====Timer=====\n";
            text += $"StartTime: {gameInfo.startTime}\n";
            text += $"TotalTime: {gameInfo.timeTotal}\n";
            debugText.text = text;
        }
    }

}
