using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Prototype.Delivery
{
    public enum Direction
    {
        Left = 1,
        Right
    }

    class ResultStat
    {
        public int Total;
        public List<int> SuccessFloorList = new List<int>();
        public List<int> FailFloorList = new List<int>();
    }

    public class DeliveryManager : MonoSingleton<DeliveryManager>
    {
        [SerializeField] Text FloorListContent;
        [SerializeField] Button CloseFloorListButton;
        [SerializeField] CanvasGroup FloorList;

        private Dictionary<int, Direction> targetHomes;
        private ResultStat resultStat;
        private int score = 0;

        private void Awake()
        {
            targetHomes = new Dictionary<int, Direction>(){
                {2, Direction.Left},
                {5, Direction.Left}
            };
            resultStat = new ResultStat();

            FloorListContent.text = "";

            foreach (var targetHome in targetHomes)
            {
                FloorListContent.text += $"{targetHome.Key}0{(int)targetHome.Value}호\n";
            }
            OpenFloorList();
        }

        private void Start()
        {
            UpdateUI();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                if (FloorList.alpha == 0)
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
            
        }

        private void CloseFloorList()
        {
            FloorList.alpha = 0;
            FloorList.blocksRaycasts = false;
        }

        private void OpenFloorList()
        {
            FloorList.alpha = 1;
            FloorList.blocksRaycasts = true;
        }

        private void DropItem(Direction direction)
        {
            
        }
    }
}
