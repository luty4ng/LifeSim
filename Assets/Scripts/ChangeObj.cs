using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ChangeObj : MonoBehaviour
{
    private Sequence mySeq;
    public SpriteRenderer spriteRenderer;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        mySeq = DOTween.Sequence();

        Reset();
        EventCenter.GetInstance().AddEventListener<string>("切换角色", (string name)=>{
            Debug.Log("切换角色");
            Reset();
            if(name == "青年期")
                spriteRenderer.sprite = ResourceManager.GetInstance().Load<Sprite>("Change/young");
            else if(name == "中年期")
                spriteRenderer.sprite = ResourceManager.GetInstance().Load<Sprite>("Change/middle");
            else
                spriteRenderer.sprite = ResourceManager.GetInstance().Load<Sprite>("Change/old");

            mySeq.Append(spriteRenderer.DOBlendableColor(new Color(1,1,1,1), 1f)).Append(transform.DOBlendableScaleBy(new Vector3(0.242f/2,0.242f/2,0.242f/2), 1f));
            StartCoroutine(MoveAway(3f));
        });
    }

    void Reset()
    {
        spriteRenderer.color = new Color(1,1,1,0);
        this.transform.position = new Vector3(0.1f, 2.73f, 0f);
        this.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
    }

    IEnumerator MoveAway(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Debug.Log("MoveAway");
        mySeq.Append(this.transform.DOLocalMoveX(-4f, 3f));
    }
}
