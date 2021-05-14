using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;
namespace Config.Behavious
{
    public class Behaviours : SerializedMonoBehaviour
    {
        [LabelText("固定事件（行为）列表"), TableList]
        public List<Behaviour> _behaviourList;
    }

    public class Behaviour
    {
        private static string[] _stages = new string[] {"青年期","中年期","老年期","全阶段"}; 
        public string _name;
        [LabelText("可执行阶段"), ValueDropdown("_stages"), FoldoutGroup("行为属性")]
        public string _availibleStage;
        [LabelText("戒断年限"), FoldoutGroup("行为属性")]
        public float _stopYear;
        
        [LabelText("精神影响"), FoldoutGroup("行为影响")]
        public float _sans;
        [LabelText("运动系统影响"), FoldoutGroup("行为影响")]
        public float _motor;
        [LabelText("神经系统影响"), FoldoutGroup("行为影响")]
        public float _nerve;
        [LabelText("内分泌系统影响"), FoldoutGroup("行为影响")]
        public float _endoc;
        [LabelText("循环系统影响"), FoldoutGroup("行为影响")]
        public float _circul;
        [LabelText("呼吸系统影响"), FoldoutGroup("行为影响")]
        public float _breath; 
        [LabelText("消化系统影响"), FoldoutGroup("行为影响")]
        public float  _digest; 
        [LabelText("泌尿系统影响"), FoldoutGroup("行为影响")]
        public float  _urinary; 
        [LabelText("生殖系统影响"), FoldoutGroup("行为影响")]
        public float _reprod;
        [LabelText("金钱影响"), FoldoutGroup("行为影响")]
        public float _money;
        [LabelText("随机事件影响"), FoldoutGroup("行为影响")]
        public  UnityEvent _randEvent;
        [LabelText("BUFF影响"), FoldoutGroup("行为影响")]
        public  UnityEvent _buff;   
        [LabelText("BUFF生效年限"), FoldoutGroup("行为影响")]
        public float _buffYear;
    }

}

