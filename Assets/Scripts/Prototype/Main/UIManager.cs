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
    [SerializeField]
    private GameObject Warning;

    private void Start()
    {
        tutoPanel.SetActive(false);
        Warning.SetActive(false);
    }

    private void Update()
    {
        days.text = $"{CharacterStat.RemainingDays}-Days";
        hpBar.fillAmount = (float)((float)CharacterStat.Hp / 100f);
        moneyText.text = $"{CharacterStat.Money}¸¸¿ø";
    }

    public void ShowWarning(string content)
    {
        StartCoroutine(ShowWarningCoruotine(content));
    }

    private IEnumerator ShowWarningCoruotine(string content)
    {
        Warning.SetActive(true);
        Warning.GetComponent<Text>().text = content;
        yield return new WaitForSeconds(0.5f);
        Warning.SetActive(false);
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
