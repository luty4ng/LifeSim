using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;

public class Protagonist : MonoBehaviour
{
    public Animator animator;
    public Config.Habbits.Habbits habbits;
    public Config.Behavious.Behaviours behaviours;
    public Config.RandEvents.RandEvents randEvents;
    public Config.Buffs.Buffs buffs;
    public Config.Traits.Traits traits;
    

    [Header("角色基础设定"), Space]
    [LabelText("健康值上限")]
    public float maxHealth;
    [LabelText("精神值上限")]
    public float maxSans;
    [LabelText("各模块值上限")]
    public float maxMoudle;
    [LabelText("每回合恢复模块值（默认）")]
    public float moduleChange;
    [LabelText("每回合恢复精神值（默认）")]
    public float sansChange;
    [LabelText("每回合金钱变化（默认）")]
    public float moneyChange;
    [LabelText("角色特性数目")]
    public float traitNum = 2;

    [Header("年龄区间设定"), Space]
    [LabelText("【中年期】年龄（分界点）")]
    public float middleAge;
    [LabelText("【老年期】年龄（分界点）")]
    public float olderAge;

    [Header("模块区间设定"), Space]
    [LabelText("【正常】模块值（分界点）")]
    public float normalModule = 10f;
    [LabelText("【健康】模块值（分界点）")]
    public float healthyModule = 20f;
    
    [Header("初始化设定"), Space]
    [LabelText("初始年龄")]
    public float initAge;
    [LabelText("初始健康值")]
    public float initHealth;
    [LabelText("初始模块值")]
    public float initModule;
    [LabelText("初始精神值")]
    public float initSans;
    [LabelText("初始金钱")]
    public float initMoney;
    private string[] habbitselection = new string[4];
    public void Init()
    {
        _age = initAge;
        _ownMoney = initMoney;
        _motor = initModule;
        _nerve = initModule;
        _endoc = initModule;
        _circul = initModule; 
        _breath = initModule; 
        _digest = initModule; 
        _urinary = initModule;
        _reprod = initModule;
        _overallSans = initSans;
        _overallHealth = initHealth;
    }
    private float _ownMoney;
    private float _age;
    private float _overallHealth;
    private float _overallSans;
    private float _motor, _nerve, _endoc, _circul, _breath, _digest, _urinary, _reprod;
    private Dictionary<string, string> _habbitSelect = new Dictionary<string, string>();
    private List<string> _behaviourSelect = new List<string>();
    private Dictionary<string, float> _behavBook = new Dictionary<string, float>(); 
    private Dictionary<string,string> _traitList = new Dictionary<string,string>();
    [ShowInInspector, FoldoutGroup("DEBUG")] private Dictionary<string, Config.Buffs.Buff> _buffList = new Dictionary<string, Config.Buffs.Buff>();
    [ShowInInspector, FoldoutGroup("DEBUG")] private Dictionary<string, float> _buffCount = new Dictionary<string, float>();
    
    void Awake()
    {
        GameObject pools = GameObject.Find("Pools");
        habbits = pools.GetComponentInChildren<Config.Habbits.Habbits>();
        behaviours = pools.GetComponentInChildren<Config.Behavious.Behaviours>();
        randEvents = pools.GetComponentInChildren<Config.RandEvents.RandEvents>();
        buffs = pools.GetComponentInChildren<Config.Buffs.Buffs>();
        traits = pools.GetComponentInChildren<Config.Traits.Traits>();

        List<Config.Traits.Trait> tmpTraitList = new List<Config.Traits.Trait>(traits._traitList);
        for (int i = 0; i < traitNum; i++)
        {
            int tmpIndex = Random.Range(0, (int)(tmpTraitList.Count-1));
            _traitList.Add(tmpTraitList[tmpIndex]._name, tmpTraitList[tmpIndex]._desc);
            tmpTraitList.RemoveAt(tmpIndex);
        }
        Init();
    }

    void Start()
    {
        
        foreach (var behav in behaviours._behaviourList)
        {
            if(!_behavBook.ContainsKey(behav._name))
                _behavBook.Add(behav._name, 0f);
            _behavBook[behav._name] = 0f;
        }
        
        EventCenter.GetInstance().AddEventListener<Config.RandEvents.RandEvent>("随机事件影响", UpdateByEvents);
        EventCenter.GetInstance().AddEventListener<Config.Buffs.Buff>("新Buff触发", UpdateBuffList);
        EventCenter.GetInstance().AddEventListener("UpdateData", UpdateData);
        // EventCenter.GetInstance().AddEventListener("UpdateExistBuff", UpdateByBuffList);
    }

    public float GetAge() => _age;
    public float GetMoney() => _ownMoney;
    public float GetSans() => _overallSans;
    public float GetHealth() => _overallHealth;
    public float[] GetModule() => new float[8]{ _motor, _nerve, _endoc, _circul, _breath, _digest, _urinary, _reprod };
    public Dictionary<string, string> GetTraitsList() => new Dictionary<string, string>(_traitList);
    public Dictionary<string, float> GetBuffCount() => new Dictionary<string, float>(_buffCount);
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
                //Debug.Log("【在】"+behavName);
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

