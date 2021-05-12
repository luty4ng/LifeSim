using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;

public class Protagonist : MonoBehaviour
{
    public Animator _animator;
    public Habbits _habbits;

    [Header("整体设定")]
    [LabelText("健康值上限")]
    public float maxHealth;
    [LabelText("精神值上限")]
    public float maxSans;
    [LabelText("各模块值上限")]
    public float maxMoudle;
    [LabelText("每回合恢复健康（默认）")]
    public float healthChange;
    [LabelText("每回合恢复精神（默认）")]
    public float sansChange;
    [LabelText("每回合金钱变化（默认）")]
    public float moneyChange;
    private string[] _habbitSelection = new string[4];
    public void Init()
    {
        _age = 18f;
        _ownMoney = 500f;
        _motor = 25f;
        _nerve = 25f;
        _endoc = 25f;
        _circul = 25f; 
        _breath = 25f; 
        _digest = 25f; 
        _urinary = 25f;
        _reprod = 25f;
        _overallSans = 25f;
        _overallHealth = 25f;
    }
    private float _ownMoney;
    private float _age;
    private float _overallHealth;
    private float _overallSans;
    private float _motor, _nerve, _endoc, _circul, _breath, _digest, _urinary, _reprod;
    private Dictionary<string, string> _habbitSelect = new Dictionary<string, string>();
    private List<string> _traits = new List<string>();
    
    void Awake()
    {
        _habbits = GetComponent<Habbits>();
    }

    void Start()
    {
        Init();
        EventCenter.GetInstance().AddEventListener("UpdateData", UpdateData);
    }

    public float GetAge() => _age;
    public float GetMoney() => _ownMoney;
    public float GetSans() => _overallSans;
    public float GetHealth()=> _overallHealth;
    public float[] GetModule() => new float[8]{ _motor, _nerve, _endoc, _circul, _breath, _digest, _urinary, _reprod };
    public void UpdateSelection(string eat, string sleep, string sport, string work)
    {
        Debug.Log("检查选择的习惯");
        _habbitSelect["Eat"] = eat;
        _habbitSelect["Sleep"] = sleep;
        _habbitSelect["Sport"] = sport;
        _habbitSelect["Work"] = work;
    }
    public void UpdateByHabbit(Habbit tmpHabbit)
    {
        Debug.Log(tmpHabbit._sans);
        _overallSans += tmpHabbit._sans;
        _motor += tmpHabbit._motor;
        _nerve += tmpHabbit._nerve;
        _endoc += tmpHabbit._endoc;
        _circul += tmpHabbit._circul;
        _breath += tmpHabbit._breath;
        _digest += tmpHabbit._digest;
        _urinary += tmpHabbit._urinary;
        _reprod += tmpHabbit._reprod;
    }

    public void UpdateData()
    {
        _age += 1f;
        float thisMoneyChange = moneyChange;

        _ownMoney += thisMoneyChange;


       foreach (var selected in _habbitSelect)
       {
           foreach (var extend in _habbits._habbitDic[selected.Key])
           {
               Debug.Log("Name: " + extend._name);
               Debug.Log("Value: " + selected.Value);
               if(extend._name == selected.Value)
               {
                   UpdateByHabbit(extend);
               }
           }
       }

        //Debug.Log(_overallSans);
    }
}
