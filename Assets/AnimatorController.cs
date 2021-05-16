using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class AnimatorController : MonoBehaviour
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
            if(protagonist.GetStage() == "中年期")
            {
                animator.SetTrigger("Middle");
            }
            else if(protagonist.GetStage() == "老年期")
            {
                animator.SetTrigger("Old");
            }
        }
        tempStage = protagonist.GetStage();
    }
}