    private float CalculHealth()
    {
        int breakM = 0, normalM = 0, healthyM = 0;
        List<float> tmpModules = new List<float>(){ _motor, _nerve, _endoc, _circul, _breath, _digest, _urinary, _reprod };
        foreach (var module in tmpModules)
        {
            breakM = module < normalModule ? breakM + 1 : breakM;
            normalM = module >= normalModule && module < healthyModule ? normalM + 1 : normalM;
            healthyM = module >= healthyModule ? healthyM + 1 : healthyM;
        }
        float tmpHealthChange;
        if(breakM > 0)
        {
            if(breakM <= 2)
                tmpHealthChange = -1f;
            else if(breakM >2 && breakM <=5)
                tmpHealthChange = -5f;
            else// if(breakM > 5)
                tmpHealthChange = -10f;
        }
        else if(breakM==0)
        {
            if(healthyM <= 2)
                tmpHealthChange = 1f;
            else if(healthyM >2 && healthyM <=5)
                tmpHealthChange = 2f;
            else// if(breakM > 5)
                tmpHealthChange = 5f;
        }
        else
            tmpHealthChange = 0f;
        return tmpHealthChange;
    }
    private void UpdateByDefault()
    {
        _age += 1f;
        _ownMoney += moneyChange;
        _motor += moduleChange;
        _nerve += moduleChange;
        _endoc += moduleChange;
        _circul += moduleChange;
        _breath += moduleChange;
        _digest += moduleChange;
        _urinary += moduleChange;
        _reprod += moduleChange;
        _ownMoney += moduleChange;
        _overallHealth += CalculHealth(); 
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
    public void UpdateByHabbit(Config.Habbits.Habbit tmpHabbit)
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

    public void UpdateByEvents(Config.RandEvents.RandEvent tmpEvents)
    {
        Debug.Log(tmpEvents._name);
        if(tmpEvents._traitReq != null)
            if(!_traitList.ContainsKey(tmpEvents._traitReq))
                return;
        
        _overallSans += tmpEvents._sans;
        _motor += tmpEvents._motor;
        _nerve += tmpEvents._nerve;
        _endoc += tmpEvents._endoc;
        _circul += tmpEvents._circul;
        _breath += tmpEvents._breath;
        _digest += tmpEvents._digest;
        _urinary += tmpEvents._urinary;
        _reprod += tmpEvents._reprod;
        _ownMoney += tmpEvents._money;

        if(tmpEvents._buff!=null)
        {
            tmpEvents._buff.Invoke();
        }
    }
    public void UpdateByBuffList()
    {
        if(_buffList == null)
            return;

        if(_buffList.Count == 0)
            return;

        Dictionary<string, float> tmpList = new Dictionary<string, float>(_buffCount);

        foreach (var buffName in tmpList.Keys)
        {
            _overallSans += _buffList[buffName]._sans;
            _motor += _buffList[buffName]._motor;
            _nerve += _buffList[buffName]._nerve;
            _endoc += _buffList[buffName]._endoc;
            _circul += _buffList[buffName]._circul;
            _breath += _buffList[buffName]._breath;
            _digest += _buffList[buffName]._digest;
            _urinary += _buffList[buffName]._urinary;
            _reprod += _buffList[buffName]._reprod;
            _ownMoney += _buffList[buffName]._money;
            _buffCount[buffName] -= 1f;

            if(_buffCount[buffName] <= 0f)
            {
                _buffCount.Remove(buffName);
                _buffList.Remove(buffName);
                EventCenter.GetInstance().EventTrigger<string>("DestroyBuffObj", buffName);
            }
        }
    }
    public void UpdateBuffList(Config.Buffs.Buff tmpBuff)
    {
        if(_buffCount.ContainsKey(tmpBuff._name) && _buffList.ContainsKey(tmpBuff._name))
        {
            _buffCount[tmpBuff._name] = tmpBuff._remainYears == 0f ? 100f : tmpBuff._remainYears;
            return;
        }
        _buffList.Add(tmpBuff._name, tmpBuff);
        if(tmpBuff._remainYears == 0f)
            _buffCount.Add(tmpBuff._name, 100f);
        else
            _buffCount.Add(tmpBuff._name, tmpBuff._remainYears);
    }

    public void CheckCondition()
    {
        if( _overallHealth < 0 || _overallSans < 0 || _motor < 0 || _nerve < 0 || _endoc < 0 || _circul < 0 || _breath < 0 || _digest < 0 || _urinary < 0 || _reprod < 0)
        {
            Debug.Log("GAME OVER");
            EventCenter.GetInstance().EventTrigger("GAMEOVER");
        }
        _overallSans = _overallSans > 30 ? 30 : _overallSans;
        _overallHealth = _overallHealth > 30 ? 30 : _overallHealth;
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
        // 优先更新当前BUFF集合的影响
        UpdateByBuffList();

        // 更新工作模式带来的概率系数影响
        if(_habbitSelect["Work"] == "究极卷王")
        {
            randEvents.multiplier = 2f;
        }
        else if(_habbitSelect["Work"] == "准时下班")
        {
            randEvents.multiplier = 1f;
        }
        else if(_habbitSelect["Work"] == "摸鱼选手")
        {
            randEvents.multiplier = 0.5f;
        }

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
        // 更新默认设定的影响
        UpdateByDefault();
        // 修正超出上下限的值
        CheckCondition();

    }
}
