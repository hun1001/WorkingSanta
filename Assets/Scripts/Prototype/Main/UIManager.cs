using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoSingleton<UIManager>
{
    [SerializeField]
    private Text days;
    [SerializeField]
    private Image hpBar;
    [SerializeField]
    private Text moneyText;
    [SerializeField]
    private GameObject tutoPanel;

    private void Start()
    {
        tutoPanel.SetActive(false);
    }

    private void Update()
    {
        days.text = $"{CharacterStat.RemainingDays}-Days";
        hpBar.fillAmount = (float)((float)CharacterStat.Hp / 100f);
        moneyText.text = $"{CharacterStat.Money}¸¸¿ø";
    }

    public void tutoPanelToggle()
    {
        if (tutoPanel.activeSelf)
        {
            tutoPanel.SetActive(false);
        }
        else
        {
            tutoPanel.SetActive(true);
        }
    }
}
