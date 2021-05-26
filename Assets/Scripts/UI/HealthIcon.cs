using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthIcon : MonoBehaviour
{
    public TextMeshProUGUI healthText;
    private Protagonist _protagonist;
    void Start()
    {
        _protagonist = GameManager.instance.playerAgent.GetComponent<Protagonist>();
        EventCenter.GetInstance().AddEventListener("UpdateUI", () => {
            StartCoroutine(SetHealthText());
        });
        StartCoroutine(SetHealthText());
    }



    IEnumerator SetHealthText()
    {
        yield return new WaitForSeconds(0.01f);
        healthText.text = _protagonist.GetHealthState();
    }
}
