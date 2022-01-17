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
            public List<int> successFloorList;
            public List<int> failFloorList;
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
            _buttonCloseFloorList.onClick.AddListener(() => closeFloorList());

            _buttonDropLeft.onClick.AddListener(() => dropItem(Direction.Left));
            _buttonDropRight.onClick.AddListener(() => dropItem(Direction.Right));

            _textFloorListContent.text = "";
            _resultStat = new ResultStat();
            _targetHomes = new Dictionary<int, Direction>(){
                {2, Direction.Left},
                {5, Direction.Left}
            };
            foreach (var targetHome in _targetHomes)
            {
                _textFloorListContent.text += $"{targetHome.Key}0{GetIntDirection(targetHome.Value)}호\n";
            }

            openFloorList();
        }

        private void Start()
        {
            currentFloor = 1;
            targetFloor = topFloor;
            updateUI();
            StartCoroutine(moveToNextFloor());
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                if (_canvasGroupFloorList.alpha == 0)
                {
                    openFloorList();
                }
                else
                {
                    closeFloorList();
                }
            }
        }

        private void updateUI()
        {
            _textFloor.text = currentFloor.ToString();
        }

        private void closeFloorList()
        {
            _canvasGroupFloorList.alpha = 0;
            _canvasGroupFloorList.blocksRaycasts = false;
        }

        private void openFloorList()
        {
            _canvasGroupFloorList.alpha = 1;
            _canvasGroupFloorList.blocksRaycasts = true;
        }

        private IEnumerator openElevatorDoor()
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
                StartCoroutine(closeElevatorDoor());
                yield return new WaitForSeconds(3f);
                StartCoroutine(moveToNextFloor());
            }
        }

        private void dropItem(Direction direction)
        {
            if (!elevatorDoorOpen)
            {
                return;
            }

            if (direction == Direction.Left)
            {
                if (_targetHomes[currentFloor] == Direction.Left)
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

        private IEnumerator closeElevatorDoor()
        {
            elevatorDoorOpen = false;
            _rectTransformElevatorDoorLeft.DOAnchorPosX(0, 3f).SetEase(Ease.InOutExpo);
            _rectTransformElevatorDoorRight.DOAnchorPosX(0, 3f).SetEase(Ease.InOutExpo);
            yield return new WaitForSeconds(3f);
        }

        private IEnumerator moveToNextFloor()
        {
            yield return new WaitForSeconds(2f);

            if (targetFloor == currentFloor) yield break;

            if (targetFloor > currentFloor)
            {
                currentFloor++;
            }
            else
            {
                currentFloor--;
            }

            updateUI();

            if (UnityEngine.Random.Range(0, 100) < 50)
            {
                StartCoroutine(openElevatorDoor());
                yield break;
            }

            if (currentFloor == targetFloor)
            {
                StartCoroutine(openElevatorDoor());
            }
            else
            {
                StartCoroutine(moveToNextFloor());
            }
        }

        private int GetIntDirection(Direction direction)
        {
            return (direction == Direction.Left) ? 1 : 2;
        }
    }
}
