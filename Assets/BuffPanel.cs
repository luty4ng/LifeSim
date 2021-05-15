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
    }

    void UpdateBuffUI()
    {
        foreach (var name in _protagonist.GetBuffCount().Keys)
        {
            if(_buffObj.Contains(name))
                continue;
            
            GameObject traitContent = ResourceManager.GetInstance().Load<GameObject>("UI/BuffUI");
            traitContent.transform.SetParent(this.transform);
            traitContent.transform.localScale = Vector3.one;
            traitContent.GetComponentInChildren<TextMeshProUGUI>().text = name;
            traitContent.name = name;
            _buffObj.Add(name);
        }
    }

    void DestroyBuff(string buffName)
    {
        _buffObj.Remove(buffName);
        Destroy(this.transform.Find(buffName).gameObject);
    }
}
