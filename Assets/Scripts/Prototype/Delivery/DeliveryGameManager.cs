using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Delivery_Prototype
{
    public class DeliveryGameManager : MonoSingleton<DeliveryGameManager>
    {
        enum Direction
        {
            Left = 1,
            Right
        }


        class ResultStat
        {
            public int total;
            public List<int> successFloorList = new List<int>();
            public List<int> failFloorList = new List<int>();
        }


        [SerializeField] int topFloor = 30;
        [SerializeField] Text _textFloor;

        [SerializeField] Text _textFloorListContent;
        [SerializeField] Button _buttonCloseFloorList;
        [SerializeField] CanvasGroup _canvasGroupFloorList;

        [SerializeField] RectTransform _rectTransformElevatorDoorLeft;
        [SerializeField] RectTransform _rectTransformElevatorDoorRight;

        [SerializeField] Button _buttonDropLeft;
        [SerializeField] Button _buttonDropRight;

        private Dictionary<int, Direction> _targetHomes;
        private ResultStat _resultStat;
        private int currentFloor = 1;
        private int targetFloor = 1;
        private int score = 0;
        private bool firstClose = true;
        private bool elevatorDoorOpen = false;

        private void Awake()
        {
            _targetHomes = new Dictionary<int, Direction>(){
                {2, Direction.Left},
                {5, Direction.Left}
            };
            _resultStat = new ResultStat();
            _buttonCloseFloorList.onClick.AddListener(() => CloseFloorList());

            _buttonDropLeft.onClick.AddListener(() => DropItem(Direction.Left));
            _buttonDropRight.onClick.AddListener(() => DropItem(Direction.Right));

            _textFloorListContent.text = "";

            foreach (var targetHome in _targetHomes)
            {
                _textFloorListContent.text += $"{targetHome.Key}0{(int)targetHome.Value}호\n";
            }

            OpenFloorList();
        }

        private void Start()
        {
            currentFloor = 1;
            targetFloor = topFloor;
            UpdateUI();
            StartCoroutine(MoveElevator());
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                if (_canvasGroupFloorList.alpha == 0)
                {
                    OpenFloorList();
                }
                else
                {
                    CloseFloorList();
                }
            }
        }

        private void UpdateUI()
        {
            _textFloor.text = currentFloor.ToString();
        }

        private void CloseFloorList()
        {
            _canvasGroupFloorList.alpha = 0;
            _canvasGroupFloorList.blocksRaycasts = false;
        }

        private void OpenFloorList()
        {
            _canvasGroupFloorList.alpha = 1;
            _canvasGroupFloorList.blocksRaycasts = true;
        }

        private IEnumerator OpenElevatorDoor()
        {
            _rectTransformElevatorDoorLeft.DOAnchorPosX(-675, 3f).SetEase(Ease.InOutExpo);
            _rectTransformElevatorDoorRight.DOAnchorPosX(675, 3f).SetEase(Ease.InOutExpo);
            yield return new WaitForSeconds(3f);

            elevatorDoorOpen = true;

            if (currentFloor == targetFloor)
            {
                // 배달 임무

            }
            else
            {
                // 랜덤 일반인 이벤트
                Debug.Log("일반인 이벤트");
                yield return new WaitForSeconds(3f);
                StartCoroutine(CloseElevatorDoor());
                yield return new WaitForSeconds(3f);
                StartCoroutine(MoveElevator());
            }
        }

        private void DropItem(Direction direction)
        {
            if (!elevatorDoorOpen)
            {
                return;
            }
            if (!_targetHomes.ContainsKey(currentFloor))
            {
                Debug.Log("배달이 없는 층");
                return;
            }

            if (direction == Direction.Left)
            {
                if (_targetHomes[currentFloor] == Direction.Left)
                {
                    // 배달 성공
                    score += 100;
                    Debug.Log("배달 성공");
                    _resultStat.successFloorList.Add(currentFloor);
                }
                else
                {
                    // 배달 실패
                    score -= 100;
                    _resultStat.failFloorList.Add(currentFloor);
                }
            }
            else
            {
                if (_targetHomes[currentFloor] == Direction.Right)
                {
                    // 배달 성공
                    score += 100;
                    _resultStat.successFloorList.Add(currentFloor);
                }
                else
                {
                    // 배달 실패
                    score -= 100;
                    _resultStat.failFloorList.Add(currentFloor);
                }
            }
        }

        private IEnumerator CloseElevatorDoor()
        {
            elevatorDoorOpen = false;
            _rectTransformElevatorDoorLeft.DOAnchorPosX(0, 3f).SetEase(Ease.InOutExpo);
            _rectTransformElevatorDoorRight.DOAnchorPosX(0, 3f).SetEase(Ease.InOutExpo);
            yield return new WaitForSeconds(3f);
        }

        private IEnumerator MoveElevator()
        {
            yield return new WaitForSeconds(2f);

            if (targetFloor == currentFloor) yield break;

            currentFloor++;

            UpdateUI();

            if (_targetHomes.ContainsKey(currentFloor))
            {
                StartCoroutine(OpenElevatorDoor());
                yield break;
            }

            if (UnityEngine.Random.Range(0, 100) < 30)
            {
                StartCoroutine(OpenElevatorDoor());
                yield break;
            }

            if (currentFloor == targetFloor)
            {
                Debug.Log("목표층에 도착");
            }
            else
            {
                StartCoroutine(MoveElevator());
            }
        }
    }
}
