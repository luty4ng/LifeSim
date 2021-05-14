using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;
using TMPro;

namespace Config.RandEvents
{
    public class RandEvents : SerializedMonoBehaviour
    {
        [LabelText("随机事件列表"), TableList]
        public List<RandEvent> _randEventList;

        public void TriggerRandEvent(string name)
        {
            if(_randEventList!=null)
            {
                foreach (var item in _randEventList)
                {
                    if(item._name!=null && item._name.Trim() == name.Trim())
                    {
                        //Debug.Log("检测到事件" + name);
                        float rand = Random.Range(0.000f, 1.000f);
                        //Debug.Log("随机数： " + rand.ToString());
                        if(rand <= item._chance)
                        {
                            Debug.Log(name + "事件发生");
                        }
                        break;
                    }
                }
            }
            
        }
    }

    public class RandEvent
    {
        public string _name;

        [LabelText("触发概率"), FoldoutGroup("事件属性")]
        public float _chance;
        [LabelText("正面事件"), FoldoutGroup("事件属性")]
        public bool _positive;
        [LabelText("事件描述（弹框）"), FoldoutGroup("事件属性"), TextArea]
        public string _desc;
        [LabelText("精神影响"), FoldoutGroup("事件影响")]
        public float _sans;
        [LabelText("运动系统影响"), FoldoutGroup("事件影响")]
        public float _motor;
        [LabelText("神经系统影响"), FoldoutGroup("事件影响")]
        public float _nerve;
        [LabelText("内分泌系统影响"), FoldoutGroup("事件影响")]
        public float _endoc;
        [LabelText("循环系统影响"), FoldoutGroup("事件影响")]
        public float _circul;
        [LabelText("呼吸系统影响"), FoldoutGroup("事件影响")]
        public float _breath; 
        [LabelText("消化系统影响"), FoldoutGroup("事件影响")]
        public float  _digest; 
        [LabelText("泌尿系统影响"), FoldoutGroup("事件影响")]
        public float  _urinary; 
        [LabelText("生殖系统影响"), FoldoutGroup("事件影响")]
        public float _reprod;
        [LabelText("金钱影响"), FoldoutGroup("事件影响")]
        public float _money;
        [LabelText("金钱生产影响"), FoldoutGroup("事件影响")]
        public float _moneyChange;
        [LabelText("BUFF影响"), FoldoutGroup("事件影响")]
        public UnityEvent _buff;   
    }
}
