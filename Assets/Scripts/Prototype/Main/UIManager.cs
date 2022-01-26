using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoSingleton<UIManager>
{
    [SerializeField]
    private Image hpBar;
    [SerializeField]
    private Text moneyText;
    public void Update()
    {
        hpBar.fillAmount = (float)((float)CharacterStat.hp / 100f);
        moneyText.text = $"{CharacterStat.Money}¸¸¿ø";
    }
}
