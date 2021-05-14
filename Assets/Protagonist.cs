using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;

using Config.Buffs;
using Config.Habbits;
using Config.Traits;
using Config.RandEvents;
using Config.Behavious;

public class Protagonist : MonoBehaviour
{
    public Animator animator;
    public Habbits habbits;
    public Behaviours behaviours;
    public RandEvents randEvents;
    public Buffs buffs;
    public Traits traits;
    

    [Header("整体设定"), Space]
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
    [LabelText("中年期年龄")]
    public float middleAge;
    [LabelText("老年期年龄")]
    public float olderAge;
    private string[] habbitselection = new string[4];
    public void Init()
    {
        _age = 18f;
        _ownMoney = 0f;
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
    private List<string> _behaviourSelect = new List<string>();
    private Dictionary<string, float> _behavBook = new Dictionary<string, float>(); 
    private List<string> _traits = new List<string>();
    
    void Awake()
    {
        GameObject pools = GameObject.Find("Pools");
        habbits = pools.GetComponentInChildren<Habbits>();
        behaviours = pools.GetComponentInChildren<Behaviours>();
        randEvents = pools.GetComponentInChildren<RandEvents>();
        buffs = pools.GetComponentInChildren<Buffs>();
        traits = pools.GetComponentInChildren<Traits>();
    }

    void Start()
    {
        Init();
        foreach (var behav in behaviours._behaviourList)
        {
            if(!_behavBook.ContainsKey(behav._name))
                _behavBook.Add(behav._name, 0f);
            _behavBook[behav._name] = 0f;
        }
        EventCenter.GetInstance().AddEventListener("UpdateData", UpdateData);
    }

    public float GetAge() => _age;
    public float GetMoney() => _ownMoney;
    public float GetSans() => _overallSans;
    public float GetHealth()=> _overallHealth;
    public float[] GetModule() => new float[8]{ _motor, _nerve, _endoc, _circul, _breath, _digest, _urinary, _reprod };
    public string GetStage()
    {
        if(_age < middleAge)
            return "青年期";
        else if(_age >= middleAge && _age < olderAge)
            return "中年期";
        else
            return "老年期";
    }
    public void UpdateHabbitSelect(string eat, string sleep, string sport, string work)
    {
        _habbitSelect["Eat"] = eat;
        _habbitSelect["Sleep"] = sleep;
        _habbitSelect["Sport"] = sport;
        _habbitSelect["Work"] = work;
    }

    public void UpdateBehaviourSelect(List<string> transmitBehav)
    {
        if(_behaviourSelect.Count > 0)
            _behaviourSelect.Clear();
        _behaviourSelect = transmitBehav;

        Dictionary<string, float> tmpBook = new Dictionary<string, float>(_behavBook);

        foreach (var behavName in tmpBook.Keys)
        {
            if(transmitBehav.Contains(behavName))
            {
                _behavBook[behavName] += 1f;
                Debug.Log("【在】"+behavName);
            }
            else
            {
                if(GetBehavByName(behavName)._stopYear <= _behavBook[behavName] && GetBehavByName(behavName)._stopYear!=0)
                {
                    Debug.Log(behavName + "戒断反应");
                }
                //_behavBook[behavName] = 0f;
            }

        }
    }

    Config.Behavious.Behaviour GetBehavByName(string name)
    {
        foreach (var item in behaviours._behaviourList)
        {
            if(item._name.Trim() == name.Trim())
                return item;
        }
        return null;
    } 

    public void UpdateByBehaviour(Config.Behavious.Behaviour tmpBehaviour)
    {
        _overallSans += tmpBehaviour._sans;
        _motor += tmpBehaviour._motor;
        _nerve += tmpBehaviour._nerve;
        _endoc += tmpBehaviour._endoc;
        _circul += tmpBehaviour._circul;
        _breath += tmpBehaviour._breath;
        _digest += tmpBehaviour._digest;
        _urinary += tmpBehaviour._urinary;
        _reprod += tmpBehaviour._reprod;
        _ownMoney += tmpBehaviour._money;

        if(tmpBehaviour._randEvent!=null)
        {
            tmpBehaviour._randEvent.Invoke();
        }

        if(_behavBook.ContainsKey(tmpBehaviour._name))
        {
            //Debug.Log("是否"+_behavBook[tmpBehaviour._name]+" > "+tmpBehaviour._buffYear + "  ??");
            if(_behavBook[tmpBehaviour._name] > tmpBehaviour._buffYear && tmpBehaviour._buffYear != 0f)
            {
                tmpBehaviour._buff.Invoke();
            }
        }
    }
    public void UpdateByHabbit(Habbit tmpHabbit)
    {
        _overallSans += tmpHabbit._sans;
        _motor += tmpHabbit._motor;
        _nerve += tmpHabbit._nerve;
        _endoc += tmpHabbit._endoc;
        _circul += tmpHabbit._circul;
        _breath += tmpHabbit._breath;
        _digest += tmpHabbit._digest;
        _urinary += tmpHabbit._urinary;
        _reprod += tmpHabbit._reprod;
        _ownMoney += tmpHabbit._money;

        if(tmpHabbit._randEvent!=null)
        {
            tmpHabbit._randEvent.Invoke();
        }
    }
    public void CheckCondition()
    {
        if( _overallSans < 0 || _motor < 0 || _nerve < 0 || _endoc < 0 || _circul < 0 || _breath < 0 || _digest < 0 || _urinary < 0 || _reprod < 0)
        {
            Debug.Log("GAME OVER");
        }
        _overallSans = _overallSans > 30 ? 30 : _overallSans;
        _motor = _motor > 30 ? 30 : _motor;
        _nerve = _nerve > 30 ? 30 : _nerve;
        _endoc = _endoc > 30 ? 30 : _endoc;
        _circul = _circul > 30 ? 30 : _circul;
        _breath = _breath > 30 ? 30 : _breath;
        _digest = _digest > 30 ? 30 : _digest;
        _urinary = _urinary > 30 ? 30 : _urinary;
        _reprod = _reprod > 30 ? 30 : _reprod;
    }
    public void UpdateData()
    {
        _age += 1f;
        float thisMoneyChange = moneyChange;

        _ownMoney += thisMoneyChange;

        // 更新习惯带来的影响
        foreach (var selected in _habbitSelect)
        {
            foreach (var habbit in habbits.habbitDic[selected.Key])
            { 
                if(habbit._name.Trim() == selected.Value.Trim())
                {
                    UpdateByHabbit(habbit);
                }
            }
        }

        // 更新行为带来的影响
        foreach (var selected in _behaviourSelect)
        {
            foreach (var behav in behaviours._behaviourList)
            {
                if(behav._name.Trim() == selected.Trim())
                {
                    UpdateByBehaviour(behav);
                }
            }
        }




        CheckCondition();

    }
}
