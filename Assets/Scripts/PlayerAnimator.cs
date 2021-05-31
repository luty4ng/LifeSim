using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class PlayerAnimator : MonoBehaviour
{
    private Animator animator;
    private Protagonist protagonist;
    [ShowInInspector, LabelText("当前年龄段"), DisplayAsString]
    private string tempStage;
    void Start()
    {
        animator = GetComponent<Animator>();
        protagonist = GetComponent<Protagonist>();
        tempStage = protagonist.GetStage();
        EventCenter.GetInstance().AddEventListener("UpdateAnimation", UpdateAnimation);
    }

    public void UpdateAnimation()
    {
        if(protagonist.GetStage() != tempStage)
        {
            NextYearButton nextYearButton = GameObject.Find("NextYearButton").GetComponent<NextYearButton>();
            nextYearButton.ResetRemain();
            if(protagonist.GetStage() == "中年期")
            {
                // animator.SetTrigger("Middle");
                StartCoroutine(ChangeAnimation("Middle"));
            }
            else if(protagonist.GetStage() == "老年期")
            {
                // animator.SetTrigger("Old");
                StartCoroutine(ChangeAnimation("Old"));
            }
            EventCenter.GetInstance().EventTrigger<string>("切换角色", protagonist.GetStage());
        }
        tempStage = protagonist.GetStage();
        
    }

    IEnumerator ChangeAnimation(string name)
    {
        yield return new WaitForSeconds(4f);
        animator.SetTrigger(name);
    }
}
