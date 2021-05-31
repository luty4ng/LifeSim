using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Michsky.UI.ModernUIPack;

public class NextYearButton : MonoBehaviour
{
    public float remainMax = 2f;
    private float remain;
    [SerializeField] private bool hasHint = false;
    [SerializeField] private GameObject habbitButton;
    [SerializeField] private GameObject conditionButton;
    [SerializeField] private WindowManager windowManager;

    void Start()
    {
        remain = remainMax;
    }
    public void TriggerNextYear()
    {
        if(!hasHint && remain > 0)
        {
            if(windowManager.currentWindowIndex != 1f)
            {
                habbitButton.GetComponent<Button>().onClick.Invoke();
                hasHint = true;
                remain -= 1f;
                return;
            }
        }

        conditionButton.GetComponent<Button>().onClick.Invoke();
        hasHint = false;


        // 更新本回合数据，确保输入数据的正确性
        EventCenter.GetInstance().EventTrigger("TransitData");
        // 优先更新当前回合BUFF集合的影响
        // EventCenter.GetInstance().EventTrigger("UpdateExistBuff");
        // 更新下回合数据
        EventCenter.GetInstance().EventTrigger("UpdateData");
        // 更新下回合UI变更
        EventCenter.GetInstance().EventTrigger("UpdateUI");
        EventCenter.GetInstance().EventTrigger("UpdateICON");
        // 更新人物动画（如有必要）
        EventCenter.GetInstance().EventTrigger("UpdateAnimation");
    }


    public void ResetRemain() => remain = remainMax;

}
