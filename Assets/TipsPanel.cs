using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Michsky.UI.ModernUIPack;

public class TipsPanel : MonoBehaviour
{
    // Start is called before the first frame update
    public ModalWindowManager modal;
    
    void Awake()
    {
        modal = GetComponent<ModalWindowManager>();
    }
    
    void Start()
    {
        //backgound.SetActive(false);
        //tipsContent.SetActive(false);
        EventCenter.GetInstance().AddEventListener<(string title, string desc)>("TIPS", (info)=>{
            modal.titleText = info.title;
            modal.descriptionText = info.desc;
            modal.OpenWindow();
        });
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.X))
            modal.OpenWindow();
        
        if(Input.GetKeyDown(KeyCode.C))
            modal.CloseWindow();
    }
}
