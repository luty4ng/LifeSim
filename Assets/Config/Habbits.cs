using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;

public class Habbits : SerializedMonoBehaviour
{
    // public List<Habbit> _habbit;

    public Dictionary<string, List<Habbit>> _habbitDic;

}

public abstract class Habbit
{
    [LabelText("名称")]
    public string _name;
    [LabelText("精神影响")]
    public float _sans;
    [LabelText("运动系统影响")]
    public float _motor;
    [LabelText("神经系统影响")]
    public float _nerve;
    [LabelText("内分泌系统影响")]
    public float _endoc;
    [LabelText("循环系统影响")]
    public float _circul;
    [LabelText("呼吸系统影响")]
    public float _breath; 
    [LabelText("消化系统影响")]
    public float  _digest; 
    [LabelText("泌尿系统影响")]
    public float  _urinary; 
    [LabelText("生殖系统影响")]
    public float _reprod;
    [LabelText("金钱影响")]
    public float _money;
    [LabelText("随机事件影响（暂定）")]
    public  string _randEvent;
    
}


public class Eat : Habbit
{
       
}

public class Sleep : Habbit
{
    
}

public class Sport : Habbit
{
    
}

public class Work : Habbit
{
    
}