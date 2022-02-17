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
        private Sprite Together;
        [SerializeField]
        private Image wnd;
        [SerializeField]
        private GameObject ShopScroll;
        [SerializeField]
        private GameObject InvScroll;
        [SerializeField]
        private CanvasGroup endingGroup;
        [SerializeField]
        private AudioSource ShopSound0;
        [SerializeField]
        private AudioSource ShopSound1;
        [SerializeField]
        private Text tutoText;

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
            if(AlbaIcon.sprite == null){
                UIManager.Instance.ShowWarning("알바를 선택하지 않았습니다.");
                return;
            }
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
            if(AlbaIcon.sprite==Delivery)
            {
                SceneManager.LoadScene("Prototype");
            }
            else
            {
                SceneManager.LoadScene("EldenScene");
            }
        }
        private void OnApplyD()
        {
            AlbaIcon.sprite = Delivery;
        }
        private void OnApplyT()
        {
            AlbaIcon.sprite = Together;

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
            if (InvScroll.gameObject.activeSelf == true)
            {
                InvScroll.gameObject.SetActive(false);
            }
            else
            {
                InvScroll.gameObject.SetActive(true);
            }
        }
        private void OnWeekendAlba()
        {
            if (!weekwork)
            {
                weekwork = true;
                wnd.color = new Color(1f, 1f, 1f, 1);
            }
            else
            {
                weekwork = false;         
                wnd.color = new Color(0.5f, 0.5f, 0.5f, 1);
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
        private void OnTutoD()
        {
            UIManager.Instance.tutoPanelToggle();
            tutoText.text = ("튜토리얼\n\n1.배달해야 할 호수와 택배의 모양을 확인한다. 단 한번만\n확인 할 수 있다!\n\n" +
            "2.가야하는 층의 버튼을 모두 누른다 하나라도 잘못된 층을 누른다면 움직이지 않는다.\n\n" +
            "3.택배를 알맞은 곳에 옮긴다.");
        }
        private void OnTutoT()
        {
            UIManager.Instance.tutoPanelToggle();
            tutoText.text = ("튜토리얼\n\n1.화면을 좌, 우로 드래그 하는것으로 차선을 바꿀 수 있다.\n뒤로 드래그하여 유지하는 동안은 감속한다!\n\n" +
            "2.다른 배달부의 근처에 간다면 배달부가 차선을 바꿔서 방해 할 것이다.\n\n" +
            "3.경찰차의 근처에서는 천천히 이동해야 단속에 걸리지 않는다.");
        }

        private void OnGameExitButton()
        {
            CharacterStat.SaveData();
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

        private void OnGameRestartButton()
        {
            CharacterStat.Reset();
            CharacterStat.SaveData();
            SceneManager.LoadScene("SampleScene");
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
                ShopSound1.Play();
                UIManager.Instance.ShowWarning("돈이 부족합니다.");
                return;
            }
            ShopSound0.Play();
            CharacterStat.Money -= price;
            CharacterStat.AddItem(infoKillingLicense);
        }
    }
}
