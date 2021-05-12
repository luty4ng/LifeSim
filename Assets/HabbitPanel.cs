using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Michsky.UI.ModernUIPack;

public class HabbitPanel : MonoBehaviour
{
    [SerializeField] private Protagonist _protagonist;
    [SerializeField] private Dictionary<string, HorizontalSelector> _selector;
  
    void Start()
    {
        _protagonist = GameManager.instance.playerAgent.GetComponent<Protagonist>();
        _selector = new Dictionary<string, HorizontalSelector>();

        HorizontalSelector[] viewers = transform.Find("Content").GetComponentsInChildren<HorizontalSelector>();
        foreach (var item in viewers)
        {
            _selector.Add(item.name, item);
        }
        for(int i = 0; i < 3; i++)
        {
            _selector["EatSelector"].itemList[i].itemTitle = _protagonist._habbits._habbitDic["Eat"][i]._name;
        }
        

    }
    public void WorkUpdate(GameObject whichHabbit)
    {
        Debug.Log(whichHabbit);
    }
    public void SleepUpdate(GameObject whichHabbit)
    {
        Debug.Log(whichHabbit);
    }
    public void EatUpdate(GameObject whichHabbit)
    {
        Debug.Log(whichHabbit);
    }
    public void SportUpdate(GameObject whichHabbit)
    {
        Debug.Log(whichHabbit);
    }
}
