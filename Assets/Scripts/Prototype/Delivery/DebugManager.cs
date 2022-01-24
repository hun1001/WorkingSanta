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
        ElevatorCore elevator;

        private void Start()
        {
            gameInfo = DeliveryManager.Instance.GameInfo;
            elevator = DeliveryManager.Instance.Elevator;
        }
        
        private void Update()
        {
            string text = "";
            text += $"Started: {gameInfo.isStart}\n";
            text += "=====ElevatorInfo=====\n";
            text += $"CurrentFloor: {elevator.CurrentFloor}/{elevator.TopFloor}\n";
            text += $"TargetFloor: {elevator.TargetFloor}\n";
            text += $"IsMoving: {elevator.IsMoving}\n";
            text += $"ElevatorDoorOpened: {elevator.Door.IsOpen}\n";
            text += $"TimeToNextFloor: {elevator.TimeToNextFloor}\n";
            text += $"ResidentEventProbability: {elevator.ResidentEventProbability}\n";
            text += "=====ResultStat=====\n";
            text += $"Score: {gameInfo.score}\n";
            text += $"Success: {gameInfo.resultStat.SuccessFloorList.Count}\n";
            text += $"Fails: {gameInfo.resultStat.FailFloorList.Count}\n";
            text += $"Total: {gameInfo.resultStat.Total}\n";
            text += $"SuccessRate: {(float)gameInfo.resultStat.SuccessFloorList.Count / gameInfo.resultStat.Total * 100}%\n";
            text += "=====Timer=====\n";
            text += $"StartTime: {gameInfo.startTime}\n";
            text += $"TotalTime: {gameInfo.timeTotal}\n";
            debugText.text = text;
        }
    }

}
