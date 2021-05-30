using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Sirenix.OdinInspector;
public class TopPanel : MonoBehaviour
{
    [SerializeField] private Protagonist _protagonist;
    public TextMeshProUGUI _moneyOnUI;
    public TextMeshProUGUI _ageOnUI;
    public GameObject hintPanel;
    public GameObject settingPanel; 
    [LabelText("提示信息"), TextArea]
    public string hintInfo;

    void Start()
    {
        _protagonist = GameObject.Find("Protagonist").GetComponent<Protagonist>();
        _moneyOnUI.text = _protagonist.GetMoney().ToString();
        _ageOnUI.text = _protagonist.GetAge().ToString();
        EventCenter.GetInstance().AddEventListener("UpdateUI", () => {
            _moneyOnUI.text = _protagonist.GetMoney().ToString();
            _ageOnUI.text = _protagonist.GetAge().ToString();
        });
    }

    public void GetHint()
    {
        hintPanel.SetActive(true);
    }
    public void CloseHint()
    {
        hintPanel.SetActive(false);
    }

    public void GetSetting()
    {
        settingPanel.SetActive(true);
    }
    public void CloseSetting()
    {
        settingPanel.SetActive(false);
    }
}
