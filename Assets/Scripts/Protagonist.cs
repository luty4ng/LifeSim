using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;

public class Protagonist : MonoBehaviour
{
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
    [LabelText("每回合恢复模块值（默认1）")]
    public float moduleChange;
    [LabelText("每回合恢复精神值（默认2）")]
    public float sansChange;
    [LabelText("每回合随机事件数量上限(默认2)")]
    public float eventNum = 2;
    
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

    private bool _canChangeHabbit = true;
    [ShowInInspector, FoldoutGroup("DEBUG")] private bool _forceMoneyChange;
    private float _forceMoneyChangeValue = 0;
    private float _ownMoney;
    private float _age;
    private float _overallHealth;
    private float _overallSans;
    private float _motor, _nerve, _endoc, _circul, _breath, _digest, _urinary, _reprod;
    [ShowInInspector, FoldoutGroup("DEBUG")] private Dictionary<string, string> _habbitSelect = new Dictionary<string, string>();
    [ShowInInspector, FoldoutGroup("DEBUG")] private List<string> _behaviourSelect = new List<string>();
    [ShowInInspector, FoldoutGroup("DEBUG")] private Dictionary<string, float> _behavBook = new Dictionary<string, float>(); 
    [ShowInInspector, FoldoutGroup("DEBUG")] private Dictionary<string, Config.Traits.Trait> _traitList = new Dictionary<string, Config.Traits.Trait>();
    [ShowInInspector, FoldoutGroup("DEBUG")] private Dictionary<string, Config.Buffs.Buff> _buffList = new Dictionary<string, Config.Buffs.Buff>();
    [ShowInInspector, FoldoutGroup("DEBUG")] private Dictionary<string, float> _buffCount = new Dictionary<string, float>();
    [ShowInInspector, FoldoutGroup("DEBUG"), DisplayAsString ]private float _motorPer, _nervePer, _endocPer, _circulPer, _breathPer, _digestPer, _urinaryPer, _reprodPer, _moneyPer, _sansPer;
    
