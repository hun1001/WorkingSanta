using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Prototype;
using UnityEngine.SceneManagement;

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
        

        private void Awake()
        {
            ButtonManager.Instance.AddHandler(this);
            ShopScroll.gameObject.SetActive(false);
        }

        private void OnDeliveryStart()
        {
            if (!weekwork)
                CharacterStat.hp -= 40;
            else
                CharacterStat.hp -= 56;
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
                wnd.text = "�ָ�����";
            }
            else
            {
                weekwork = false;
                wnd.text = "���Ͼ˹�";
            }
        }
        private void OnPI()
        {
            Debug.Log("����");
        }
        private void OnPH()
        {
            Debug.Log("����");
        }
        private void OnPF()
        {
            Debug.Log("����");
        }
        private void OnPR()
        {
            Debug.Log("����");
        }
        private void OnPG()
        {
            Debug.Log("����");
        }
        private void OnPC()
        {
            Debug.Log("����");
        }
    }
}
