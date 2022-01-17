using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

        private int currentFloor = 0;

        private void Awake()
        {
            _buttonCloseFloorList.onClick.AddListener(() => closeFloorList());
            foreach (int targetHome in _targetHomes)
            {
                _textFloorListContent.text += $"{targetHome}호\n";
            }
        }

        private void Start()
        {
            currentFloor = 0;
            updateUI();
        }

        private void Update()
        {
            
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

        private IEnumerator moveToNextFloor()
        {
            yield return new WaitForSeconds(1f);
            currentFloor++;
            updateUI();
        }
    }
}
