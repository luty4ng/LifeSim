using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Michsky.UI.ModernUIPack;

public class HabbitPanel : MonoBehaviour
{
    [SerializeField] private Protagonist _protagonist;
    [SerializeField] private Dictionary<string, HorizontalSelector> _selector;

    public TextMeshProUGUI _workSelection;
    public TextMeshProUGUI _eatSelection;
    public TextMeshProUGUI _sleepSelection;
    public TextMeshProUGUI _sportSelection;
  
    void Start()
    {
        EventCenter.GetInstance().AddEventListener("TransitData", () => {
            _protagonist.UpdateHabbitSelect(_eatSelection.text, _sleepSelection.text, _sportSelection.text, _workSelection.text);
        });

        _protagonist = GameManager.instance.playerAgent.GetComponent<Protagonist>();
        _selector = new Dictionary<string, HorizontalSelector>();
    
        HorizontalSelector[] viewers = transform.Find("Content").GetComponentsInChildren<HorizontalSelector>();
        foreach (var item in viewers)
        {
            _selector.Add(item.name, item);
        }

        for(int i = 0; i < 3; i++)
        {
            _selector["EatSelector"].itemList[i].itemTitle = _protagonist.habbits.habbitDic["Eat"][i]._name;
            _selector["WorkSelector"].itemList[i].itemTitle = _protagonist.habbits.habbitDic["Work"][i]._name;
            _selector["SleepSelector"].itemList[i].itemTitle = _protagonist.habbits.habbitDic["Sleep"][i]._name;
            _selector["SportSelector"].itemList[i].itemTitle = _protagonist.habbits.habbitDic["Sport"][i]._name;
        }
    }

    public bool CheckSelection(string select)
    {
        if(_eatSelection.text == select.Trim() || _sleepSelection.text == select.Trim() || _sportSelection.text == select.Trim() || _workSelection.text == select.Trim())
            return true;
        else
            return false;
    }

    
}
