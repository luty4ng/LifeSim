using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Sirenix.OdinInspector;

public class BuffPanel : MonoBehaviour
{
    [SerializeField] private Protagonist _protagonist;
    [ShowInInspector] private List<string> _buffObj = new List<string>();
    void Start()
    {
        _protagonist = GameManager.instance.playerAgent.GetComponent<Protagonist>(); 
        EventCenter.GetInstance().AddEventListener("UpdateUI", UpdateBuffUI);
        EventCenter.GetInstance().AddEventListener<string>("DestroyBuffObj", DestroyBuff);
        EventCenter.GetInstance().AddEventListener("DestroyAllBuffOnUI", ()=>{
            RectTransform[] tmpRects = this.GetComponentsInChildren<RectTransform>();
            foreach (var item in tmpRects)
            {
                Destroy(item.gameObject);
            }
        });
    }

    void UpdateBuffUI()
    {
        foreach (var name in _protagonist.GetBuffCount().Keys)
        {
            if(_buffObj.Contains(name))
                continue;
            
            GameObject buffContent = ResourceManager.GetInstance().Load<GameObject>("UI/BuffUI");
            buffContent.transform.SetParent(this.transform);
            buffContent.transform.localScale = Vector3.one;
            buffContent.GetComponentInChildren<TextMeshProUGUI>().text = name;
            buffContent.name = name;
            _buffObj.Add(name);
        }
    }

    void DestroyBuff(string buffName)
    {
        _buffObj.Remove(buffName);
        Destroy(this.transform.Find(buffName).gameObject);
    }
}
