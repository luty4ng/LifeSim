using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;

namespace Config.Buffs
{
    public class Buffs : SerializedMonoBehaviour
    {
        [LabelText("BUFF列表"), TableList]
        public List<Buff> _buffList;

        public void TriggerBuff(string name)
        {
            Debug.Log("暂时检测到BUFF" + name);
            if(_buffList!=null)
            {
                foreach (var item in _buffList)
                {
                    if(item._name!=null && item._name.Trim() == name.Trim())
                    {
                        Debug.Log("检测到BUFF" + name);
                        EventCenter.GetInstance().EventTrigger<Buff>("新Buff触发", item);
                        break;
                    }
                }
            }
        }
    }

    public class Buff
    {
        public string _name;
        [LabelText("持续年限"), FoldoutGroup("BUFF属性")]
        public float _remainYears;
        [LabelText("可被治愈"), FoldoutGroup("BUFF属性")]
        public bool _canCure;
        [LabelText("治疗价格"), FoldoutGroup("BUFF属性")]
        public float _moneyToCure;
        [LabelText("精神影响"), FoldoutGroup("BUFF影响")]
        public float _sans;
        [LabelText("运动系统影响"), FoldoutGroup("BUFF影响")]
        public float _motor;
        [LabelText("神经系统影响"), FoldoutGroup("BUFF影响")]
        public float _nerve;
        [LabelText("内分泌系统影响"), FoldoutGroup("BUFF影响")]
        public float _endoc;
        [LabelText("循环系统影响"), FoldoutGroup("BUFF影响")]
        public float _circul;
        [LabelText("呼吸系统影响"), FoldoutGroup("BUFF影响")]
        public float _breath; 
        [LabelText("消化系统影响"), FoldoutGroup("BUFF影响")]
        public float  _digest; 
        [LabelText("泌尿系统影响"), FoldoutGroup("BUFF影响")]
        public float  _urinary; 
        [LabelText("生殖系统影响"), FoldoutGroup("BUFF影响")]
        public float _reprod;
        [LabelText("金钱影响"), FoldoutGroup("BUFF影响")]
        public float _money;
        [LabelText("金钱产出影响"), FoldoutGroup("BUFF影响")]
        public float _moneyChange;

    }
}
