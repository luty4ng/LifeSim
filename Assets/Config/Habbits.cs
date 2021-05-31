using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;

namespace Config.Habbits
{
    public class Habbits : SerializedMonoBehaviour
    {
        // public List<Habbit> _habbit;
        [LabelText("习惯配表")]
        public Dictionary<string, List<Habbit>> habbitDic;

    }

    public abstract class Habbit
    {
        [LabelText("名称")]
        public string _name;
        [LabelText("习惯描述"), TextArea]
        public string _desc;
        [LabelText("精神影响"), FoldoutGroup("习惯影响")]
        public float _sans;
        [LabelText("运动系统影响"), FoldoutGroup("习惯影响")]
        public float _motor;
        [LabelText("神经系统影响"), FoldoutGroup("习惯影响")]
        public float _nerve;
        [LabelText("内分泌系统影响"), FoldoutGroup("习惯影响")]
        public float _endoc;
        [LabelText("循环系统影响"), FoldoutGroup("习惯影响")]
        public float _circul;
        [LabelText("呼吸系统影响"), FoldoutGroup("习惯影响")]
        public float _breath; 
        [LabelText("消化系统影响"), FoldoutGroup("习惯影响")]
        public float  _digest; 
        [LabelText("泌尿系统影响"), FoldoutGroup("习惯影响")]
        public float  _urinary; 
        [LabelText("生殖系统影响"), FoldoutGroup("习惯影响")]
        public float _reprod;
        [LabelText("金钱影响"), FoldoutGroup("习惯影响")]
        public float _money;
        [LabelText("随机事件影响"), FoldoutGroup("习惯影响")]
        public  UnityEvent _randEvent;
        [LabelText("BUFF影响"), FoldoutGroup("习惯影响")]
        public  UnityEvent _buff;
        [LabelText("BUFF生效年限"), FoldoutGroup("习惯影响")]
        public  float _buffYear;
        
        
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

}
