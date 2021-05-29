using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Michsky.UI.ModernUIPack;

public class BehaviourPanel : MonoBehaviour
{
    [SerializeField] private Protagonist _protagonist;
    [SerializeField] private  List<CustomToggle> _toggles;
    private Dictionary<string, List<string>> behavDic = new Dictionary<string, List<string>>();
    private CustomToggle[] viewers;
    void Start()
    {
        EventCenter.GetInstance().AddEventListener("TransitData", () => {
            List<string> tmpBehav = new List<string>();
            foreach (var item in viewers)
            {
                if(item.toggleObject.isOn)
                {
                    tmpBehav.Add(item.toggleSelection.text);
                }
            }
            _protagonist.UpdateBehaviourSelect(tmpBehav);
        });

        EventCenter.GetInstance().AddEventListener("UpdateUI", UpdateBehavPanel);
        
        _protagonist = GameManager.instance.playerAgent.GetComponent<Protagonist>();
        viewers = transform.GetComponentsInChildren<CustomToggle>();

        foreach (var behav in _protagonist.behaviours._behaviourList)
        {
            if(!behavDic.ContainsKey(behav._availibleStage))
                behavDic.Add(behav._availibleStage, new List<string>());
            behavDic[behav._availibleStage].Add(behav._name);
        }
        UpdateBehavPanel();
    }

    void UpdateBehavPanel()
    {
        List<string> tmpBebav = new List<string>(behavDic["全阶段"]);
        tmpBebav.AddRange(behavDic[_protagonist.GetStage()]);
        if(tmpBebav.Count == viewers.Length)
        {
           for (int i = 0; i < tmpBebav.Count; i++)
           {
               viewers[i].toggleObject.isOn = false;
               viewers[i].toggleSelection.text = tmpBebav[i];
           } 
        }

    }

    public void BehaviourSound()
    {
        Debug.Log("Play Behaviour Sound");
        AudioManager.GetInstance().PlaySound("Bahaviour", (AudioSource source) => {});
    }
}
