using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using TMPro;


public class BuffClick : MonoBehaviour, IPointerClickHandler
{

    [SerializeField] private Protagonist _protagonist;
    public UnityEvent leftClick;
    public UnityEvent rightClick;



    private void Start()
    {
        leftClick.AddListener(new UnityAction(ButtonLeftClick));
        rightClick.AddListener(new UnityAction(ButtonRightClick));
        _protagonist = GameObject.Find("Protagonist").GetComponent<Protagonist>();
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
            leftClick.Invoke();
        else if (eventData.button == PointerEventData.InputButton.Right)
            rightClick.Invoke();
    }


    private void ButtonLeftClick()
    {
        // Debug.Log(this.name + "Button Left Click");
        EventCenter.GetInstance().EventTrigger<(string, string)>("TIPS" , (this.name.Trim() + "-影响", _protagonist.GetBuffDesc(this.name.Trim(), true)));
        
    }

    private void ButtonRightClick()
    {
        // Debug.Log(this.name + "Button Right Click");
        EventCenter.GetInstance().EventTrigger<(string, string)>("TIPS", (this.name.Trim() + "-来源", _protagonist.GetBuffDesc(this.name.Trim(), false)));
    }


}
