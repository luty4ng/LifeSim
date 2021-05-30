using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.Events;
using Michsky.UI.ModernUIPack;

public class TipsPanel : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject allBoxsObj;
    private bool hasEvent = false;

    public Button confirmButton;
    public Animator mwAnimator;
    // Events
    public UnityEvent onConfirm;
    // Settings
    public bool sharpAnimations = false;

    public bool isOn = false;
    public bool isOver = false;
    
    
    void Start()
    {
        allBoxsObj = GameObject.Find("AllBoxs");
        if (mwAnimator == null)
                mwAnimator = gameObject.GetComponent<Animator>();

        if (confirmButton != null)
            confirmButton.onClick.AddListener(onConfirm.Invoke);

        EventCenter.GetInstance().AddEventListener<(string title, string desc)>("TIPS", (info)=>{
            GameObject messageBox = ResourceManager.GetInstance().Load<GameObject>("UI/MessageBox");
            messageBox.transform.SetParent(allBoxsObj.transform);
            messageBox.transform.localScale = Vector3.one;

            messageBox.GetComponent<MessageBox>().titleTMPro.text = info.title;
            messageBox.GetComponent<MessageBox>().descTMPro.text = info.desc;

            OpenWindow();
        });

        EventCenter.GetInstance().AddEventListener("随机事件弹窗", (Config.RandEvents.RandEvent randEvent)=>{
                //CloseWindows();
                GameObject messageBox = ResourceManager.GetInstance().Load<GameObject>("UI/MessageBox");
                messageBox.transform.SetParent(allBoxsObj.transform);
                messageBox.transform.localScale = Vector3.one;

                messageBox.GetComponent<MessageBox>().titleTMPro.text = randEvent._name;
                messageBox.GetComponent<MessageBox>().descTMPro.text = randEvent._desc;
                hasEvent = true;
        });

        EventCenter.GetInstance().AddEventListener("UpdateUI",() => {
            if(hasEvent)
                OpenWindow();
            hasEvent = false;
        });

        EventCenter.GetInstance().AddEventListener("GAMEOVER",() => {
            //CloseWindows();
            GameObject messageBox = ResourceManager.GetInstance().Load<GameObject>("UI/MessageBox");
            messageBox.transform.SetParent(allBoxsObj.transform);
            messageBox.transform.localScale = Vector3.one;

            messageBox.GetComponent<MessageBox>().titleTMPro.text = "游戏结束";
            messageBox.GetComponent<MessageBox>().descTMPro.text = "点击任意处退出/开始新的人生";
            
            OpenWindow();
            // StartCoroutine(RestartGame());
            isOver = true;
            
            // GameManager.instance.playerAgent.Reset();
            
            Time.timeScale = 0f;
            

        });
    }

            

        public void OpenWindow()
        {
            if (isOn == false)
            {
                if (sharpAnimations == false)
                    mwAnimator.CrossFade("Fade-in", 0.1f);
                else
                    mwAnimator.Play("Fade-in");

                isOn = true;
            }
        }

        public void CloseWindow()
        {
            if (isOn == true)
            {
                if (sharpAnimations == false)
                    mwAnimator.CrossFade("Fade-out", 0.1f);
                else
                    mwAnimator.Play("Fade-out");

                isOn = false;
            }

            if(isOver)
            {
                EventCenter.GetInstance().Clear();
                StopAllCoroutines();
                ScenesManager.GetInstance().LoadScene("Main", ()=>{});
            }
        }

        public void AnimateWindow()
        {
            if (isOn == false)
            {
                if (sharpAnimations == false)
                    mwAnimator.CrossFade("Fade-in", 0.1f);
                else
                    mwAnimator.Play("Fade-in");

                isOn = true;
            }

            else
            {
                if (sharpAnimations == false)
                    mwAnimator.CrossFade("Fade-out", 0.1f);
                else
                    mwAnimator.Play("Fade-out");

                isOn = false;
            }
        }


    IEnumerator RestartGame()
    {
        yield return new WaitForSeconds(0f);
        GameManager.instance.cover.SetActive(true);
    }
    public void CloseWindows()
    {
        if(Time.timeScale == 0)
        {
            Time.timeScale = 1f;
            StartCoroutine(RestartGame());
        }
        
        CloseWindow();
        MessageBox[] boxComps = GetComponentsInChildren<MessageBox>();
        foreach (var comp in boxComps)
        {
            Destroy(comp.gameObject);
        }
    }

}
