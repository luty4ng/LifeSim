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

    public void Init()
    {
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
    private float _overallHealth;
    private float _overallSans;
    private float _motor, _nerve, _endoc, _circul, _breath, _digest, _urinary, _reprod;
    private float _ownMoney;
    private float _age;
    private List<string> _traits = new List<string>();
    private UnityAction NextUpdate;

    
    void Awake()
    {
        _habbits = GetComponent<Habbits>();
    }
    
    void Start()
    {
        _age = 18f;
        _ownMoney = 500f;
        Init();

        EventCenter.GetInstance().AddEventListener("NextYear", UpdateData);

        
        //Debug.Log(_habbits._habbitDic["Eat"][0]._name);
    }

    public float GetAge() => _age;
    public float GetMoney() => _ownMoney;
    public float GetSans() => _overallSans;
    public float GetHealth()=> _overallHealth;
    public float[] GetModule() => new float[8]{ _motor, _nerve, _endoc, _circul, _breath, _digest, _urinary, _reprod };

    public void UpdateData()
    {
        _age += 1f;

    }
}
