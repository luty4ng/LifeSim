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
        [LabelText("特性列表")]
        public List<Trait> _traitList;
    }

    public class Trait
    {
        
        private static string[] selection = new string[] {"无","习惯","BUFF","随机事件","固定事件"}; 
        public string _name;
        [LabelText("条件变量"), ValueDropdown("selection"), FoldoutGroup("条件")]
        public string _selection;
        [LabelText("条件选择"), FoldoutGroup("条件")]
        public string _condition;
    }
}
