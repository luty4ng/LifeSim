using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Michsky.UI.ModernUIPack;

public class TipsPanel : MonoBehaviour
{
    // Start is called before the first frame update
    public ModalWindowManagerCustom modal;
    public GameObject allBoxsObj;
    private bool hasEvent = false;
    
    void Awake()
    {
        modal = GetComponent<ModalWindowManagerCustom>();
    }
    
    void Start()
    {

        EventCenter.GetInstance().AddEventListener<(string title, string desc)>("TIPS", (info)=>{
            GameObject messageBox = ResourceManager.GetInstance().Load<GameObject>("UI/MessageBox");
            messageBox.transform.SetParent(allBoxsObj.transform);
            messageBox.transform.localScale = Vector3.one;

            messageBox.GetComponent<MessageBox>().titleTMPro.text = info.title;
            messageBox.GetComponent<MessageBox>().descTMPro.text = info.desc;

            modal.OpenWindow();
        });

        EventCenter.GetInstance().AddEventListener("随机事件弹窗", (Config.RandEvents.RandEvent randEvent)=>{
                GameObject messageBox = ResourceManager.GetInstance().Load<GameObject>("UI/MessageBox");
                messageBox.transform.SetParent(allBoxsObj.transform);
                messageBox.transform.localScale = Vector3.one;

                messageBox.GetComponent<MessageBox>().titleTMPro.text = randEvent._name;
                messageBox.GetComponent<MessageBox>().descTMPro.text = randEvent._desc;
                hasEvent = true;
        });

        EventCenter.GetInstance().AddEventListener("UpdateUI",() => {
            if(hasEvent)
                modal.OpenWindow();
            hasEvent = false;
        });

        EventCenter.GetInstance().AddEventListener("GAMEOVER",() => {
            
            GameObject messageBox = ResourceManager.GetInstance().Load<GameObject>("UI/MessageBox");
            messageBox.transform.SetParent(allBoxsObj.transform);
            messageBox.transform.localScale = Vector3.one;

            messageBox.GetComponent<MessageBox>().titleTMPro.text = "游戏结束";
            messageBox.GetComponent<MessageBox>().descTMPro.text = "点击任意处退出/开始新的人生";
            
            modal.OpenWindow();
            Time.timeScale = 0f;
        });
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.X))
            modal.OpenWindow();
        
        if(Input.GetKeyDown(KeyCode.C))
            modal.CloseWindow();
        
        if(Input.GetKeyDown(KeyCode.Z))
        {
            GameObject messageBox = ResourceManager.GetInstance().Load<GameObject>("UI/MessageBox");
            messageBox.transform.SetParent(allBoxsObj.transform);
            messageBox.transform.localScale = Vector3.one;

            messageBox.GetComponent<MessageBox>().titleTMPro.text = "测试弹窗";
            messageBox.GetComponent<MessageBox>().descTMPro.text = "测试弹窗描述";
        }  
    }

    public void CloseWindows()
    {
        modal.CloseWindow();
        MessageBox[] boxComps = modal.GetComponentsInChildren<MessageBox>();
        foreach (var comp in boxComps)
        {
            Destroy(comp.gameObject);
        }
    }
}
