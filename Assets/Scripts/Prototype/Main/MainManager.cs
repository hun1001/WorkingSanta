using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Prototype;
using UnityEngine.SceneManagement;
using System;

namespace Prototype_Main
{
    public class MainManager : MonoBehaviour
    {
        [SerializeField]
        private Image AlbaIcon;
        [SerializeField]
        private Sprite Delivery;
        [SerializeField]
        private Text wnd;
        [SerializeField]
        private GameObject ShopScroll;
        private bool weekwork = false;
        private int hp = 100;

        private void Awake()
        {
            ButtonManager.Instance.AddHandler(this);
            ShopScroll.gameObject.SetActive(false);
        }

        private void OnDeliveryStart()
        {
            if (!weekwork)
                hp -= 40;
            else
                hp -= 56;
            //road other scene
            SceneManager.LoadScene("Prototype");
        }
        private void OnApplyD()
        {
            AlbaIcon.sprite = Delivery;
        }
        private void OnShop()
        {
            if (ShopScroll.gameObject.activeSelf == true)
            {
                ShopScroll.gameObject.SetActive(false);
            }
            else
            {
                ShopScroll.gameObject.SetActive(true);
            }
        }
        private void OnAlba()
        {

        }
        private void OnWeekendAlba()
        {
            if(!weekwork)
            {
                weekwork = true;
                wnd.text = "주말포함";
            }
            else
            {
                weekwork = false;
                wnd.text = "평일알바";
            }
        }

        #region 상점 버튼
        private void OnPI()
        {
            BuyItem(Item.InfoKillingLicense, 1_000_000);
        }
        private void OnPH()
        {
            BuyItem(Item.Hamburger, 500_000);
        }
        private void OnPF()
        {
            BuyItem(Item.Fairly, 2_500_000);
        }
        private void OnPR()
        {
            BuyItem(Item.Rudolf, 2_500_000);
        }
        private void OnPG()
        {
            BuyItem(Item.Rayder, 2_500_000);
        }
        private void OnPC()
        {
            BuyItem(Item.Vaccine, 3_000_000);
        }
        #endregion

        private void BuyItem(Item infoKillingLicense, int price)
        {
            if (price > CharacterStat.Money)
            {
                return;
            }
            CharacterStat.Money -= price;
            CharacterStat.AddItem(infoKillingLicense);
        }
    }
}
