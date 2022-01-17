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
        public int CurrentFloor
        {
            get { return currentFloor; }
        }

        [SerializeField] int[] _targetHomes;
        [SerializeField] Text _textFloor;

        [SerializeField] Text _textFloorListContent;
        [SerializeField] Button _buttonCloseFloorList;
        [SerializeField] CanvasGroup _canvasGroupFloorList;

        [SerializeField] RectTransform _rectTransformElevatorDoorLeft;
        [SerializeField] RectTransform _rectTransformElevatorDoorRight;

        private int currentFloor = 1;

        private void Awake()
        {
            _buttonCloseFloorList.onClick.AddListener(() => closeFloorList());
            _textFloorListContent.text = "";
            foreach (int targetHome in _targetHomes)
            {
                _textFloorListContent.text += $"{targetHome}호\n";
            }
            openFloorList();
        }

        private void Start()
        {
            currentFloor = 1;
            updateUI();
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

        private void openElevatorDoor()
        {

        }

        private IEnumerator moveToNextFloor()
        {
            yield return new WaitForSeconds(2f);
            currentFloor++;
            updateUI();

            
        }
    }
}
