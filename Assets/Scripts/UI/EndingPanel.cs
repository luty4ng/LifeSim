using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class EndingPanel : MonoBehaviour
{
    public Image endingImage;
    public GameObject tipsPanel;
    private Sequence mySeq;
    private Protagonist _protagonist;
    void Start()
    {
        _protagonist = GameObject.Find("Protagonist").GetComponent<Protagonist>();
        this.gameObject.transform.localPosition = new Vector3(-1300, -39, 0f);
        this.gameObject.transform.localScale = new Vector3(0.9f, 0.9f, 0f);
        endingImage.color = new Color(1,1,1,0);
        mySeq = DOTween.Sequence();

        EventCenter.GetInstance().AddEventListener("UpdateUI",()=>{
            if(_protagonist.GetAge() >= 100f)
                Invoke("ShowPanel", 2f);
        });
    }
    public void RestartGame()
    {
        
        StopAllCoroutines();
        EventCenter.GetInstance().Clear();
        ScenesManager.GetInstance().LoadScene("Main", ()=>{});
    }

    public void ShowPanel()
    {
        tipsPanel.GetComponent<TipsPanel>().CloseWindows();
        this.gameObject.transform.localPosition = new Vector3(-7, -39, 0f);
        mySeq.Append(endingImage.DOBlendableColor(new Color(1,1,1,1), 1f)).Append(transform.DOBlendableScaleBy(new Vector3(0.13f,0.13f,0.13f), 1f));
        StartCoroutine(StopTime(2.5f));
        
    }
    void Update()
    {
        // if(Input.GetKeyDown(KeyCode.M))
        // {
        //     tipsPanel.GetComponent<TipsPanel>().CloseWindows();
        //     ShowPanel();
        // }
    }

    IEnumerator StopTime(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Time.timeScale = 0f;
    }
}
