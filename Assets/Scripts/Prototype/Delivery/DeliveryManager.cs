using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Prototype.Delivery.Elevator;
using UnityEngine.SceneManagement;

namespace Prototype.Delivery
{
    public enum Direction
    {
        Left = 1,
        Right = 2,
        Up = 3,
        Down = 4,
        None = 5
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
        public bool isEnd = false;

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
        [SerializeField] CanvasGroup resultCanvas;
        [SerializeField] GameObject resultTextPrefab;
        [SerializeField] GameObject resultLinePrefab;
        [SerializeField] RectTransform resultTextParent;

        GameInfo gameInfo = new GameInfo();

        private void Awake()
        {
            ButtonManager.Instance.AddHandler(this);
        }

        private void Start()
        {
            //TODO: 시작 전 건물 선택
            elevator.Door.Open();
            elevator.Door.DisableAutoClose();
        }

        private void Update()
        {
            if(gameInfo.isEnd) return;

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

        private void OnGotoMain()
        {
            Debug.Log("goto other scene");
            SceneManager.LoadScene("SampleScene");
        }
        
        public void OnGameOver()
        {
            gameInfo.isStart = false;
            gameInfo.isEnd = true;

            StartCoroutine(GameOverCoroutine());
        }

        private IEnumerator GameOverCoroutine()
        {
            resultCanvas.blocksRaycasts = true;

            resultCanvas.DOFade(1f, 1f);
            yield return new WaitForSeconds(1f);
            var item = Instantiate(resultTextPrefab, resultTextParent);
            item.GetComponent<Text>().text = $"택배 수 : {gameInfo.resultStat.Total}개";
            yield return new WaitForSeconds(0.5f);

            item = Instantiate(resultTextPrefab, resultTextParent);
            item.GetComponent<Text>().text = $"성공 횟수 : {gameInfo.resultStat.SuccessFloorList.Count}회";
            yield return new WaitForSeconds(0.5f);

            item = Instantiate(resultTextPrefab, resultTextParent);
            item.GetComponent<Text>().text = $"실패 횟수 : {gameInfo.resultStat.FailFloorList.Count}회";
            yield return new WaitForSeconds(0.5f);

            item = Instantiate(resultTextPrefab, resultTextParent);
            item.GetComponent<Text>().text = $"성공률 : {((float)gameInfo.resultStat.SuccessFloorList.Count / gameInfo.resultStat.Total * 100).ToString("00")}%";
            yield return new WaitForSeconds(0.5f);

            item = Instantiate(resultTextPrefab, resultTextParent);
            item.GetComponent<Text>().text = $"총 소요 시간 : {gameInfo.timeTotal.ToString("0")}초";
            yield return new WaitForSeconds(0.5f);

            Instantiate(resultLinePrefab, resultTextParent);
            yield return new WaitForSeconds(1f);

            item = Instantiate(resultTextPrefab, resultTextParent);
            item.GetComponent<Text>().text = $"수익: {(int)(((float)gameInfo.resultStat.SuccessFloorList.Count / gameInfo.resultStat.Total)*25)}만원";
        }
    }
}
