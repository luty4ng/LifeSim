using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RulerAnimator : MonoBehaviour
{

    void Start()
    {
        transform.DOMoveX(-8.61f, 5f).SetEase(Ease.Linear);
    }


    void Update()
    {
        
    }
}
