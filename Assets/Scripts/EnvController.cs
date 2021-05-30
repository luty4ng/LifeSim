using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using DG.Tweening;

public class EnvController : MonoBehaviour
{
    private static string[] _modes = new string[] {"周期性","触发性","随机性"}; 
    private static string[] selections = new string[] {"Rock","Grass","Trash","None"}; 
    private static float[] posY = new float[] {-3.97f,-3f, -3.35f, 0f}; 
    private static Vector3[] scale = new Vector3[] {new Vector3(0.3f, 0.9f, 0.7f), new Vector3(0.4f, 0.75f, 1.26f), new Vector3(0.42f, 0.78f, 0.6f), Vector3.zero}; 
    [SerializeField] private List<GameObject> stuffObj = new List<GameObject>();
    [SerializeField] private bool moveSwitch;
    [LabelText("室外物品数量上限")]
    public float maxStuff = 1f;
    [LabelText("移动时间")]
    public float moveTime = 10f;
    [LabelText("移动周期（如果是周期性）")]
    public float movePeriod = 10f;
    [LabelText("误差系数")]
    public float errorCoef = 0.1f;
    [LabelText("背景动画模式（周期性和随机性有BUG）"), ValueDropdown("_modes")]
    public string mode;
    
    void Start()
    {
        moveSwitch = true;
        EventCenter.GetInstance().AddEventListener("UpdateAnimation", UpdateByButton);

    }

    void FixedUpdate()
    {
        
        if(mode == "周期性")
        {

            if(!moveSwitch)
                return;
            // Debug.Log(this.transform.localPosition);
            if(this.transform.localPosition.x <= 0f + errorCoef && this.transform.localPosition.x >= 0f - errorCoef)
                StartCoroutine(MoveBKG(-8.67f, movePeriod));
            else if(this.transform.localPosition.x <= 8.67f + errorCoef && this.transform.localPosition.x >= 8.67f - errorCoef)
                StartCoroutine(MoveBKG(0f, movePeriod));
        
            GenerateStuff();
        }

        if(this.transform.localPosition.x >= -8.67f - errorCoef && this.transform.localPosition.x <= -8.67f + errorCoef)
            StartCoroutine(ResetBKG());

        
    }

    IEnumerator MoveBKG(float XPos, float period)
    {
        moveSwitch = false;
        Debug.Log("READY TO MOVE");
        yield return new WaitForSeconds(period);
        Debug.Log("GO TO MOVE");
        this.transform.DOLocalMoveX(XPos, moveTime);
        moveSwitch = true;
    }

    IEnumerator ResetBKG()
    {
        yield return null;
        this.transform.DOKill();
        this.transform.localPosition = new Vector3(8.67f, 0 ,0);
    }

    void UpdateByButton()
    {
        if(mode != "触发性")
            return;
        GenerateStuff();
        if(this.transform.localPosition.x <= 0f + errorCoef && this.transform.localPosition.x >= 0f - errorCoef)
            this.transform.DOLocalMoveX(-8.67f, moveTime);
        else if(this.transform.localPosition.x <= 8.67f + errorCoef && this.transform.localPosition.x >= 8.67f - errorCoef)
            this.transform.DOLocalMoveX(0f, moveTime);
    }

    void GenerateStuff()
    {
        if(this.gameObject.name == "SceneB")
        {
            if(this.transform.localPosition.x <= 8.67f + errorCoef && this.transform.localPosition.x >= 8.67f - errorCoef)
            {
                int randIndex = Random.Range(0,3);
                if(stuffObj.Count > maxStuff)
                {
                    while (true)
                    {
                        if(stuffObj.Count <= maxStuff)
                            break;
                        int randRemove = Random.Range(0,stuffObj.Count);
                        Destroy(stuffObj[randRemove]);
                        stuffObj.RemoveAt(randRemove);
                    }
                    
                }
                if(selections[randIndex]!="None")
                {
                    GameObject newStuff = ResourceManager.GetInstance().Load<GameObject>(selections[randIndex]);
                    int randPosX = (int)Random.Range(-2.3f, 2.3f);
                    newStuff.transform.parent = this.transform;
                    newStuff.transform.localPosition = new Vector3(randPosX, posY[randIndex], 0f);
                    newStuff.transform.localScale = scale[randIndex];
                    stuffObj.Add(newStuff);
                }  
            }
        }
        
    }
    

}
