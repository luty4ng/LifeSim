using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehavIcon : MonoBehaviour
{
    public Animator iconBGAnimator;
    public Animator iconAnimator;
    private Protagonist _protagonist;
    private Dictionary<string, string> behavBook = new Dictionary<string, string>(){
        {"饮酒", "Yinjiu"},{"吸烟", "Xiyan"},{"加餐", "Jiacan"},{"阅读", "Yuedu"},{"旅行", "Lvxing"},{"电子游戏", "Dianziyouxi"},{"追星", "Zhuixing"},
        {"纵欲", "Zongyu"},{"打麻将", "Damajiang"},{"钓鱼", "Diaoyu"},{"炒股", "Chaogu"},{"买保健品", "Maibaojian"},{"遛鸟", "Liuniao"},{"下象棋", "Xiaqi"}
    };

    void Start()
    {
        _protagonist = GameObject.Find("Protagonist").GetComponent<Protagonist>();
        iconBGAnimator.SetBool("Finished", false);
        EventCenter.GetInstance().AddEventListener("UpdateICON", ()=>{
            try
            {
                StopAllCoroutines();
            }
            catch (System.Exception)
            {
                Debug.Log("协程停止问题");
            }
            
            StartCoroutine(OutputAnimationList());
        });

        EventCenter.GetInstance().AddEventListener("GAMEOVER", ()=>{
            StopAllCoroutines();
        });
    }

    IEnumerator OutputAnimationList()
    {
        List<string> animationList = _protagonist.GetBehavSelect();
        foreach (var behav in animationList)
        {
            if(!behavBook.ContainsKey(behav))
                continue;
            iconAnimator.SetTrigger(behavBook[behav]);
            yield return new WaitForSeconds(2f);
        }
        
    }

}
