using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextYearButton : MonoBehaviour
{
    public void TriggerNextYear()
    {
        // 更新本回合数据，确保输入数据的正确性
        EventCenter.GetInstance().EventTrigger("TransitData");
        // 更新下回合数据
        EventCenter.GetInstance().EventTrigger("UpdateData");
        // 更新下回合UI变更
        EventCenter.GetInstance().EventTrigger("UpdateUI");
    }

}
