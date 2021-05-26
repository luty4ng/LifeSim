using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class StageIcon : MonoBehaviour
{
    public TextMeshProUGUI stageText;
    private Protagonist _protagonist;
    void Start()
    {
        _protagonist = GameManager.instance.playerAgent.GetComponent<Protagonist>();
        EventCenter.GetInstance().AddEventListener("UpdateUI", () => {
            StartCoroutine(SetStageText());
        });
        StartCoroutine(SetStageText());
    }

    IEnumerator SetStageText()
    {
        yield return new WaitForSeconds(0.01f);
        stageText.text = _protagonist.GetStage();
    }
}
