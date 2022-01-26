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
                wnd.text = "주말포함";
            }
            else
            {
                weekwork = false;
                wnd.text = "평일알바";
            }
        }
        private void OnPI()
        {
            Debug.Log("구매");
        }
        private void OnPH()
        {
            Debug.Log("구매");
        }
        private void OnPF()
        {
            Debug.Log("구매");
        }
        private void OnPR()
        {
            Debug.Log("구매");
        }
        private void OnPG()
        {
            Debug.Log("구매");
        }
        private void OnPC()
        {
            Debug.Log("구매");
        }
    }
}
