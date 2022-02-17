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
                endingGroup.gameObject.transform.GetChild(0).GetComponent<Text>().text = "���� Ŭ����\n\n����� �Ұ�⿡�� ���� ���\n��̵鿡�� �ް� ����� ���ϴµ�\n�����ϼ̽��ϴ�.";
            }
            else
            {
                endingGroup.gameObject.transform.GetChild(0).GetComponent<Text>().text = "�ּ��� ���Ͽ� ���� �� ��������� �ں��� ���� ���ҽ��ϴ�.\n����̵鿡�� ������ �������� ���Ͽ����� ���̵���\n�ڽŵ��� ������ �����̶�� ��å�ϴ±���, ����� ſ�� �ƴѰ����ϴ�.";
            }
            yield return new WaitForSeconds(1f);
            endingGroup.DOFade(1f, 1f);
        }

        private void OnDeliveryStart()
        {
            if(AlbaIcon.sprite == null){
                UIManager.Instance.ShowWarning("�˹ٸ� �������� �ʾҽ��ϴ�.");
                return;
            }
            if (!weekwork)
            {
                CharacterStat.Hp -= 40;
                if (CharacterStat.Hp < 0)
                {
                    UIManager.Instance.ShowWarning("ü���� �����մϴ�.");
                    CharacterStat.Hp += 40;
                    return;
                }
            }
            else
            {
                CharacterStat.Hp -= 56;
                if (CharacterStat.Hp < 0)
                {
                    UIManager.Instance.ShowWarning("ü���� �����մϴ�.");
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
            tutoText.text = ("Ʃ�丮��\n\n1.����ؾ� �� ȣ���� �ù��� ����� Ȯ���Ѵ�. �� �ѹ���\nȮ�� �� �� �ִ�!\n\n" +
            "2.�����ϴ� ���� ��ư�� ��� ������ �ϳ��� �߸��� ���� �����ٸ� �������� �ʴ´�.\n\n" +
            "3.�ù踦 �˸��� ���� �ű��.");
        }
        private void OnTutoT()
        {
            UIManager.Instance.tutoPanelToggle();
            tutoText.text = ("Ʃ�丮��\n\n1.ȭ���� ��, ��� �巡�� �ϴ°����� ������ �ٲ� �� �ִ�.\n�ڷ� �巡���Ͽ� �����ϴ� ������ �����Ѵ�!\n\n" +
            "2.�ٸ� ��޺��� ��ó�� ���ٸ� ��޺ΰ� ������ �ٲ㼭 ���� �� ���̴�.\n\n" +
            "3.�������� ��ó������ õõ�� �̵��ؾ� �ܼӿ� �ɸ��� �ʴ´�.");
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

        #region ���� ��ư
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
                UIManager.Instance.ShowWarning("���� �����մϴ�.");
                return;
            }
            ShopSound0.Play();
            CharacterStat.Money -= price;
            CharacterStat.AddItem(infoKillingLicense);
        }
    }
}
