using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TraitPanel : MonoBehaviour
{
    [SerializeField] private Protagonist _protagonist;
    void Start()
    {
        _protagonist = GameObject.Find("Protagonist").GetComponent<Protagonist>();
        foreach (var name in _protagonist.GetTraitsList().Keys)
        {
            GameObject traitContent = ResourceManager.GetInstance().Load<GameObject>("UI/traitUI");
            traitContent.transform.SetParent(this.transform);
            traitContent.transform.localScale = Vector3.one;
            traitContent.GetComponentInChildren<TextMeshProUGUI>().text = name;
            traitContent.GetComponent<Button>().onClick.AddListener(() => {
                EventCenter.GetInstance().EventTrigger<(string, string)>("TIPS", (name, _protagonist.GetTraitsList()[name]));
            });
        }
        // StartCoroutine(Init());
        
    }

    IEnumerator Init()
    {
        yield return new WaitForSeconds(0.05f);
        foreach (var name in _protagonist.GetTraitsList().Keys)
        {
            GameObject traitContent = ResourceManager.GetInstance().Load<GameObject>("UI/traitUI");
            traitContent.transform.SetParent(this.transform);
            traitContent.transform.localScale = Vector3.one;
            traitContent.GetComponentInChildren<TextMeshProUGUI>().text = name;
            traitContent.GetComponent<Button>().onClick.AddListener(() => {
                EventCenter.GetInstance().EventTrigger<(string, string)>("TIPS", (name, _protagonist.GetTraitsList()[name]));
            });
        }
    }
}
