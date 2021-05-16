using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;
using Config.Habbits;
using Config.RandEvents;
using Config.Buffs;
using Config.Behavious;

namespace Config.Traits
{
    public class Traits : SerializedMonoBehaviour
    {
        [LabelText("特性列表"),TableList]
        public List<Trait> _traitList;
    }

    public class Trait
    {
        
        private static string[] selection = new string[] {"无","习惯","固定事件"}; 
        public string _name;
        [LabelText("条件变量"), ValueDropdown("selection"), FoldoutGroup("条件及描述")]
        public string _conditionVariable;
        [LabelText("条件选择"), FoldoutGroup("条件及描述")]
        public string _condition;
        [LabelText("特殊条件"), FoldoutGroup("条件及描述")]
        public bool hasSpecial;
        [LabelText("特性描述"), TextArea, FoldoutGroup("条件及描述")]
        public string _desc;
        [LabelText("精神恢复"), FoldoutGroup("特性效果")]
        public float _sans;
        [LabelText("运动模块恢复"), FoldoutGroup("特性效果")]
        public float _motor;
        [LabelText("神经模块恢复"), FoldoutGroup("特性效果")]
        public float _nerve;
        [LabelText("内分泌模块恢复"), FoldoutGroup("特性效果")]
        public float _endoc;
        [LabelText("循环模块恢复"), FoldoutGroup("特性效果")]
        public float _circul;
        [LabelText("呼吸模块恢复"), FoldoutGroup("特性效果")]
        public float _breath; 
        [LabelText("消化模块恢复"), FoldoutGroup("特性效果")]
        public float  _digest; 
        [LabelText("泌尿模块恢复"), FoldoutGroup("特性效果")]
        public float  _urinary; 
        [LabelText("生殖模块恢复"), FoldoutGroup("特性效果")]
        public float _reprod;
        [LabelText("金钱产出变化"), FoldoutGroup("特性效果")]
        public float _moneyChange;
    }
}
