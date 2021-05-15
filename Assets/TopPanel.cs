using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TopPanel : MonoBehaviour
{
    [SerializeField] private Protagonist _protagonist;
    public TextMeshProUGUI _moneyOnUI;
    public TextMeshProUGUI _ageOnUI;
    void Start()
    {
        _protagonist = GameManager.instance.playerAgent.GetComponent<Protagonist>();
        _moneyOnUI.text = _protagonist.GetMoney().ToString();
        _ageOnUI.text = _protagonist.GetAge().ToString();
        EventCenter.GetInstance().AddEventListener("UpdateUI", () => {
            _moneyOnUI.text = _protagonist.GetMoney().ToString();
            _ageOnUI.text = _protagonist.GetAge().ToString();
        });
    }
}