    private void Init()
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
    void Awake()
    {
        GameObject pools = GameObject.Find("Pools");
        traits = pools.GetComponentInChildren<Config.Traits.Traits>();
        habbits = pools.GetComponentInChildren<Config.Habbits.Habbits>();
        behaviours = pools.GetComponentInChildren<Config.Behavious.Behaviours>();
        randEvents = pools.GetComponentInChildren<Config.RandEvents.RandEvents>();
        buffs = pools.GetComponentInChildren<Config.Buffs.Buffs>();
        randEvents.eventNum = eventNum;
        List<Config.Traits.Trait> tmpTraitList = new List<Config.Traits.Trait>(traits._traitList);
        for (int i = 0; i < traitNum; i++)
        {
            int tmpIndex = Random.Range(0, (int)(tmpTraitList.Count-1));
            _traitList.Add(tmpTraitList[tmpIndex]._name, tmpTraitList[tmpIndex]);
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
        EventCenter.GetInstance().AddEventListener("Cure", Cure);
        EventCenter.GetInstance().AddEventListener("UpdateUI", ()=>{
            // Reset eventNum into pre-set value
            randEvents.eventNum = eventNum;
        });
    }

    public float GetAge() => _age;
    public float GetMoney() => _ownMoney;
    public float GetSans() => _overallSans;
    public float GetHealth() => _overallHealth;
    public float[] GetModule() => new float[8]{ _motor, _nerve, _endoc, _circul, _breath, _digest, _urinary, _reprod };
    public List<string> GetBehavSelect() => _behaviourSelect;
    public Dictionary<string, float> GetBuffCount() => new Dictionary<string, float>(_buffCount);
    public Dictionary<string, string> GetTraitsList()
    {
        Dictionary<string, string> traitDesc = new Dictionary<string, string>();
        foreach (var trait in _traitList)
        {
            if(!traitDesc.ContainsKey(trait.Key))
            {
                traitDesc.Add(trait.Key, trait.Value._desc);
            }
        }
        return traitDesc;
    }
    public string GetStage()
    {
        if(_age < middleAge)
            return "青年期";
        else if(_age >= middleAge && _age < olderAge)
            return "中年期";
        else
            return "老年期";
    }

    public string GetHealthState()
    {
        if(_overallHealth < normalModule)
            return "受损";
        else if(_overallHealth >= normalModule && _overallHealth < healthyModule)
            return "正常";
        else
            return "健康";
    }
    public void UpdateHabbitSelect(string eat, string sleep, string sport, string work)
    {
        if(_age == initAge)
        {
            _habbitSelect.Add("Eat", eat);
            _habbitSelect.Add("Sleep", sleep);
            _habbitSelect.Add("Sport", sport);
            _habbitSelect.Add("Work", work);
            _canChangeHabbit = true;
            return;
        }
        _canChangeHabbit = _age == middleAge || _age == olderAge ? true : false;
        if(_habbitSelect["Eat"] != eat || _habbitSelect["Sleep"] != sleep || _habbitSelect["Sport"] != sport || _habbitSelect["Work"] != work)
        {
            if(!_canChangeHabbit && !_traitList.ContainsKey("意志坚定"))
            {
                Debug.Log("习惯适应期");
                buffs.TriggerBuff("习惯适应期");
            }
        }
        
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
                    if(!_traitList.ContainsKey("意志坚定"))
                    {
                        Debug.Log(behavName + "戒断反应");
                        buffs.TriggerBuff("戒断反应");
                    }
                    
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

    void Cure()
    {
        Dictionary<string, Config.Buffs.Buff> tmpList = new Dictionary<string, Config.Buffs.Buff>(_buffList);
        foreach (var buff in tmpList)
        {
            if(buff.Value._canCure)
            {
                _ownMoney -= buff.Value._moneyToCure;
            }
            _buffCount.Remove(buff.Key);
            _buffList.Remove(buff.Key);
            EventCenter.GetInstance().EventTrigger<string>("DestroyBuffObj", buff.Key);
        }
        EventCenter.GetInstance().EventTrigger("UpdateUI");
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

        Debug.Log(tmpHealthChange);
        return tmpHealthChange;
    }
    private void UpdateByDefault()
    {
        _age += 1f;
        _sansPer += sansChange;
        _motorPer += moduleChange;
        _nervePer += moduleChange;
        _endocPer += moduleChange;
        _circulPer += moduleChange;
        _breathPer += moduleChange;
        _digestPer += moduleChange;
        _urinaryPer += moduleChange;
        _reprodPer += moduleChange;
        _overallHealth += CalculHealth(); 
    }

    private void UpdateByBehaviour()
    {
        foreach (var selected in _behaviourSelect)
        {
            foreach (var behav in behaviours._behaviourList)
            {
                if(behav._name.Trim() == selected.Trim())
                {
                    _sansPer += behav._sans;
                    _motorPer += behav._motor;
                    _nervePer += behav._nerve;
                    _endocPer += behav._endoc;
                    _circulPer += behav._circul;
                    _breathPer += behav._breath;
                    _digestPer += behav._digest;
                    _urinaryPer += behav._urinary;
                    _reprodPer += behav._reprod;
                    _moneyPer += behav._money;

                    if(behav._randEvent!=null)
                    {
                        behav._randEvent.Invoke();
                    }

                    if(_behavBook.ContainsKey(behav._name))
                    {
                        //Debug.Log("是否"+_behavBook[behav._name]+" > "+behav._buffYear + "  ??");
                        if(_behavBook[behav._name] > behav._buffYear && behav._buffYear != 0f)
                        {
                            behav._buff.Invoke();
                        }
                    }
                }
            }
        }
    }

    private void UpdateByHabbit()
    {
        
        foreach (var selected in _habbitSelect)
        {
            foreach (var habbit in habbits.habbitDic[selected.Key])
            { 
                if(habbit._name.Trim() == selected.Value.Trim())
                {
                    _sansPer += habbit._sans;
                    _motorPer += habbit._motor;
                    _nervePer += habbit._nerve;
                    _endocPer += habbit._endoc;
                    _circulPer += habbit._circul;
                    _breathPer += habbit._breath;
                    _digestPer += habbit._digest;
                    _urinaryPer += habbit._urinary;
                    _reprodPer += habbit._reprod;
                    _moneyPer += habbit._money;
                    if(habbit._randEvent!=null)
                    {
                        habbit._randEvent.Invoke();
                    }
                }
            }
        }
    }
    private void UpdateByNonRestrictedEvents()
    {
        foreach (var tmpEvent in randEvents._randEventList)
        {
            if(!tmpEvent._isCondition)
            {
                if(tmpEvent._availibleStage.Trim() == GetStage() ||
                    (tmpEvent._availibleStage.Trim() == "青中年期" && (GetStage() == "青年期" || GetStage() == "中年期")) ||
                    (tmpEvent._availibleStage.Trim() == "中老年期" && (GetStage() == "中年期" || GetStage() == "老年期")) ||
                    tmpEvent._availibleStage.Trim() == "全阶段")
                    {
                        randEvents.TriggerRandEvent(tmpEvent._name);
                        tmpEvent._happenNum-=1;
                    }
            }
        }
    }

    private void UpdateByEvents(Config.RandEvents.RandEvent tmpEvents)
    {
        if(tmpEvents._traitReq != null)
            if(!_traitList.ContainsKey(tmpEvents._traitReq))
                return;
        
        _sansPer += tmpEvents._sans;
        _motorPer += tmpEvents._motor;
        _nervePer += tmpEvents._nerve;
        _endocPer += tmpEvents._endoc;
        _circulPer += tmpEvents._circul;
        _breathPer += tmpEvents._breath;
        _digestPer += tmpEvents._digest;
        _urinaryPer += tmpEvents._urinary;
        _reprodPer += tmpEvents._reprod;
        _moneyPer += tmpEvents._money;

        if(tmpEvents._buff!=null)
        {
            tmpEvents._buff.Invoke();
        }
    }
    private void UpdateByBuffList()
    {
        if(_buffList == null)
            return;

        if(_buffList.Count == 0)
            return;

        Dictionary<string, float> tmpList = new Dictionary<string, float>(_buffCount);

        foreach (var buffName in tmpList.Keys)
        {
            _sansPer += _buffList[buffName]._sans;
            _motorPer += _buffList[buffName]._motor;
            _nervePer += _buffList[buffName]._nerve;
            _endocPer += _buffList[buffName]._endoc;
            _circulPer += _buffList[buffName]._circul;
            _breathPer += _buffList[buffName]._breath;
            _digestPer += _buffList[buffName]._digest;
            _urinaryPer += _buffList[buffName]._urinary;
            _reprodPer += _buffList[buffName]._reprod;
            _moneyPer += _buffList[buffName]._money;

            _forceMoneyChange = _buffList[buffName]._moneyChange == 0 ? false : true;
            _forceMoneyChangeValue = _buffList[buffName]._moneyChange == -1 ? 0f : _buffList[buffName]._moneyChange;

            _buffCount[buffName] -= 1f;

            if(_buffCount[buffName] <= 0f)
            {
                _buffCount.Remove(buffName);
                _buffList.Remove(buffName);
                EventCenter.GetInstance().EventTrigger<string>("DestroyBuffObj", buffName);
            }
        }
    }
    private void UpdateBuffList(Config.Buffs.Buff tmpBuff)
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


    private void InitTempData()
    {
        _motorPer = 0f;
        _nervePer = 0f;
        _endocPer = 0f;
        _circulPer = 0f;
        _breathPer = 0f;
        _digestPer = 0f;
        _urinaryPer = 0f;
        _reprodPer = 0f;
        _moneyPer = 0f;
        _sansPer = 0f;
    }
    private void CheckAndUpdateConditionData()
    {
        _overallSans += _sansPer;
        _motor += _motorPer;
        _nerve += _nervePer;
        _endoc += _endocPer;
        _circul += _circulPer;
        _breath += _breathPer;
        _digest += _digestPer;
        _urinary += _urinaryPer;
        _reprod += _reprodPer;
        _ownMoney += _moneyPer;

        if( _overallHealth < 0 || _overallSans < 0)
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

    private void CheckForceChange()
    {
        if(_forceMoneyChange)
            _moneyPer = _forceMoneyChangeValue;
    }

    private void UpdateTrait(Config.Traits.Trait tmpTrait)
    {
        _sansPer += tmpTrait._sans;
        _motorPer += tmpTrait._motor;
        _nervePer += tmpTrait._nerve;
        _endocPer += tmpTrait._endoc;
        _circulPer += tmpTrait._circul;
        _breathPer += tmpTrait._breath;
        _digestPer += tmpTrait._digest;
        _urinaryPer += tmpTrait._urinary;
        _reprodPer += tmpTrait._reprod;
        _moneyPer += tmpTrait._moneyChange;
    }
    private void UpdateByTrait()
    {
        foreach (var trait in _traitList)
        {
            if(trait.Value._conditionVariable == "特殊事件")
            {
                continue;
            }
            else if(trait.Value._conditionVariable == "习惯")
            {
                if(!_habbitSelect.ContainsValue(trait.Value._condition.Trim()))
                {
                    continue;
                }
            }
            else if(trait.Value._conditionVariable == "固定事件")
            {
                if(! _behaviourSelect.Contains(trait.Value._condition.Trim()))
                {
                    continue;
                }
            }
            UpdateTrait(trait.Value);
        }
    }

    public void Reset()
    {
        randEvents.eventNum = eventNum;
        _traitList.Clear();
        _buffCount.Clear();
        _buffList.Clear();
        _behavBook.Clear();
        _habbitSelect.Clear();
        _behaviourSelect.Clear();

        List<Config.Traits.Trait> tmpTraitList = new List<Config.Traits.Trait>(traits._traitList);
        for (int i = 0; i < traitNum; i++)
        {
            int tmpIndex = Random.Range(0, (int)(tmpTraitList.Count-1));
            _traitList.Add(tmpTraitList[tmpIndex]._name, tmpTraitList[tmpIndex]);
            tmpTraitList.RemoveAt(tmpIndex);
        }
        Init();

        foreach (var behav in behaviours._behaviourList)
        {
            if(!_behavBook.ContainsKey(behav._name))
                _behavBook.Add(behav._name, 0f);
            _behavBook[behav._name] = 0f;
        }

        EventCenter.GetInstance().EventTrigger("UpdateUI");
        EventCenter.GetInstance().EventTrigger("DestroyAllBuffOnUI");
    }
    public void UpdateData()
    { 
        InitTempData();
        /*
            无法进行序列化的特殊方法的更新  ↓
        */

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

        /*
            序列化框架内的更新  ↓
        */
        UpdateByBuffList(); // 根据BUFF更新数据
        UpdateByHabbit();   // 根据习惯更新数据
        UpdateByBehaviour();    // 根据固定事件更新数据
        UpdateByDefault();  // 根据默认设定更新影响
        UpdateByTrait();   // 根据特性更新数据
        UpdateByNonRestrictedEvents();  //有概率触发非限制事件
        
        CheckForceChange();  //检查有没有强制的数据变化
        CheckAndUpdateConditionData();  // 检查并更新Condition值
    }
}
