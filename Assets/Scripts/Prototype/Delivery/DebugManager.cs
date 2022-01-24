using System.Text;
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
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Started: {gameInfo.isStart}");
            sb.AppendLine("=====ElevatorInfo=====");
            sb.AppendLine($"CurrentFloor: {elevator.CurrentFloor}/{elevator.TopFloor}");
            sb.AppendLine($"TargetFloor: {elevator.TargetFloor}");
            sb.AppendLine($"IsMoving: {elevator.IsMoving}");
            sb.AppendLine($"ElevatorDoorOpened: {elevator.Door.IsOpen}");
            sb.AppendLine($"TimeToNextFloor: {elevator.TimeToNextFloor}");
            sb.AppendLine($"ResidentEventProbability: {elevator.ResidentEventProbability}");
            sb.AppendLine("=====ResultStat=====");
            sb.AppendLine($"Score: {gameInfo.score}");
            sb.AppendLine($"Success: {gameInfo.resultStat.SuccessFloorList.Count}");
            sb.AppendLine($"Fails: {gameInfo.resultStat.FailFloorList.Count}");
            sb.AppendLine($"Total: {gameInfo.resultStat.Total}");
            sb.AppendLine($"SuccessRate: {(float)gameInfo.resultStat.SuccessFloorList.Count / gameInfo.resultStat.Total * 100}%");
            sb.AppendLine("=====Timer=====");
            sb.AppendLine($"StartTime: {gameInfo.startTime}");
            sb.AppendLine($"TotalTime: {gameInfo.timeTotal}");
            debugText.text = sb.ToString();
        }
    }

}
