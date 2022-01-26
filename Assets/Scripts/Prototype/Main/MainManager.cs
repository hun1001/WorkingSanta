using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Prototype;
using UnityEngine.SceneManagement;
using System;
using DG.Tweening;

namespace Prototype_Main
{
    public class MainManager : MonoBehaviour
    {
        public static bool Weekwork; 

        [SerializeField]
        private Image AlbaIcon;
        [SerializeField]
        private Sprite Delivery;
        [SerializeField]
        private Text wnd;
        [SerializeField]
        private GameObject ShopScroll;
        [SerializeField]
        private CanvasGroup endingGroup;

        private bool weekwork = false;
        

        private void Awake()
        {
            ButtonManager.Instance.AddHandler(this);
            ShopScroll.gameObject.SetActive(false);

            if (CharacterStat.RemainingDays <= 0)
            {
                StartCoroutine(Ending());
            }
        }

        private IEnumerator Ending()
        {
            if(CharacterStat.Money >= 500)
            {
                endingGroup.gameObject.transform.GetChild(0).GetComponent<Text>().text = "게임 클리어\n\n당신은 불경기에도 돈은 모아\n어린이들에게 꿈과 희망을 전하는데\n성공하셨습니다.";
            }
            else
            {
                endingGroup.gameObject.transform.GetChild(0).GetComponent<Text>().text = "최선을 다하여 돈을 번 당신이지만 자본의 벽은 높았습니다.\n어린아이들에게 선물을 전달하지 못하였지만 아이들은\n자신들이 나빴기 때문이라며 자책하는군요, 당신의 탓은 아닌가봅니다.";
            }
            yield return new WaitForSeconds(1f);
            endingGroup.DOFade(1f, 1f);
        }

        private void OnDeliveryStart()
        {
            if (!weekwork)
            {
                CharacterStat.Hp -= 40;
                if (CharacterStat.Hp < 0)
                {
                    UIManager.Instance.ShowWarning("체력이 부족합니다.");
                    CharacterStat.Hp += 40;
                    return;
                }
            }
            else
            {
                CharacterStat.Hp -= 56;
                if (CharacterStat.Hp < 0)
                {
                    UIManager.Instance.ShowWarning("체력이 부족합니다.");
                    CharacterStat.Hp += 56;
                    return;
                }
            }

            //road other scene
            Weekwork = weekwork;
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

        private void OnRest()
        {
            CharacterStat.Hp += 50;
            CharacterStat.RemainingDays -= 7;
        }

        private void OnTuto()
        {
            UIManager.Instance.tutoPanelToggle();
        }

        private void OnGameExitButton()
        {

        }

        private void OnGameRestartButton()
        {

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
